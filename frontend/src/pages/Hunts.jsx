import React, { useState, useEffect, useContext } from "react";
import "./Hunts.css";
import { useTranslation } from "react-i18next";
import { AuthContext } from "../AuthContext";

export default function Hunts() {
  const { t } = useTranslation();
  const { user, authFetch } = useContext(AuthContext);
  const [selectedTab, setSelectedTab] = useState("joined");
  const [hunts, setHunts] = useState([]);
  const [loading, setLoading] = useState(false);

  const fetchHunts = async (type) => {
    setLoading(true);
    try {
      let endpoint;
      let options = {};

      switch (type) {
        case "joined":
          endpoint = "http://localhost:8000/hunts/joined";
          options = { method: "GET" };
          break;
        case "own":
          endpoint = "http://localhost:8000/hunts/own";
          options = { method: "GET" };
          break;
        case "browse":
          endpoint = "http://localhost:8000/hunts/public";
          options = { method: "GET" };
          break;
        default:
          endpoint = "http://localhost:8000/hunts/public";
      }

      const response =
        user && (type === "joined" || type === "own")
          ? await authFetch(endpoint, options)
          : await fetch(endpoint, options);

      if (!response.ok) {
        throw new Error(`Failed to fetch ${type} hunts`);
      }

      const data = await response.json();
      setHunts(data);
    } catch (error) {
      console.error(`Error fetching ${type} hunts:`, error);
      setHunts([]);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchHunts(selectedTab);
  }, [selectedTab, user]);

  const handleTabChange = (tab) => {
    setSelectedTab(tab);
  };

  return (
    <div className="hunts-container">
      <h1 className="heading">{t("hunts")}</h1>

      {/* Tab Slider */}
      <div className="hunt-tab-slider">
        <div className="tab-radio-group">
          <input
            type="radio"
            id="tab-joined"
            name="hunt-tabs"
            checked={selectedTab === "joined"}
            onChange={() => handleTabChange("joined")}
          />
          <label htmlFor="tab-joined" data-label={t("joined")}>
            {t("joined")}
          </label>

          <input
            type="radio"
            id="tab-own"
            name="hunt-tabs"
            checked={selectedTab === "own"}
            onChange={() => handleTabChange("own")}
          />
          <label htmlFor="tab-own" data-label={t("own")}>
            {t("own")}
          </label>

          <input
            type="radio"
            id="tab-browse"
            name="hunt-tabs"
            checked={selectedTab === "browse"}
            onChange={() => handleTabChange("browse")}
          />
          <label htmlFor="tab-browse" data-label={t("browse")}>
            {t("browse")}
          </label>

          <span className="tab-glider" />
        </div>
      </div>

      {/* Hunt List */}
      <div className="hunt-list">
        {loading ? (
          <div className="loading">{t("loading")}...</div>
        ) : hunts.length > 0 ? (
          hunts.map((hunt) => (
            <div key={hunt.id} className="hunt-card">
              <h3>{hunt.name}</h3>
              <p>
                <strong>{t("location")}:</strong> {hunt.place_to_play}
              </p>
              <p>
                <strong>{t("start_point")}:</strong> {hunt.start_point}
              </p>
              <p>
                <strong>{t("creator")}:</strong> {hunt.creator_username}
              </p>
              {hunt.created_at && (
                <p className="date">
                  {new Date(hunt.created_at).toLocaleDateString()}
                </p>
              )}
            </div>
          ))
        ) : (
          <div className="no-hunts">
            {selectedTab === "joined" && t("no_joined_hunts")}
            {selectedTab === "own" && t("no_own_hunts")}
            {selectedTab === "browse" && t("no_public_hunts")}
          </div>
        )}
      </div>
    </div>
  );
}
