import React from "react";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";
import "./Home.css";

export default function Home() {
  const { t } = useTranslation();
  const navigate = useNavigate();

  return (
    <div className="home-container">
      <h1>{t("scavenger_hunt")}</h1>
      <div className="button-row">
        <button className="main-button" onClick={() => navigate("/join")}>{t("join")}</button>
        <button className="main-button" onClick={() => navigate("/create")}>{t("create")}</button>
      </div>
    </div>
  );
}
