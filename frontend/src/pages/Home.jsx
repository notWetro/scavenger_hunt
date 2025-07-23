import React from "react";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";
import "./Home.css";

export default function Home() {
  const { t } = useTranslation();
  const navigate = useNavigate();

  return (
    <div style={{ textAlign: "center", marginTop: "50px" }}>
      <h1>{t("welcome")}</h1>
      <div className="button-row">
        <button className="main-button" onClick={() => navigate("/join")}>join</button>
        <button className="main-button" onClick={() => navigate("/create")}>creat</button>
      </div>
    </div>
  );
}
