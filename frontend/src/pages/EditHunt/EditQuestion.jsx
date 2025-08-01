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
    hint: "",
    questionType: "text", // text, image, audio, gps
    answerType: "text", // text, multiple_choice, gps
    hintType: "text", // text, image, audio, gps
    imageFile: null,
    audioFile: null,
    hintImageFile: null,
    hintAudioFile: null,
    questionGpsCoordinates: { lat: "", lng: "" },
    answerGpsCoordinates: { lat: "", lng: "" },
    hintGpsCoordinates: { lat: "", lng: "" },
    multipleChoiceOptions: ["", "", ""],
    correctOptionIndex: 0 // for multiple choice
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
          hint: data.hint || "",
          // Hier können Sie zusätzliche Felder aus der Datenbank laden
          questionType: data.question_type || "text",
          answerType: data.answer_type || "text",
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

  const handleHintTypeChange = (type) => {
    setQuestion(prev => ({ ...prev, hintType: type }));
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

  const clearOtherFields = () => {
    switch (question.questionType) {
      case "text":
        question.imageFile = null;
        question.audioFile = null;
        question.questionGpsCoordinates = { lat: "", lng: "" };
        break;
      case "image":
        question.audioFile = null;
        question.questionGpsCoordinates = { lat: "", lng: "" };
        break;
      case "audio":
        question.imageFile = null;
        question.questionGpsCoordinates = { lat: "", lng: "" };
        break;
      case "gps":
        question.imageFile = null;
        question.audioFile = null;
        break;
      default:
        break;
    }
    switch (question.answerType) {
      case "text":
        question.multipleChoiceOptions = [];
        question.answerGpsCoordinates = { lat: "", lng: "" };
        break;
      case "multiple_choice":
        question.answer = "";
        question.answerGpsCoordinates = { lat: "", lng: "" };
        break;
      case "gps":
        question.answer = "";
        question.multipleChoiceOptions = [];
        break;
      default:
        break;
    }
    switch (question.hintType) {
      case "text":
        question.hintImageFile = null;
        question.hintAudioFile = null;
        question.hintGpsCoordinates = { lat: "", lng: "" };
        break;
      case "image":
        question.hint = "";
        question.hintAudioFile = null;
        question.hintGpsCoordinates = { lat: "", lng: "" };
        break;
      case "audio":
        question.hint = "";
        question.hintImageFile = null;
        question.hintGpsCoordinates = { lat: "", lng: "" };
        break;
      case "gps":
        question.hint = "";
        question.hintImageFile = null;
        question.hintAudioFile = null;
        break;
      default:
        break;
    }
  }

  const saveChange = async () => {
    clearOtherFields();
    try {
      const res = await authFetch(`/hunts/${huntId}/clues/${questionId}`, {
        method: "PATCH",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(
          {
            description: question.text,
            correct_answer: question.answer,
            question_type: question.questionType,
            answer_type: question.answerType,
            //hint_type: question.hintType,
            hint: question.hint,
            image_url: question.imageFile ? question.imageFile.name : null,
            audio_url: question.audioFile ? question.audioFile.name : null,
            expected_gps: toString(question.answerGpsCoordinates),
            //hint_gps_coordinates: question.hintGpsCoordinates,
          }),
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
                value={question.questionGpsCoordinates.lat}
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
                value={question.questionGpsCoordinates.lng}
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
                value={question.answerGpsCoordinates.lat}
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
                value={question.answerGpsCoordinates.lng}
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

  const renderHintContent = () => {
    switch (question.hintType) {
      case "text":
        return (
          <input
            type="text"
            className="EditQuestion-input"
            value={question.hint}
            onChange={(e) => setQuestion(prev => ({ ...prev, hint: e.target.value }))}
            placeholder="Hinweis eingeben"
          />
        );
      case "image":
        return (
          <div className="media-upload">
            <input
              type="file"
              accept="image/*"
              onChange={(e) => setQuestion(prev => ({ ...prev, hintImageFile: e.target.files[0] }))}
              className="file-input"
            />
            {question.hintImageFile && (
              <div className="file-preview">
                <p>Ausgewählte Datei: {question.hintImageFile.name}</p>
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
              onChange={(e) => setQuestion(prev => ({ ...prev, hintAudioFile: e.target.files[0] }))}
              className="file-input"
            />
            {question.hintAudioFile && (
              <div className="file-preview">
                <p>Ausgewählte Datei: {question.hintAudioFile.name}</p>
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
                value={question.hintGpsCoordinates.lat}
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
                value={question.hintGpsCoordinates.lng}
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
          {/* <option value="image">Bild</option> */}
          {/* <option value="audio">Audio</option> */}
          {/* <option value="gps">GPS</option> */}
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
          {/* <option value="multiple_choice">Multiple Choice</option> */}
          {/* <option value="gps">GPS</option> */}
        </select>
      </div>

      <div className="input-group">
        <label htmlFor="answer-input">
          {question.answerType === "multiple_choice" ? "Antwortoptionen:" : "Antwort:"}
        </label>
        {renderAnswerContent()}
      </div>

      <hr className="section-divider" />

      {/* Hinweis Sektion */}
      <div className="input-group">
        <label htmlFor="hint-type">Hinweis Typ:</label>
        <select
          id="hint-type"
          value={question.hintType}
          onChange={(e) => handleHintTypeChange(e.target.value)}
          className="type-dropdown"
        >
          <option value="text">Text</option>
          {/* <option value="image">Bild</option> */}
          {/* <option value="audio">Audio</option> */}
          {/* <option value="gps">GPS</option> */}
        </select>
        <div className="hint-content">
          <label htmlFor="hint-input">Hinweis (optional):</label>
          {renderHintContent()}
        </div>
      </div>

      {/* Aktionen Sektion */}
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
