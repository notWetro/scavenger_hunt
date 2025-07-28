import React, { useContext, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";
import { DragDropContext, Droppable, Draggable } from "@hello-pangea/dnd";
import "./EditHunt.css";
import { useSearchParams } from "react-router-dom";
import { AuthContext } from "../../AuthContext";

export default function EditHunt() {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const [showDetails, setShowDetails] = useState(true);
  const [showQuestions, setShowQuestions] = useState(true);
  const [creatorName, setCreatorName] = useState("");
  const [huntLocation, setHuntLocation] = useState("");
  const [startPoint, setStartPoint] = useState("");
  const [huntNameState, setHuntNameState] = useState(""); 
  // Array für alle Fragen

  const [searchParams] = useSearchParams();
  const huntId = searchParams.get("name");

  const { authFetch } = useContext(AuthContext);

  const [questions, setQuestions] = useState([]);

  useEffect(() => {
    if (!huntId) return;

    async function loadHunt() {
      try {
        const [huntRes, cluesRes] = await Promise.all([
          authFetch(`http://localhost:8000/hunts/${huntId}`),
          authFetch(`http://localhost:8000/hunts/${huntId}/clues`),
        ]);

        if (!huntRes.ok || !cluesRes.ok) {
          throw new Error("Failed to fetch hunt or clues");
        }


        const hunt = await huntRes.json();
        const clues = await cluesRes.json();
        console.log(" hunt:", hunt);
        console.log(" clues:", clues);

        setCreatorName(hunt.description || "");
        setHuntLocation(hunt.place_to_play || "");
        setStartPoint(hunt.start_point || "");
        setHuntNameState(hunt.name || "");

        setQuestions(clues.map(clue => ({
          id:     clue.id,
          text:   clue.description   ?? "",
          answer: clue.correct_answer ?? "",
          order: clue.clue_order ?? 0,
          open:   false
        })));
      } catch (err) {
        console.error("Failed to load hunt or clues", err);
      }
    }

    loadHunt();
  }, [huntId, authFetch]);
  

  async function syncOrder(updatedQuestions) {
    await Promise.all(
        updatedQuestions.map(({ id, order }) =>
          authFetch(
            `http://localhost:8000/hunts/${huntId}/clues/${id}`,
            {
              method: "PATCH",
              headers: { "Content-Type": "application/json" },
              body: JSON.stringify({ clue_order: order }),
            }
          )
        )
      );
  }

  // Neue Frage hinzufügen
  const handleAddQuestion = () => {
    async function addQuestion() {
      try {
        const nextOrder = questions.length + 1;
        const res = await authFetch(
          `http://localhost:8000/hunts/${huntId}/clues`,
          { method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ clue_order: nextOrder }),
          }
        );
        if (!res.ok) {
          const text = await res.text();
          throw new Error(text || "Failed to create clue");
        }

        const payload = await res.json();
        console.log("New clue created:", payload);
        const newClueId = payload.id;
        setQuestions([...questions, { text: "", answer: "", id: newClueId, order: nextOrder, open: false }]);
      } catch (err) {
        console.error("Failed to add question", err);
      }
    }

    addQuestion();
  };

  // Frage öffnen/schließen
  const handleToggleQuestion = (idx) => {
    setQuestions(questions =>
      questions.map((q, i) =>
        i === idx ? { ...q, open: !q.open } : q
      )
    );
  };

  // Frage entfernen
  const handleRemoveQuestion = async (clueId) => {
    if (!window.confirm("Delete this question?")) return;
    try {
      const res = await authFetch(
        `http://localhost:8000/hunts/${huntId}/clues/${clueId}`,
        { method: "DELETE" }
      );
      if (!res.ok) throw new Error("Delete failed");

      const filtered = questions.filter((q) => q.id !== clueId);

      const reindexed = filtered.map((q, idx) => ({
        ...q,
        order: idx + 1,
      }));
      setQuestions(reindexed);

      await syncOrder(reindexed);
    } catch (err) {
      console.error("Failed to delete question", err);
      alert("Could not remove question.");
    }
  };

  const handleEditQuestion = (idx) => {

    const question = questions[idx];
    if (!question) return;

    // Navigate to EditQuestion page with huntId and clueId
    navigate(`/EditQuestion?hunt=${huntId}&clue=${question.id}`);
  };

  // Funktion zum Tauschen der Reihenfolge
  const handleDragEnd = (result) => {
    if (!result.destination) return;
    const newQuestions = Array.from(questions);
    const [moved] = newQuestions.splice(result.source.index, 1);
    newQuestions.splice(result.destination.index, 0, moved);
    const reordered = newQuestions.map((q, idx) => ({
    ...q,
    order: idx + 1,           
    }));

    setQuestions(reordered);

    syncOrder(reordered).catch(err => {
      console.error("Failed to sync order", err);
    });
  };

  const handleSaveAndExit = () => {

    async function saveAndExit() {

      const res = await authFetch(`http://localhost:8000/hunts/${huntId}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          description:   creatorName,
          place_to_play: huntLocation,
          start_point:   startPoint,
          is_active:     true
        })
      });
      if (!res.ok) throw new Error('Failed to save hunt');
      
      try {
      await Promise.all(
        questions.map((q) =>
          authFetch(
            `http://localhost:8000/hunts/${huntId}/clues/${q.id}`,
            {
              method: "PATCH",
              headers: { "Content-Type": "application/json" },
              body: JSON.stringify({ clue_order: q.order }),
            }
          )
        )
      );
      alert(t("hunt_saved_successfully"));
      // navigate(-1);
      } catch (err) {
        console.error("Failed to save question order", err);
        alert(t("could_not_save_order"));
      }

    }

    saveAndExit();
    
  };

  return (
    <div className="edit-hunt-container">
      <h1 className="heading">{huntNameState}</h1>

      {/* Angaben Reiter */}
      <div className={`accordion-section ${showDetails ? "open" : ""}`}>
        <button
          className="accordion-toggle"
          onClick={() => setShowDetails((prev) => !prev)}
        >
          Angaben {showDetails ? "▲" : "▼"}
        </button>
        {showDetails && (
          <div className="accordion-content">
          {/*             <label>
              Hunt Name:
              <input
                className="EditHunt-input"
                type="text"
                value={huntNameState}
                onChange={(e) => setHuntNameState(e.target.value)}
                placeholder="Hunt Name eingeben"
              />
            </label> */}
            <label>
              Kurzinfo:
              <input
                className="EditHunt-input"
                type="text"
                value={creatorName}
                onChange={(e) => setCreatorName(e.target.value)}
                placeholder="Kurzinfo eingeben"
              />
            </label>
            <label>
              Ort des Spieles:
              <input
                className="EditHunt-input"
                type="text"
                value={huntLocation}
                onChange={(e) => setHuntLocation(e.target.value)}
                placeholder="Ort des Spieles"
              />
            </label>
            <label>
              Startpunkt:
              <input
                className="EditHunt-input"
                type="text"
                value={startPoint}
                onChange={(e) => setStartPoint(e.target.value)}
                placeholder="Startpunkt"
              />
            </label>

          </div>
        )}
      </div>

      {/* Fragen Reiter */}
      <div className={`accordion-section ${showQuestions ? "open" : ""}`}>
        <button
          className="accordion-toggle"
          onClick={() => setShowQuestions((prev) => !prev)}
        >
          Fragen {showQuestions ? "▲" : "▼"}
        </button>
        {showQuestions && (
          <div className="accordion-content">
            <DragDropContext onDragEnd={handleDragEnd}>
              <Droppable droppableId="questions">
                {(provided) => (
                  <div ref={provided.innerRef} {...provided.droppableProps}>
                    {questions.map((question, idx) => (
                      <Draggable key={idx} draggableId={String(idx)} index={idx}>
                        {(provided, snapshot) => (
                          <div
                            className={`question-widget${snapshot.isDragging ? " dragging" : ""}`}
                            ref={provided.innerRef}
                            {...provided.draggableProps}
                            {...provided.dragHandleProps}
                          >
                            <div>{question.order}</div>
                            <button
                              className={`question-toggle ${question.open ? "corners" : ""}`}
                              onClick={() => handleToggleQuestion(idx)}
                            >
                              Frage {idx + 1} {question.open ? "▲" : "▼"}
                            </button>
                            {question.open && (
                              <div className="question-content">
                                <div className="drag-icon">⋮⋮</div>
                                <label>
                                  Frage:<br />
                                  <label>
                                    {question.text || "question text"}<br />
                                  </label>
                                </label>
                                <label>
                                  Antwort:<br />
                                  <label>
                                    {question.answer || "answer text"}<br />
                                  </label>
                                </label>
                                <div className="question-actions">
                                  <button
                                    className="main-button main-button-orange"
                                    onClick={() => handleEditQuestion(idx)}
                                  >
                                    Edit
                                  </button>
                                  <button
                                    className="main-button main-button-red"
                                    onClick={() => handleRemoveQuestion(questions[idx].id)}
                                  >
                                    Remove
                                  </button>
                                </div>
                              </div>
                            )}
                          </div>
                        )}
                      </Draggable>
                    ))}
                    {provided.placeholder}
                  </div>
                )}
              </Droppable>
            </DragDropContext>
            <button className="main-button main-button-blue butt" onClick={handleAddQuestion}>
              Frage hinzufügen
            </button>
          </div>
        )}
      </div>

      {/* Save and Exit Button */}
      <div className="save-exit-container">
        <button className="main-button main-button-green" onClick={handleSaveAndExit}>
          {t("save_and_exit")}
        </button>
      </div>
    </div>
  );
}
