import React, { useState,useEffect, useContext } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate, useLocation } from "react-router-dom";
import "./EditQuestion.css";
import { AuthContext } from "../../AuthContext";


export default function EditQuestion() {
  const { t } = useTranslation();
  const navigate = useNavigate();
  
  const {search} = useLocation();
  const params = new URLSearchParams(search);
  const huntId = params.get("hunt");
  const questionId = params.get("clue");
  const { authFetch } = useContext(AuthContext);


  const [question, setQuestion] = useState({ text: " ", answer: " " });

  useEffect(() => {
    if (!huntId || !questionId) {
      console.error("Hunt ID or Question ID is missing");
      console.log(huntId, questionId);
      return;
    }
    async function load() {
      try {
        console.log("Loading question data for", huntId, questionId);
        const res = await authFetch(
          `http://localhost:8000/hunts/${huntId}/clues/${questionId}`
        );
        console.log("Response status:", res.status);
        if (!res.ok) throw new Error();
        const data = await res.json();
        console.log("Question data loaded:", data);

        setQuestion({ text: data.description, answer: data.correct_answer });
      } catch {
      }
    }
    load();
  }, [huntId, questionId, authFetch]);


  const saveChange = async () => {
    try {
      const res = await authFetch(
        `http://localhost:8000/hunts/${huntId}/clues/${questionId}`,
        {
          method: "PATCH",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({description: question.text, correct_answer: question.answer}),
        }
      );
      if (!res.ok) throw new Error(await res.text());
      navigate(-1);
    } catch {
    }
  };

  return (
    <div className="edit-question-content">
      <label>
        Frage:
        <input
          type="text"
          value={question.text}
          onChange={(e) => setQuestion(f => ({ ...f, text: e.target.value }))}
          placeholder="Frage eingeben"
        />
      </label>
      <label>
        Antwort:
        <input
          type="text"
          value={question.answer}
          onChange={e => setQuestion(f => ({ ...f, answer: e.target.value }))}
          placeholder="Antwort eingeben"
        />
      </label>
      <div className="question-actions">
        <button
          className="save-question-btn"
          onClick={() => saveChange()}
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
