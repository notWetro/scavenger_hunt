import { useTranslation } from "react-i18next";

export default function Profile() {
  const { t, i18n } = useTranslation();

  const changeLanguage = (lng) => {
    i18n.changeLanguage(lng);
    localStorage.setItem("language", lng);
  };

  return (
    <div style={{ textAlign: "center", marginTop: "50px" }}>
      <h2>{t("profile")}</h2>
      <p>{t("chooseLanguage")}</p>
      <button onClick={() => changeLanguage("de")}>Deutsch</button>
      <button onClick={() => changeLanguage("en")}>English</button>
      <p style={{ marginTop: "20px" }}>✅ {t("save")}</p>
    </div>
  );
}
