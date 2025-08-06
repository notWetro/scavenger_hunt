import React, { useEffect, useState, useContext } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useTranslation } from "react-i18next";
import "./StartHunt.css";
import { AuthContext } from "../../AuthContext";
import QRCode from "react-qr-code";

export default function StartHunt() {
  const { huntCode } = useParams();
  const navigate = useNavigate();
  const { t } = useTranslation();
  const [hunt, setHunt] = useState({});
  const { user, authFetch, logout } = useContext(AuthContext);
  const [showSharePopup, setShowSharePopup] = useState(false);
  const [copySuccess, setCopySuccess] = useState("");
  const [error, setError] = useState("");

  const shareUrl = `${window.location.origin}/StartHunt/${huntCode}`;
  
  useEffect(() => {
    console.log(huntCode);
    const fetchHunt = async () => {
      try {
        const response = await authFetch(`/hunts/by-code/${huntCode}`);
        if (!response.ok) {
          throw new Error("Failed to fetch hunt details");
        }
        const data = await response.json();
        setHunt(data);
        console.log(data);
      } catch (error) {
        console.error("Error fetching hunt details:", error);
      }
    };
    fetchHunt();
  }, [huntCode]);

  const handleCopyLink = async () => {
    try {
      await navigator.clipboard.writeText(shareUrl);
      setCopySuccess(t("link_copied"));
    } catch {
      setCopySuccess(t("copy_failed"));
    }
  };

  const handleStartHunt = async () => {
    if (hunt.is_active === false) {
      alert(t("hunt_inactive"));
    } else {
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
        
      } catch (err) {
        console.error(err);
        setError(t("join_failed"));
      }
      navigate(`/PlayHunt/${huntCode.trim()}`);
    }
  };

  //ToDo: add translation and change alerts to notifications from our side
  const removeHunt = async () => {
    if (!window.confirm("Are you sure you want to leave this hunt?")) return;
    try {
      const response = await authFetch(`/hunts/by-code/${hunt.code}/leave`, {
        method: "DELETE",
      });
      if (!response.ok) {
        throw new Error("Failed to remove hunt");
      }
      alert("You’ve left the hunt.");
      navigate(-1);
    } catch (error) {
      console.error("Error removing hunt:", error);
      alert("Could not leave the hunt.");
    }
  };

  return (
    <div className="start-hunt-container">
      <h1 className="heading">{hunt.name}</h1>

      <div className="hunt-details">
        <p>
          <strong>{t("hunt_code")}:</strong> {hunt.code}
        </p>
        <p>
          <strong>{t("hunt_info")}:</strong> {hunt.description}
        </p>
        <p>
          <strong>{t("location")}:</strong> {hunt.place_to_play}
        </p>
        <p>
          <strong>{t("start_point")}:</strong> {hunt.start_point}
        </p>
        <p>
          <strong>{t("creator")}:</strong> {hunt.creator_username}
        </p>
        <p>
          <strong>{t("hunt_status")}:</strong> {hunt.is_active ? t("active") : t("inactive")}
        </p>
        <p>
          <strong>{t("private_hunt")}:</strong> {hunt.private ? t("yes") : t("no")}
        </p>
      </div>
      {error && (
          <div className="error-message-hunt-code">
            {error}
          </div>
        )}
      <div className="button-column">
        <button
          className="main-button main-button-green"
          onClick={handleStartHunt}
        >
          {t("start_hunt")}
        </button>
        {user && (
          <button
            className="main-button main-button-red"
            onClick={() => removeHunt()}
          >
            {t("remove_hunt")}
          </button>
        )}
        <button
          type="button"
          className="main-button"
          onClick={() => navigate(-1)}
        >
          {t("back")}
        </button>
        <button
          className="main-button main-button-blue"
          onClick={() => {
            setCopySuccess("");
            setShowSharePopup(true);
          }}
        >
          {t("publish_hunt")}
        </button>

        {showSharePopup && (
          <div className="popup-overlay">
            <div className="popup">
              <h2>{t("share_link")}</h2>
              <p>{shareUrl}</p>
              <div>
                <QRCode value={shareUrl} size={128} />
              </div>
              <div className="popup-buttons">
                <button className="main-button" onClick={handleCopyLink}>
                  {t("copy_link")}
                </button>
                <button
                  className="main-button"
                  onClick={() => setShowSharePopup(false)}
                >
                  {t("close")}
                </button>
              </div>
              {copySuccess && (
                <p className="copy-feedback">{copySuccess}</p>
              )}
            </div>
          </div>
        )}
      </div>
    </div>
  );
}
