import React, { useEffect, useState, useContext } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useTranslation } from "react-i18next";
import "./StartHunt.css";
import { AuthContext } from "../../AuthContext";


export default function StartHunt() {
  const { huntId } = useParams();
  const navigate = useNavigate();
  const { t } = useTranslation();
  const [hunt, setHunt] = useState({});
  const { user, authFetch, logout } = useContext(AuthContext);


  useEffect(() => { 
    const fetchHunt = async () => {
      try {
        const response = await authFetch(`/hunts/${huntId}`);
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
  }, [huntId]);

  const removeHunt = async () => {
    if (!window.confirm("Are you sure you want to leave this hunt?")) return;
    try {
      const response = await authFetch(`/hunts/${huntId}/leave`, {
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
      <h1 className="heading">
        {hunt.name}
      </h1>
      
      <div className="hunt-details">
        <p><strong>{t("hunt_id")}:</strong> {hunt.id}</p>
        <p><strong>{t("hunt_info")}:</strong> {hunt.description}</p>
        <p><strong>{t("location")}:</strong> {hunt.place_to_play}</p>
        <p><strong>{t("start_point")}:</strong> {hunt.start_point}</p>
        <p><strong>{t("creator")}:</strong> {hunt.creator_username}</p>
      </div>
      
      <div className="button-column">
        <button
          className="main-button main-button-green"
          onClick={() => navigate(`/PlayHunt/${huntId.trim()}`)}
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
      </div>
    </div>
  );
}
