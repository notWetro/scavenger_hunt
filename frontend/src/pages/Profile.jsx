import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";
import "./Profile.css";
import { AuthContext } from "../AuthContext";

export default function Profile() {
  const { t, i18n } = useTranslation();
  const navigate = useNavigate();
  const { user, logout, authFetch } = React.useContext(AuthContext);

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

  const handleSave = async () => {
    
    try {
      const resp = await authFetch("http://localhost:8000/users/me", {
        method: "PATCH",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          username: user.username,
          language: i18n.language,
          dark_mode: darkMode,
        }),
      });
      if (!resp.ok) throw new Error(await resp.text());
      
    } catch (err) {
      console.error(err);
      
    }
  };


  return (
    <div className="profile-container">
      <h1 className="heading">{t("profile")}</h1>
      <div className="button-column">
        {user ? (
          <>
            <div>{user.email}</div>
            <div>{user.username}</div>
            {/*}
            <button
              className="main-button"
              onClick={() => navigate("/change-password")}
            >
              {t("change_password")}
            </button>
            */}
          </>
        ) : (
          <>
            <button
              className="main-button main-button-green"
              onClick={() => navigate("/login")}
            >
              {t("login")}
            </button>
            <button
              className="main-button"
              onClick={() => navigate("/register")}
            >
              {t("register")}
            </button>
          </>
        )}
      </div>

      {/* choose language */}
      <div className="language-container">
        <label className="language-label">{t("language")}:</label>
        <select
          className="language-select"
          value={i18n.language}
          onChange={(e) => {
            i18n.changeLanguage(e.target.value);
            handleSave();
          }}
        >
          <option value="de">{t("german")}</option>
          <option value="en">{t("english")}</option>
        </select>
      </div>

      {/* darkmode button */}
      <div className="darkmode-container">
        <button
          className="main-button"
          onClick={() => {
            setDarkMode(!darkMode);
            handleSave();
          }}
        >
          {darkMode ? "☀️ " + t("light_mode") : "🌙 " + t("dark_mode")}
        </button>
      </div>

      {/* logout button */}
      {user && (
        <div className="logout-container">
          <button
            className="main-button main-button-red"
            onClick={logout}
          >
            {t("logout")}
          </button>
        </div>
      )}
    </div>
  );
}
