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
          back: "Back",
          join_a_hunt: "You joined the hunt with the ID: ",
          join_hunt: "Join a Hunt",
          enter_hunt_id: "Enter Hunt ID",
          login: "Login",
          register: "Register",
          logouot: "Logout",
          change_password: "Change Password",
          hunt_id_not_exist: "Hunt ID does not exist",
          hunt_id: "Hunt ID",
          location: "Location",
          start_point: "Start Point",
          creator: "Creator",
          start_hunt: "Start Hunt",
          remove_hunt: "Remove Hunt",
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
          back: "Zurück",
          join_a_hunt: "Du trittst der Jagd mit der ID bei: ",
          join_hunt: "Tritt einer Jagd bei",
          enter_hunt_id: "Gib die Jagd-ID ein",
          login: "Anmelden",
          register: "Registrieren",
          logout: "Abmelden",
          change_password: "Passwort ändern",
          hunt_id_not_exist: "Jagd-ID existiert nicht",
          hunt_id: "Jagd-ID",
          location: "Standort",
          start_point: "Startpunkt",
          creator: "Ersteller",
          start_hunt: "Jagd starten",
          remove_hunt: "Jagd entfernen",
        },
      },
    },
    lng: localStorage.getItem("language") || "en",
    fallbackLng: "en",
    interpolation: { escapeValue: false },
  });

export default i18n;
