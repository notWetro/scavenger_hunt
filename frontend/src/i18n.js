import i18n from "i18next";
import { initReactI18next } from "react-i18next";

i18n
  .use(initReactI18next)
  .init({
    resources: {
      en: {
        translation: {
          home: "Home",
          welcome: "Welcome to my homepage!",
          profile: "Profile",
          chooseLanguage: "Choose your language:",
          save: "Save",
          hunts: "Hunts",
        },
      },
      de: {
        translation: {
          home: "Startseite",
          welcome: "Willkommen auf meiner Homepage!",
          profile: "Profil",
          chooseLanguage: "Wähle deine Sprache:",
          save: "Speichern",
          hunts: "Jagden",
        },
      },
    },
    lng: localStorage.getItem("language") || "en",
    fallbackLng: "en",
    interpolation: { escapeValue: false },
  });

export default i18n;
