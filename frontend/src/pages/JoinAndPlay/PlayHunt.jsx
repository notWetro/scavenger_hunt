import React, { useState, useContext, useEffect } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate, useParams } from "react-router-dom";
import { AuthContext } from "../../AuthContext";
import MapComponent from "../../components/MapComponent.jsx";
import { getCurrentLocation } from "../../utils/geolocation";
import { getDistanceFromLatLonInMeters } from "../../utils/distance";
import usePopup from "../../components/usePopup";
import Popup from "../../components/Popup";
import "./PlayHunt.css";

const API_BASE = import.meta.env.VITE_API_BASE;

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
  const [userLocation, setUserLocation] = useState({ lat: null, lng: null });
  const { popup, showAlert, showConfirm, handleClose, handleConfirm } =
    usePopup();

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
        await showAlert("Fehler beim Laden der Schnitzeljagd");
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
    const currentQuestion = questions[currentQuestionIndex];

    let isCorrect = false;

    if (currentQuestion.answer_type === "gps") {
      if (!userAnswer.lat || !userAnswer.lng) {
        showAlert("Bitte holen Sie Ihre aktuelle Position ein.");
        return;
      }

      const correctCoords = currentQuestion.answer_gps_coordinates;
      const tolerance = parseFloat(currentQuestion.answer_gps_radius); // in Metern

      const distance = getDistanceFromLatLonInMeters(
        userAnswer.lat,
        userAnswer.lng,
        parseFloat(correctCoords.lat),
        parseFloat(correctCoords.lng),
      );

      console.log("Distanz zur richtigen Position:", distance, "Meter");

      isCorrect = distance <= tolerance;
    } else {
      // TEXT & MULTIPLE CHOICE
      if (!userAnswer.trim()) {
        showAlert("Bitte geben Sie eine Antwort ein");
        return;
      }

      isCorrect =
        userAnswer.toLowerCase().trim() ===
        currentQuestion.correct_answer.toLowerCase().trim();
    }

    if (isCorrect) {
      // Richtige Antwort
      saveClueProgress(huntCode, currentQuestion.id);

      if (currentQuestionIndex + 1 < questions.length) {
        setCurrentQuestionIndex(currentQuestionIndex + 1);
        setUserAnswer("");
        setShowHint(false);
        showAlert("Richtig! Nächste Frage wird geladen."); // Eventuell weglassen
      } else {
        setGameCompleted(true);
      }
    } else {
      showAlert(
        currentQuestion.answer_type === "gps"
          ? "Sie sind nicht nah genug am Zielort. Versuchen Sie es erneut."
          : "Falsche Antwort. Versuchen Sie es erneut oder nutzen Sie den Hinweis.",
      );
    }
  };

  const handleHint = () => {
    setShowHint(true);
  };

  const handleBack = async () => {
    if (currentQuestionIndex > 0) {
      if (
        await showConfirm(
          "Möchten Sie wirklich zur vorherigen Frage zurückkehren? Ihre aktuelle Antwort geht verloren.",
        )
      ) {
        setCurrentQuestionIndex(currentQuestionIndex - 1);
        setUserAnswer("");
        setShowHint(false);
      }
    } else {
      showAlert("Dies ist die erste Frage. Sie können nicht zurückgehen.");
    }
  };

  const handleEnd = async () => {
    if (await showConfirm("Möchten Sie die Schnitzeljagd wirklich beenden?")) {
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
    switch (currentQuestion.question_type) {
      case "text":
        return <div></div>;
      case "image":
        console.log(currentQuestion.image_url);
        return (
          <div>
            <img
              src={`${API_BASE}${currentQuestion.image_url}`}
              style={{ maxWidth: 200 }}
            />
          </div>
        );
      case "audio":
        return (
          <div>
            <audio
              controls
              src={`${API_BASE}${currentQuestion.audio_url}`}
              style={{ width: 300 }}
            >
              Your browser does not support the audio element.
            </audio>
          </div>
        );
      case "gps":
        return (
          <MapComponent
            latitude={
              currentQuestion.question_gps_coordinates.lat == ""
                ? 0
                : parseFloat(currentQuestion.question_gps_coordinates.lat)
            }
            longitude={
              currentQuestion.question_gps_coordinates.lng == ""
                ? 0
                : parseFloat(currentQuestion.question_gps_coordinates.lng)
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

  const renderAnswerReturn = () => {
    switch (currentQuestion.answer_type) {
      case "text":
        return (
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
          </div>
        );
      case "multiple_choice":
        return (
          <div className="answer-section">
            <div className="multiple-choice-options">
              {currentQuestion.choices.map((option, index) => (
                <div key={index} className="radio-option">
                  <input
                    type="radio"
                    id={`option-${index}`}
                    name="multiple-choice-answer"
                    value={option}
                    checked={userAnswer === option}
                    onChange={(e) => setUserAnswer(e.target.value)}
                  />
                  <label htmlFor={`option-${index}`} className="radio-label">
                    {option}
                  </label>
                </div>
              ))}
            </div>
          </div>
        );
      case "gps":
        return (
          <div className="answer-section">
            <h4>Position:</h4>
            <MapComponent
              latitude={userAnswer.lat == null ? 0 : parseFloat(userAnswer.lat)}
              longitude={
                userAnswer.lng == null ? 0 : parseFloat(userAnswer.lng)
              }
              zoom={15}
              height="300px"
              popupText="Antwort Ort"
              className="map-container"
            />
            <button
              className="main-button main-button-blue"
              onClick={async () => {
                try {
                  const position = await getCurrentLocation(showAlert);
                  console.log(position);
                  if (position) {
                    setUserAnswer({
                      lat: position.latitude,
                      lng: position.longitude,
                    });
                  }
                } catch (error) {
                  // Error is already handled by showAlert in getCurrentLocation
                  console.error("Geolocation error:", error);
                }
              }}
            >
              Get your Position
            </button>
          </div>
        );
      default:
        return null;
    }
  };

  const renderHintReturn = () => {
    switch (currentQuestion.hint_type) {
      case "text":
        return (
          <div>
            <p>
              {currentQuestion.hint ||
                "Kein Hinweis verfügbar für diese Frage."}
            </p>
          </div>
        );
      case "image":
        return (
          <div>
            <p>
              {currentQuestion.hint_image_file
                ? ""
                : "Kein Hinweis verfügbar für diese Frage."}
            </p>
            {currentQuestion.hint_image_file && (
              <img
                src={`${API_BASE}${currentQuestion.hint_image_file}`}
                style={{ maxWidth: 200 }}
              />
            )}
          </div>
        );
      case "audio":
        return (
          <div>
            <audio
              controls
              src={`${API_BASE}${currentQuestion.hint_audio_file}`}
              style={{ width: 300 }}
            >
              Your browser does not support the audio element.
            </audio>
          </div>
        );
      case "gps":
        return (
          <div>
            <p>
              {currentQuestion.hint_gps_coordinates
                ? ""
                : "Kein Hinweis verfügbar für diese Frage."}
            </p>
            {currentQuestion.hint_gps_coordinates && (
              <MapComponent
                latitude={
                  parseFloat(currentQuestion.hint_gps_coordinates.lat) || 0
                }
                longitude={
                  parseFloat(currentQuestion.hint_gps_coordinates.lng) || 0
                }
                zoom={15}
                height="250px"
                popupText="Hinweis Ort"
                className="map-container"
              />
            )}
          </div>
        );
      default:
        return (
          <div>
            <p>
              {currentQuestion.hint ||
                "Kein Hinweis verfügbar für diese Frage."}
            </p>
          </div>
        );
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
        {renderAnswerReturn()}
        <div className="button-group">
          <button
            className="main-button main-button-green"
            onClick={handleAnswer}
          >
            Antworten
          </button>
          <button className="main-button main-button-blue" onClick={handleHint}>
            Hinweis
          </button>
          <hr className="section-divider" />{" "}
          {/* css Code in EditQuestion.css */}
          <button className="main-button" onClick={handleBack}>
            Zurück
          </button>
          <button className="main-button main-button-red" onClick={handleEnd}>
            Beenden
          </button>
        </div>
      </div>

      {/* Hinweis Popup */}
      {showHint && (
        <div className="popup-overlay" onClick={closeHintPopup}>
          <div className="popup" onClick={(e) => e.stopPropagation()}>
            <div className="hint-content">
              <h3>Hinweis</h3>
              {renderHintReturn()}
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
      <Popup
        open={popup.open}
        text={popup.text}
        confirmMode={popup.confirmMode}
        onClose={handleClose}
        onConfirm={handleConfirm}
      />
    </div>
  );
}
