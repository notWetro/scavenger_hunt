import React, { useState, useEffect, useContext } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate, useLocation } from "react-router-dom";
import "./EditQuestion.css";
import MapComponent from "../../components/MapComponent.jsx";
import { AuthContext } from "../../AuthContext";
import { getCurrentLocation } from "../../utils/geolocation";

const API_BASE = import.meta.env.VITE_API_BASE;

export default function EditQuestion() {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const { search } = useLocation();
  const params = new URLSearchParams(search);
  const huntId = params.get("hunt");
  const questionId = params.get("clue");
  const { authFetch } = useContext(AuthContext);
  const [imageFile, setImageFile] = useState(null);
  const [previewUrl, setPreviewUrl] = useState("");
  const [previewHintUrl, setPreviewHintUrl] = useState("");

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
    answerGpsRadius: null,
    hintGpsCoordinates: { lat: "", lng: "" },
    multipleChoiceOptions: ["", "", ""],
    correctOptionIndex: 0, // for multiple choice
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

        console.log("Loaded question:", data);

        setQuestion((prev) => ({
          ...prev,
          text: data.description || "",
          hint: data.hint || "",
          answer: data.correct_answer || "",

          imageFile: data.image_url || null,
          audioFile: data.audio_url || null,
          questionGpsCoordinates: data.question_gps_coordinates || { lat: "", lng: "" },

          hintType: data.hint_type || "text",
          hintImageFile: data.hint_image_file || null,
          hintAudioFile: data.hint_audio_file || null,
          hintGpsCoordinates: data.hint_gps_coordinates || { lat: "", lng: "" },
          hintGpsRadius: data.hint_gps_radius || null,

          questionType: data.question_type || "text",
          answerType: data.answer_type || "text",
          answerGpsCoordinates: data.answer_gps_coordinates || { lat: "", lng: "" },
          answerGpsRadius: data.answer_gps_radius || null,
          multipleChoiceOptions: data.choices || ["", "", ""],
        }));
        setPreviewUrl(`${API_BASE}${data.image_url || ""}`);
        setPreviewHintUrl(`${API_BASE}${data.hint_image_file || ""}`);
        console.log("Question loaded successfully:", question);
      } catch (error) {
        console.error("Error loading question:", error);
      }
    }
    load();
  }, [huntId, questionId, authFetch]);

  const handleQuestionTypeChange = (type) => {
    setQuestion((prev) => ({ ...prev, questionType: type }));
  };

  const handleAnswerTypeChange = (type) => {
    setQuestion((prev) => ({ ...prev, answerType: type }));
  };

  const handleHintTypeChange = (type) => {
    setQuestion((prev) => ({ ...prev, hintType: type }));
  };

  const handleHintImageUpload = async (e) => {
    const file = e.target.files[0];
    if (!file) return alert("Pick a hint image first");

    setPreviewHintUrl(URL.createObjectURL(file));
    const form = new FormData();
    form.append("file", file);

    const res = await authFetch(
      `/hunts/${huntId}/clues/${questionId}/hint-image`,
      { method: "POST", body: form }
    );

    if (!res.ok) {
      const err = await res.text();
      return console.error("Hint upload failed:", err);
    }
    const { hint_image_url } = await res.json();
    setQuestion(q => ({ ...q, hintImageFile: hint_image_url }));
    setPreviewHintUrl(`${API_BASE}${hint_image_url}`);
    console.log("Hint image uploaded successfully:", hint_image_url);
  };


  const handleImageChange = e => {
    const file = e.target.files[0];
    console.log("Selected file:", file);
    setImageFile(file);
    setPreviewUrl(URL.createObjectURL(file));
  };

  const uploadQuestionImage = async () => {
    if (!imageFile) return alert("Choose an image first");
    const formData = new FormData();
    formData.append("file", imageFile);

    const res = await authFetch(
      `/hunts/${huntId}/clues/${questionId}/image`,
      { method: "POST", body: formData }
    );
    if (!res.ok) {
      const err = await res.text();
      return console.error("Upload failed:", err);
    }
    const { image_url } = await res.json();
    setQuestion((prev) => ({ ...prev, imageFile: image_url }));
    return image_url;
   
  };

  const handleImageUpload = (e) => {
    const file = e.target.files[0];
    setQuestion((prev) => ({ ...prev, imageFile: file }));
    console.log("Image file selected:", file);
  };

  const handleAudioUpload = (e) => {
    const file = e.target.files[0];
    setQuestion((prev) => ({ ...prev, audioFile: file }));
  };

  const handleGpsChange = (coordinateType, field, value) => {
    setQuestion((prev) => ({
      ...prev,
      [coordinateType]: { ...prev[coordinateType], [field]: value },
    }));
  };

  const handleMultipleChoiceChange = (index, value) => {
    const newOptions = [...question.multipleChoiceOptions];
    newOptions[index] = value;
    setQuestion((prev) => ({ ...prev, multipleChoiceOptions: newOptions }));
  };

  const addMultipleChoiceOption = () => {
    setQuestion((prev) => ({
      ...prev,
      multipleChoiceOptions: [...prev.multipleChoiceOptions, ""],
    }));
  };

  const removeMultipleChoiceOption = (index) => {
    if (question.multipleChoiceOptions.length > 2) {
      const newOptions = question.multipleChoiceOptions.filter(
        (_, i) => i !== index,
      );
      setQuestion((prev) => ({
        ...prev,
        multipleChoiceOptions: newOptions,
        correctOptionIndex:
          prev.correctOptionIndex >= index
            ? Math.max(0, prev.correctOptionIndex - 1)
            : prev.correctOptionIndex,
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
  };

  const saveChange = async () => {
    clearOtherFields();
    try {
      let finalQuestionImageUrl = question.imageFile;  
      if (imageFile instanceof File) {
        finalQuestionImageUrl = await uploadQuestionImage();
      }
      const res = await authFetch(`/hunts/${huntId}/clues/${questionId}`, {
        method: "PATCH",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          description: question.text,
          correct_answer: question.answer,
          hint: question.hint,
          question_type: question.questionType,
          answer_type: question.answerType,
          hint_type: question.hintType,
          image_url: finalQuestionImageUrl,
          audio_url: question.audioFile,
          question_gps_coordinates: question.questionGpsCoordinates,
          answer_gps_coordinates: question.answerGpsCoordinates,
          answer_gps_radius: question.answerGpsRadius || null,
          hint_image_url: question.hintImageFile || null,
          hint_audio_url: question.hintAudioFile || null,
          hint_gps_coordinates: question.hintGpsCoordinates,
          hint_gps_radius: question.hintGpsRadius || null,
          choices: question.multipleChoiceOptions,
        }),
      });

      console.log("uploaded question:", {
        description: question.text,
        image_url: finalQuestionImageUrl,
        audio_url: question.audioFile,
        question_gps_coordinates: question.questionGpsCoordinates,
        answer_gps_coordinates: question.answerGpsCoordinates,
        answer_gps_radius: question.answerGpsRadius || null,
        hint_image_url: question.hintImageFile || null,
        hint_audio_url: question.hintAudioFile || null,
        hint_gps_coordinates: question.hintGpsCoordinates,
        hint_gps_radius: question.hintGpsRadius || null,
        choices: question.multipleChoiceOptions,
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
          <div>
            <h2>Edit Clue #{questionId}</h2>
            <input type="file" onChange={handleImageChange} />
            {previewUrl && <img src={previewUrl} style={{maxWidth:200}} />}
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
                onChange={(e) =>
                  handleGpsChange(
                    "questionGpsCoordinates",
                    "lat",
                    e.target.value,
                  )
                }
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
                onChange={(e) =>
                  handleGpsChange(
                    "questionGpsCoordinates",
                    "lng",
                    e.target.value,
                  )
                }
                placeholder="z.B. 13.4050"
                className="EditQuestion-input"
              />
            </div>
            <div>
              <h4>Position</h4>
              <MapComponent
                latitude={
                  question.questionGpsCoordinates.lat == ""
                    ? 0
                    : parseFloat(question.questionGpsCoordinates.lat)
                }
                longitude={
                  question.questionGpsCoordinates.lng == ""
                    ? 0
                    : parseFloat(question.questionGpsCoordinates.lng)
                }
                zoom={15}
                height="300px"
                popupText="Antwort Ort"
                className="map-container"
              />
              <button
                className="main-button main-button-blue"
                onClick={async () => {
                  const position = await getCurrentLocation();
                  console.log(position);
                  if (position) {
                    setQuestion({
                      ...question,
                      questionGpsCoordinates: {
                        lat: String(position.latitude),
                        lng: String(position.longitude),
                      },
                    });
                  }
                }}
              >
                Get your Position
              </button>
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
            onChange={(e) =>
              setQuestion((prev) => ({ ...prev, answer: e.target.value }))
            }
            placeholder="Antwort eingeben"
          />
        );
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
                  onChange={() =>
                    setQuestion((prev) => ({
                      ...prev,
                      correctOptionIndex: index,
                    }))
                  }
                />
                <input
                  type="text"
                  value={option}
                  onChange={(e) =>
                    handleMultipleChoiceChange(index, e.target.value)
                  }
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
        );
      case "gps":
        return (
          <div className="gps-input">
            <div className="gps-field">
              <label>Breitengrad:</label>
              <input
                type="number"
                step="any"
                value={question.answerGpsCoordinates.lat}
                onChange={(e) =>
                  handleGpsChange("answerGpsCoordinates", "lat", e.target.value)
                }
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
                onChange={(e) =>
                  handleGpsChange("answerGpsCoordinates", "lng", e.target.value)
                }
                placeholder="z.B. 13.4050"
                className="EditQuestion-input"
              />
            </div>
            <div>
              <h4>Position</h4>
              <MapComponent
                latitude={
                  question.answerGpsCoordinates.lat == ""
                    ? 0
                    : parseInt(question.answerGpsCoordinates.lat)
                }
                longitude={
                  question.answerGpsCoordinates.lng == ""
                    ? 0
                    : parseInt(question.answerGpsCoordinates.lng)
                }
                zoom={15}
                height="300px"
                popupText="Antwort Ort"
                className="map-container"
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
            onChange={(e) =>
              setQuestion((prev) => ({ ...prev, hint: e.target.value }))
            }
            placeholder="Hinweis eingeben"
          />
        );
      case "image":
        return (
          <div>
            <h2>Hint Clue #{questionId}</h2>
            <input type="file" onChange={handleHintImageUpload} />
            {previewHintUrl && <img src={previewHintUrl} style={{maxWidth:200}} />}
            <div>{previewHintUrl}</div>
          </div>
        );
      case "audio":
        return (
          <div className="media-upload">
            <input
              type="file"
              accept="audio/*"
              onChange={(e) =>
                setQuestion((prev) => ({
                  ...prev,
                  hintAudioFile: e.target.files[0],
                }))
              }
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
                onChange={(e) =>
                  handleGpsChange("hintGpsCoordinates", "lat", e.target.value)
                }
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
                onChange={(e) =>
                  handleGpsChange("hintGpsCoordinates", "lng", e.target.value)
                }
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
          {/* <option value="audio">Audio</option> */}
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
          onChange={(e) =>
            setQuestion((prev) => ({ ...prev, text: e.target.value }))
          }
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
          {question.answerType === "multiple_choice"
            ? "Antwortoptionen:"
            : "Antwort:"}
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
          <option value="image">Bild</option>
          {/* <option value="audio">Audio</option> */}
          <option value="gps">GPS</option>
        </select>
        <div className="hint-content">
          <label htmlFor="hint-input">Hinweis (optional):</label>
          {renderHintContent()}
        </div>
      </div>

      {/* Aktionen Sektion */}
      <div className="question-actions">
        <button className="main-button main-button-green" onClick={saveChange}>
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
