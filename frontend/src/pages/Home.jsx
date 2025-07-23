import React, { useState } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";
import "./Home.css";

export default function Home() {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const [showPopup, setShowPopup] = useState(false);
  const [huntName, setHuntName] = useState("");

  const handleCreate = () => {
    if (huntName.trim()) {
      // Here you would typically save the hunt to a database or state management
      // For now, we just navigate to the edit page with the hunt name
      navigate(`/EditHunt?name=${encodeURIComponent(huntName)}`);
    }
  };

  return (
    <div className="home-container">
      <h1 className="heading">{t("scavenger_hunt")}</h1>
      <div className="button-row">
        <button className="main-button main-button-green" onClick={() => navigate("/join")}>{t("join")}</button>
        <button className="main-button" onClick={() => setShowPopup(true)}>{t("create")}</button>
      </div>
      {showPopup && (
        <div className="popup-overlay">
          <div className="popup">
            <h2>{t("enter_hunt_name")}</h2>
            <input
              type="text"
              value={huntName}
              onChange={e => setHuntName(e.target.value)}
              placeholder={t("hunt_name")}
              autoFocus
            />
            <div className="popup-buttons">
              {/* after pressing the create button -> save the hunt with hunt_id and hunt_name then call with the now existing hunt_id the edit page */}
              <button className="main-button main-button-green" onClick={handleCreate}>{t("create")}</button>
              <button className="main-button" onClick={() => setShowPopup(false)}>{t("cancel")}</button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
