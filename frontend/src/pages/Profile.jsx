import { useTranslation } from "react-i18next";
import { useEffect, useState } from "react";
import "./Profile.css";

export default function Profile() {
  const { t, i18n } = useTranslation();

  const [darkMode, setDarkMode] = useState(
    () => localStorage.getItem("darkMode") === "true"
  );

  useEffect(() => {
    if (darkMode) {
      document.body.classList.add("dark-mode");
    } else {
      document.body.classList.remove("dark-mode");
    }
    localStorage.setItem("darkMode", darkMode);
  }, [darkMode]);

  return (
    <div className="profile-container">
      <h1>{t("profile")}</h1>

      {/* 🌐 Sprachauswahl */}
      <div className="language-container">
        <label htmlFor="language" className="language-label">
          {t("language")}:
        </label>
        <select
          id="language"
          className="language-select"
          onChange={(e) => i18n.changeLanguage(e.target.value)}
          value={i18n.language}
        >
          <option value="de">Deutsch</option>
          <option value="en">English</option>
        </select>
      </div>

      {/* 🌙 Darkmode Umschalter */}
      <div className="darkmode-container">
        <button
          className="main-button"
          onClick={() => setDarkMode(!darkMode)}
        >
          {darkMode ? "☀️ " + t("light_mode") : "🌙 " + t("dark_mode")}
        </button>
      </div>
    </div>
  );
}
