import React, { useState } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";
import { DragDropContext, Droppable, Draggable } from "@hello-pangea/dnd";
import "./EditHunt.css";

export default function EditHunt({ huntName }) {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const [showDetails, setShowDetails] = useState(true);
  const [showQuestions, setShowQuestions] = useState(false);
  const [creatorName, setCreatorName] = useState("");
  const [huntLocation, setHuntLocation] = useState("");
  const [startPoint, setStartPoint] = useState("");
  // Array für alle Fragen
  const [questions, setQuestions] = useState([
    { text: "", answer: "", open: false }
  ]);

  // Neue Frage hinzufügen
  const handleAddQuestion = () => {
    setQuestions([...questions, { text: "", answer: "", open: false }]);
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
  const handleRemoveQuestion = (idx) => {
    setQuestions(questions => questions.filter((_, i) => i !== idx));
  };

  const handleEditQuestion = (idx) => {
    navigate("/EditQuestion");
  };

  // Funktion zum Tauschen der Reihenfolge
  const handleDragEnd = (result) => {
    if (!result.destination) return;
    const newQuestions = Array.from(questions);
    const [moved] = newQuestions.splice(result.source.index, 1);
    newQuestions.splice(result.destination.index, 0, moved);
    setQuestions(newQuestions);
  };

  const handleSaveAndExit = () => {
    // Speichern der Änderungen im Backend

    alert(t("hunt_saved_successfully")); // Alert für Rückmeldung
    navigate(-1);
  };

  return (
    <div className="edit-hunt-container">
      <h1 className="heading">huntName</h1>

      {/* Angaben Reiter */}
      <div className="accordion-section">
        <button
          className="accordion-toggle"
          onClick={() => setShowDetails((prev) => !prev)}
        >
          Angaben {showDetails ? "▲" : "▼"}
        </button>
        {showDetails && (
          <div className="accordion-content">
            <label>
              Name des Erstellers:
              <input
                type="text"
                value={creatorName}
                onChange={(e) => setCreatorName(e.target.value)}
                placeholder="Dein Name"
              />
            </label>
            <label>
              Ort des Spieles:
              <input
                type="text"
                value={huntLocation}
                onChange={(e) => setHuntLocation(e.target.value)}
                placeholder="Ort des Spieles"
              />
            </label>
            <label>
              Startpunkt:
              <input
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
      <div className="accordion-section">
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
                            <button
                              className="question-toggle"
                              onClick={() => handleToggleQuestion(idx)}
                            >
                              Frage {idx + 1} {question.open ? "▲" : "▼"}
                            </button>
                            {question.open && (
                              <div className="question-content">
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
                                    onClick={() => handleRemoveQuestion(idx)}
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
            <button className="main-button main-button-blue" onClick={handleAddQuestion}>
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
