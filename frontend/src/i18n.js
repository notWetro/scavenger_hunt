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
          hunts: "Hunts",
          language: "Language",
          light_mode: "Light Mode",
          dark_mode: "Dark Mode"
        },
      },
      de: {
        translation: {
          home: "Startseite",
          welcome: "Willkommen auf meiner Homepage!",
          profile: "Profil",
          hunts: "Jagden",
          language: "Sprache",
          light_mode: "Heller Modus",
          dark_mode: "Dunkler Modus"
        },
      },
    },
    lng: localStorage.getItem("language") || "en",
    fallbackLng: "en",
    interpolation: { escapeValue: false },
  });

export default i18n;
