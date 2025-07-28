import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useTranslation } from "react-i18next";
import "./StartHunt.css";

const mockHunts = [
  {
    id: "hunt2025",
    name: "Abenteuer durch die Altstadt",
    location: "München",
    startPoint: {
      lat: 48.137154,
      lng: 11.576124
    },
    creator: "max.mustermann@email.de"
  },
  {
    id: "1",
    name: "Abenteuer durch die Altstadt",
    location: "München",
    startPoint: {
      lat: 48.137154,
      lng: 11.576124
    },
    creator: "max.mustermann@email.de"
  },
  {
    id: "12345",
    name: "Rätselrallye im Park",
    location: "Berlin",
    startPoint: {
      lat: 52.520008,
      lng: 13.404954
    },
    creator: "julia.schmidt@email.de"
  }
];

export default function StartHunt() {
  const { huntId } = useParams();
  const navigate = useNavigate();
  const { t } = useTranslation();
  const [hunt, setHunt] = useState({});

  // const hunt = mockHunts.find(h => h.id === huntId);


  useEffect(() => { 
    const fetchHunt = async () => {
      try {
        const response = await fetch(`http://localhost:8000/hunts/${huntId}`);
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

  return (
    <div className="start-hunt-container">
      <h1 className="heading">
        {hunt.name}
      </h1>
      
      <div className="hunt-details">
        <p><strong>{t("hunt_id")}:</strong> {hunt.id}</p>
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
        <button
          className="main-button main-button-red"
          // onClick={() => removeHunt(huntId)} // need to implement this function -> funktion removes hunt from user's list and navigates back (to Hunts page???)
        >
          {t("remove_hunt")}
        </button>
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
