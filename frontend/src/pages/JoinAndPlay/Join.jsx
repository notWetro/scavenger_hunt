import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";
import "./Join.css";
import { AuthContext } from "../../AuthContext";

export default function Join() {
  const [huntCode, setHuntCode] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();
  const { t } = useTranslation();
  const { authFetch } = React.useContext(AuthContext);


  const handleJoin = async (e) => {
    e.preventDefault();
    
    try {
      const res = await authFetch(
        `/hunts/${huntCode}/join`,
        { method: "POST" }
      );

      if (!res.ok) {
      const errorData = await res.json(); 
      const errorDetail = errorData.detail || t("join_failed"); 
      setError(errorDetail); 
      return;
    }

      const data = await res.json();
      console.log(data);
      navigate(`/StartHunt/${huntCode}`, { state: data });
    } catch (err) {
      console.error(err);
      setError(t("join_failed"));
    }
    
  };

  return (
    <div className="join-container">
      <h1 className="heading">{t("join_hunt")}</h1>
      <form className="join-form" onSubmit={handleJoin}>
        <input
          type="text"
          placeholder={t("enter_hunt_code")}
          value={huntCode}
          onChange={(e) => setHuntCode(e.target.value)}
          required
          className="join-input"
        />
        {error && (
          <div className="error-message-hunt-code">
            {error}
          </div>
        )}
        <button type="submit" className="main-button main-button-green">
          {t("join")}
        </button>
        <button
          type="button"
          className="main-button"
          onClick={() => navigate(-1)}
        >
          {t("back")}
        </button>
      </form>
    </div>
  );
}
