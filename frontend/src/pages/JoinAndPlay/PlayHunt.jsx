import React, { useState, useContext, useEffect } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate, useParams } from "react-router-dom";
import { AuthContext } from "../../AuthContext";
import "./PlayHunt.css";

export default function PlayHunt() {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const { huntCode } = useParams();
  const { authFetch } = useContext(AuthContext);

  const [hunt, setHunt] = useState(null);
  const [questions, setQuestions] = useState([]);
  const [currentQuestionIndex, setCurrentQuestionIndex] = useState(0);
  const [userAnswer, setUserAnswer] = useState("");
  const [showHint, setShowHint] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  const [gameCompleted, setGameCompleted] = useState(false);

  useEffect(() => {
    if (!huntCode) {
      navigate("/");
      return;
    }

    async function loadHunt() {
      try {
        setIsLoading(true);
        const [huntRes, cluesRes, nextClueRes] = await Promise.all([
          authFetch(`/hunts/by-code/${huntCode}`),
          authFetch(`/hunts/by-code/${huntCode}/clues`),
          authFetch(`/hunts/by-code/${huntCode}/current-clue`),
        ]);

        if (!huntRes.ok || !cluesRes.ok) {
          throw new Error("Failed to fetch hunt or clues");
        }

        const huntData = await huntRes.json();
        const cluesData = await cluesRes.json();
        console.log("Hunt data:", huntData);

        setHunt(huntData);
        setQuestions(cluesData);
        console.log("Hunt loaded:", cluesData);
        const nextClue = await nextClueRes.json();
        console.log(nextClue);
        if (nextClue) {
          console.log("Next clue found:", nextClue.current_clue_id);
          const idx = cluesData.findIndex(
            (clue) => clue.id === nextClue.current_clue_id,
          );
          setCurrentQuestionIndex(idx >= 0 ? idx : 0);
          console.log(idx);
        }
      } catch (err) {
        console.error("Failed to load hunt", err);
        alert("Fehler beim Laden der Schnitzeljagd");
        navigate("/");
      } finally {
        setIsLoading(false);
      }
    }

    loadHunt();
  }, [huntCode, authFetch, navigate]);

  async function saveClueProgress(huntCode, clueId) {
    const res = await authFetch(`/hunts/by-code/${huntCode}/progress`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ clue_id: clueId }),
    });

    if (!res.ok) {
      const text = await res.text();
      throw new Error(text || "Failed to record progress");
    }
  }

  const handleAnswer = () => {
    if (!userAnswer.trim()) {
      alert("Bitte geben Sie eine Antwort ein");
      return;
    }

    const currentQuestion = questions[currentQuestionIndex];
    const isCorrect =
      userAnswer.toLowerCase().trim() ===
      currentQuestion.correct_answer.toLowerCase().trim();

    if (isCorrect) {
      // Richtige Antwort

      saveClueProgress(huntCode, currentQuestion.id);

      if (currentQuestionIndex + 1 < questions.length) {
        // Nächste Frage laden
        setCurrentQuestionIndex(currentQuestionIndex + 1);
        setUserAnswer("");
        setShowHint(false);
        // Wenn user angemeldet ist, kann hier ein Fortschritt gespeichert werden (HuntProgressNumber)
        alert("Richtig! Nächste Frage wird geladen.");
      } else {
        // Schnitzeljagd beendet
        setGameCompleted(true);
        alert(
          "Herzlichen Glückwunsch! Sie haben die Schnitzeljagd erfolgreich beendet!",
        );
      }
    } else {
      // Falsche Antwort
      alert(
        "Falsche Antwort. Versuchen Sie es erneut oder nutzen Sie den Hinweis.",
      );
    }
  };

  const handleHint = () => {
    setShowHint(true);
  };

  const handleBack = () => {
    if (currentQuestionIndex > 0) {
      if (
        window.confirm(
          "Möchten Sie wirklich zur vorherigen Frage zurückkehren? Ihre aktuelle Antwort geht verloren.",
        )
      ) {
        setCurrentQuestionIndex(currentQuestionIndex - 1);
        setUserAnswer("");
        setShowHint(false);
      }
    } else {
      alert("Dies ist die erste Frage. Sie können nicht zurückgehen.");
    }
  };

  const handleEnd = () => {
    if (window.confirm("Möchten Sie die Schnitzeljagd wirklich beenden?")) {
      navigate("/");
    }
  };

  const closeHintPopup = () => {
    setShowHint(false);
  };

  if (isLoading) {
    return <div className="play-hunt-container">Laden...</div>;
  }

  if (!hunt || questions.length === 0) {
    return <div className="play-hunt-container">Keine Fragen gefunden.</div>;
  }

  if (gameCompleted) {
    return (
      <div className="play-hunt-container">
        <h1>Schnitzeljagd beendet!</h1>
        <p>
          Herzlichen Glückwunsch! Sie haben alle Fragen erfolgreich beantwortet.
        </p>
        <button
          className="main-button main-button-green"
          onClick={() => navigate("/")}
        >
          Zurück zur Startseite
        </button>
      </div>
    );
  }

  const currentQuestion = questions[currentQuestionIndex];

  const renderQuestionReturn = () => {
    switch (currentQuestion.quesionType) {
      case "text":
        return <div></div>;
      case "Bild":
        return (
          <div>
            {currentQuestion.imageURL && (
              <img src={currentQuestion.imageURL} style={{ maxWidth: 200 }} />
            )}
          </div>
        );
      case "Audio":
      case "GPS":
        return (
          <MapComponent
            latitude={
              currentQuestion.questionGpsCoordinates.lat == ""
                ? 0
                : parseFloat(currentQuestion.questionGpsCoordinates.lat)
            }
            longitude={
              currentQuestion.questionGpsCoordinates.lng == ""
                ? 0
                : parseFloat(currentQuestion.questionGpsCoordinates.lng)
            }
            zoom={15}
            height="300px"
            popupText="Antwort Ort"
            className="map-container"
          />
        );
      default:
        return null;
    }
  };

  return (
    <div className="play-hunt-container">
      <h1>{hunt.name}</h1>

      <div className="progress-info">
        {t("Frage")} {currentQuestionIndex + 1} von {questions.length}
      </div>

      <div className="question-section">
        <div className="question-text">
          <h2>{currentQuestion.description}</h2>
        </div>
        {renderQuestionReturn()}
        <hr className="section-divider" /> {/* css Code in EditQuestion.css */}
        <div className="answer-section">
          <input
            type="text"
            className="answer-input"
            value={userAnswer}
            onChange={(e) => setUserAnswer(e.target.value)}
            placeholder="Ihre Antwort hier eingeben..."
            onKeyPress={(e) => {
              if (e.key === "Enter") {
                handleAnswer();
              }
            }}
          />

          <div className="button-group">
            <button
              className="main-button main-button-green"
              onClick={handleAnswer}
            >
              Antworten
            </button>

            <button
              className="main-button main-button-blue"
              onClick={handleHint}
            >
              Hinweis
            </button>

            <button className="main-button" onClick={handleBack}>
              Zurück
            </button>

            <button className="main-button main-button-red" onClick={handleEnd}>
              Beenden
            </button>
          </div>
        </div>
      </div>

      {/* Hinweis Popup */}
      {showHint && (
        <div className="popup-overlay" onClick={closeHintPopup}>
          <div className="popup" onClick={(e) => e.stopPropagation()}>
            <div className="hint-content">
              <h3>Hinweis</h3>
              <p>
                {currentQuestion.hint ||
                  "Kein Hinweis verfügbar für diese Frage."}
              </p>
              <button
                className="main-button main-button-gray"
                onClick={closeHintPopup}
              >
                Schließen
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
