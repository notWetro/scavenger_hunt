import React, { useState, useContext } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";
import "./Home.css";
import { AuthContext } from "../AuthContext";
import config from "../../config.js";

export default function Home() {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const { authFetch } = useContext(AuthContext);
  const [showPopup, setShowPopup] = useState(false);
  const [huntName, setHuntName] = useState("");
  const [error, setError] = useState("");
  const [showInfo, setShowInfo] = useState(false);

  const handleCreate = async () => {
    setError("");
    if (!huntName.trim()) {
      setError(t("please_enter_name"));
      return;
    }

    try {
      const res = await authFetch("/create-hunt", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ name: huntName.trim() }),
      });

      if (!res.ok) {
        const err = await res.text();
        throw new Error(err || "Create hunt failed");
      }

      const data = await res.json();
      const newHuntId = data.hunt.id;

      setShowPopup(false);
      setHuntName("");
      navigate(`/EditHunt/${newHuntId}`);
    } catch (err) {
      console.error(err);
      if (err.message.includes("Unauthorized")) {
        setError(t("Error: You should be logged in to create a hunt"));
        <button
          className="main-button main-button-green"
          onClick={() => navigate("/login")}
        >
          {t("login")}
        </button>;
        return;
      }
      setError(t(err.message || "An error occurred while creating the hunt"));
    }
  };

  return (
    <div className="home-container">
      <h1 className="heading">{t("scavenger_hunt")}</h1>
      {config.linkOfImage && (
        <img
          src={config.linkOfImage}
          alt="Scavenger Hunt"
          className="home-image"
        />
      )}
      <button
          onClick={() => setShowInfo(!showInfo)}
          className="info-button"
        >
          i
        </button>
        {showInfo && (
          <div className="info-text">
            {config.infoText}
          </div>
        )}
      
      <div className="button-row">
        <button
          className="main-button main-button-green"
          onClick={() => navigate("/join")}
        >
          {t("join")}
        </button>
        <button className="main-button" onClick={() => setShowPopup(true)}>
          {t("create")}
        </button>
      </div>
      {showPopup && (
        <div className="popup-overlay">
          <div className="popup">
            <h2>{t("enter_hunt_name")}</h2>
            <input
              type="text"
              value={huntName}
              onChange={(e) => setHuntName(e.target.value)}
              placeholder={t("hunt_name")}
              autoFocus
            />
            {error && <p className="error">{error}</p>}
            <div className="popup-buttons">
              <button
                className="main-button main-button-green"
                onClick={handleCreate}
              >
                {t("create")}
              </button>
              <button
                className="main-button"
                onClick={() => {
                  setShowPopup(false);
                  setError("");
                  setHuntName("");
                }}
              >
                {t("cancel")}
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
