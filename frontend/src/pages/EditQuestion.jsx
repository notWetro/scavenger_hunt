import React, { useState } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";
import "./EditQuestion.css";

export default function EditQuestion({ questionData, onEdit, onRemove }) {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const [question, setQuestion] = useState(
    questionData || { text: "", answer: "" }
  );

  const handleQuestionChange = (field, value) => {
    setQuestion((prev) => ({ ...prev, [field]: value }));
  };

  return (
    <div className="edit-question-content">
      <label>
        Frage:
        <input
          type="text"
          value={question.text}
          onChange={(e) => handleQuestionChange("text", e.target.value)}
          placeholder="Frage eingeben"
        />
      </label>
      <label>
        Antwort:
        <input
          type="text"
          value={question.answer}
          onChange={(e) => handleQuestionChange("answer", e.target.value)}
          placeholder="Antwort eingeben"
        />
      </label>
      <div className="question-actions">
        <button
          className="save-question-btn"
          onClick={() => onEdit && onEdit(question)}
        >
          save and back
        </button>
        <button
          className="main-button main-button-red"
          onClick={() => navigate(-1)}
        >
          cancel
        </button>
      </div>
    </div>
  );
}
