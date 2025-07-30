import React, { useState, useEffect, useContext } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate, useLocation } from "react-router-dom";
import "./EditQuestion.css";
import { AuthContext } from "../../AuthContext";

export default function EditQuestion() {
  const { t } = useTranslation();
  const navigate = useNavigate();
  
  const { search } = useLocation();
  const params = new URLSearchParams(search);
  const huntId = params.get("hunt");
  const questionId = params.get("clue");
  const { authFetch } = useContext(AuthContext);

  const [question, setQuestion] = useState({ 
    text: "", 
    answer: "",
    questionType: "text", // text, image, audio, gps
    answerType: "text", // text, multiple_choice, gps
    imageFile: null,
    audioFile: null,
    gpsCoordinates: { lat: "", lng: "" },
    multipleChoiceOptions: ["", "", "", ""],
    correctOptionIndex: 0
  });

  useEffect(() => {
    if (!huntId || !questionId) {
      console.error("Hunt ID or Question ID is missing");
      return;
    }
    async function load() {
      try {
        const res = await authFetch(`/hunts/${huntId}/clues/${questionId}`);
        if (!res.ok) throw new Error();
        const data = await res.json();

        setQuestion(prev => ({ 
          ...prev,
          text: data.description || "", 
          answer: data.correct_answer || "",
          // Hier können Sie zusätzliche Felder aus der Datenbank laden
          questionType: data.question_type || "text",
          answerType: data.answer_type || "text"
        }));
      } catch (error) {
        console.error("Error loading question:", error);
      }
    }
    load();
  }, [huntId, questionId, authFetch]);

  const handleQuestionTypeChange = (type) => {
    setQuestion(prev => ({ ...prev, questionType: type }));
  };

  const handleAnswerTypeChange = (type) => {
    setQuestion(prev => ({ ...prev, answerType: type }));
  };

  const handleImageUpload = (e) => {
    const file = e.target.files[0];
    setQuestion(prev => ({ ...prev, imageFile: file }));
  };

  const handleAudioUpload = (e) => {
    const file = e.target.files[0];
    setQuestion(prev => ({ ...prev, audioFile: file }));
  };

  const handleGpsChange = (field, value) => {
    setQuestion(prev => ({
      ...prev,
      gpsCoordinates: { ...prev.gpsCoordinates, [field]: value }
    }));
  };

  const handleMultipleChoiceChange = (index, value) => {
    const newOptions = [...question.multipleChoiceOptions];
    newOptions[index] = value;
    setQuestion(prev => ({ ...prev, multipleChoiceOptions: newOptions }));
  };

  const addMultipleChoiceOption = () => {
    setQuestion(prev => ({
      ...prev,
      multipleChoiceOptions: [...prev.multipleChoiceOptions, ""]
    }));
  };

  const removeMultipleChoiceOption = (index) => {
    if (question.multipleChoiceOptions.length > 2) {
      const newOptions = question.multipleChoiceOptions.filter((_, i) => i !== index);
      setQuestion(prev => ({
        ...prev,
        multipleChoiceOptions: newOptions,
        correctOptionIndex: prev.correctOptionIndex >= index ? Math.max(0, prev.correctOptionIndex - 1) : prev.correctOptionIndex
      }));
    }
  };

  const saveChange = async () => {
    try {
      const formData = new FormData();
      formData.append("description", question.text);
      formData.append("question_type", question.questionType);
      formData.append("answer_type", question.answerType);

      if (question.answerType === "multiple_choice") {
        formData.append("correct_answer", question.multipleChoiceOptions[question.correctOptionIndex]);
        formData.append("multiple_choice_options", JSON.stringify(question.multipleChoiceOptions));
      } else {
        formData.append("correct_answer", question.answer);
      }

      if (question.questionType === "image" && question.imageFile) {
        formData.append("image", question.imageFile);
      }

      if (question.questionType === "audio" && question.audioFile) {
        formData.append("audio", question.audioFile);
      }

      if (question.questionType === "gps") {
        formData.append("gps_coordinates", JSON.stringify(question.gpsCoordinates));
      }

      const res = await authFetch(`/hunts/${huntId}/clues/${questionId}`, {
        method: "PATCH",
        body: formData,
      });

      if (!res.ok) throw new Error(await res.text());
      navigate(-1);
    } catch (error) {
      console.error("Error saving question:", error);
      alert("Fehler beim Speichern der Frage");
    }
  };

  const renderQuestionContent = () => {
    switch (question.questionType) {
      case "image":
        return (
          <div className="media-upload">
            <input
              type="file"
              accept="image/*"
              onChange={handleImageUpload}
              className="file-input"
            />
            {question.imageFile && (
              <div className="file-preview">
                <p>Ausgewählte Datei: {question.imageFile.name}</p>
              </div>
            )}
          </div>
        );
      case "audio":
        return (
          <div className="media-upload">
            <input
              type="file"
              accept="audio/*"
              onChange={handleAudioUpload}
              className="file-input"
            />
            {question.audioFile && (
              <div className="file-preview">
                <p>Ausgewählte Datei: {question.audioFile.name}</p>
              </div>
            )}
          </div>
        );
      case "gps":
        return (
          <div className="gps-input">
            <div className="gps-field">
              <label>Breitengrad:</label>
              <input
                type="number"
                step="any"
                value={question.gpsCoordinates.lat}
                onChange={(e) => handleGpsChange("lat", e.target.value)}
                placeholder="z.B. 52.5200"
                className="EditQuestion-input"
              />
            </div>
            <div className="gps-field">
              <label>Längengrad:</label>
              <input
                type="number"
                step="any"
                value={question.gpsCoordinates.lng}
                onChange={(e) => handleGpsChange("lng", e.target.value)}
                placeholder="z.B. 13.4050"
                className="EditQuestion-input"
              />
            </div>
          </div>
        );
      default:
        return null;
    }
  };

  const renderAnswerContent = () => {
    switch (question.answerType) {
      case "text":
        return (
          <input
            id="answer-input"
            type="text"
            className="EditQuestion-input"
            value={question.answer}
            onChange={(e) => setQuestion(prev => ({ ...prev, answer: e.target.value }))}
            placeholder="Antwort eingeben"
          />
        )
      case "multiple_choice":
        return (
          <div className="multiple-choice-container">
            <label>Antwortoptionen:</label>
            {question.multipleChoiceOptions.map((option, index) => (
              <div key={index} className="multiple-choice-option">
                <input
                  type="radio"
                  name="correct-answer"
                  checked={question.correctOptionIndex === index}
                  onChange={() => setQuestion(prev => ({ ...prev, correctOptionIndex: index }))}
                />
                <input
                  type="text"
                  value={option}
                  onChange={(e) => handleMultipleChoiceChange(index, e.target.value)}
                  placeholder={`Option ${index + 1}`}
                  className="EditQuestion-input"
                />
                {question.multipleChoiceOptions.length > 2 && (
                  <button
                    type="button"
                    onClick={() => removeMultipleChoiceOption(index)}
                    className="remove-option-btn"
                  >
                    ✕
                  </button>
                )}
              </div>
            ))}
            <button
              type="button"
              onClick={addMultipleChoiceOption}
              className="add-option-btn"
            >
              + Option hinzufügen
            </button>
          </div>
        )
      case "gps":
        return (
          <div className="gps-input">
            <div className="gps-field">
              <label>Breitengrad:</label>
              <input
                type="number"
                step="any"
                value={question.gpsCoordinates.lat}
                onChange={(e) => handleGpsChange("lat", e.target.value)}
                placeholder="z.B. 52.5200"
                className="EditQuestion-input"
              />
            </div>
            <div className="gps-field">
              <label>Längengrad:</label>
              <input
                type="number"
                step="any"
                value={question.gpsCoordinates.lng}
                onChange={(e) => handleGpsChange("lng", e.target.value)}
                placeholder="z.B. 13.4050"
                className="EditQuestion-input"
              />
            </div>
          </div>
        );
      default:
        return null;
    }
  };


  return (
    <div className="edit-question-container">
      {/* Frage Sektion */}
      <div className="input-group">
        <label htmlFor="question-type">Fragetyp:</label>
        <select
          id="question-type"
          value={question.questionType}
          onChange={(e) => handleQuestionTypeChange(e.target.value)}
          className="type-dropdown"
        >
          <option value="text">Text</option>
          <option value="image">Bild</option>
          <option value="audio">Audio</option>
          <option value="gps">GPS</option>
        </select>
      </div>

      <div className="input-group">
        <label htmlFor="question-input">Frage:</label>
        <input
          id="question-input"
          type="text"
          className="EditQuestion-input"
          value={question.text}
          onChange={(e) => setQuestion(prev => ({ ...prev, text: e.target.value }))}
          placeholder="Frage eingeben"
        />
      </div>

      {renderQuestionContent()}

      <hr className="section-divider" />

      {/* Antwort Sektion */}
      <div className="input-group">
        <label htmlFor="answer-type">Antworttyp:</label>
        <select
          id="answer-type"
          value={question.answerType}
          onChange={(e) => handleAnswerTypeChange(e.target.value)}
          className="type-dropdown"
        >
          <option value="text">Text</option>
          <option value="multiple_choice">Multiple Choice</option>
          <option value="gps">GPS</option>
        </select>
      </div>

      <div className="input-group">
        <label htmlFor="answer-input">
          {question.answerType === "multiple_choice" ? "Antwortoptionen:" : "Antwort:"}
        </label>
        {renderAnswerContent()}
      </div>

      <div className="question-actions">
        <button
          className="main-button main-button-green"
          onClick={saveChange}
        >
          Speichern und zurück
        </button>
        <button
          className="main-button main-button-red"
          onClick={() => navigate(-1)}
        >
          Abbrechen
        </button>
      </div>
    </div>
  );
}
