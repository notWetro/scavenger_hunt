import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";
import "./Join.css";

export default function Join() {
  const [huntId, setHuntId] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();
  const { t } = useTranslation();

  // Mock-Daten: Liste existierender Hunt-IDs
  const mockHuntIds = ["12345", "hunt2025"];

  const handleJoin = (e) => {
    e.preventDefault();
    if (mockHuntIds.includes(huntId.trim())) {
      setError("");
      navigate(`/StartHunt/${huntId.trim()}`);
    } else {
      setError(t("hunt_id_not_exist"));
    }
  };

  return (
    <div className="join-container">
      <h1 className="heading">{t("join_hunt")}</h1>
      <form className="join-form" onSubmit={handleJoin}>
        <input
          type="text"
          placeholder={t("enter_hunt_id")}
          value={huntId}
          onChange={(e) => setHuntId(e.target.value)}
          required
          className="join-input"
        />
        {error && (
          <div className="error-message-hunt-ID">
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
