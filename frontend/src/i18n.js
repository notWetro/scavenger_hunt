import i18n from "i18next";
import { initReactI18next } from "react-i18next";

i18n
  .use(initReactI18next)
  .init({
    resources: {
      en: {
        translation: {
          home: "Home",
          scavenger_hunt: "Scavenger Hunt",
          profile: "Profile",
          hunts: "Hunts",
          language: "Language",
          german: "German",
          english: "English",
          light_mode: "Light Mode",
          dark_mode: "Dark Mode",
          join: "Join",
          create: "Create",
        },
      },
      de: {
        translation: {
          home: "Startseite",
          scavenger_hunt: "Schnitzeljagd",
          profile: "Profil",
          hunts: "Jagden",
          language: "Sprache",
          german: "Deutsch",
          english: "Englisch",
          light_mode: "Heller Modus",
          dark_mode: "Dunkler Modus",
          join: "Beitreten",  
          create: "Erstellen",
        },
      },
    },
    lng: localStorage.getItem("language") || "en",
    fallbackLng: "en",
    interpolation: { escapeValue: false },
  });

export default i18n;
