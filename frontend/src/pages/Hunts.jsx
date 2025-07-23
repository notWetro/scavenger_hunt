import "./Hunts.css";
import { useTranslation } from "react-i18next";

const hunts = [
  {
    id: 1,
    animal: "Rehbock",
    location: "Bayern, Deutschland",
    date: "2025-06-12",
  },
  {
    id: 2,
    animal: "Wildschwein",
    location: "Sachsen",
    date: "2025-05-03",
  },
  {
    id: 3,
    animal: "Fuchs",
    location: "Tirol, Österreich",
    date: "2025-04-18",
  },
];

export default function Hunts() {
  const { t } = useTranslation();

  return (
    <div className="hunts-container">
      <h1 className="heading">{t("hunts")}</h1>
      <div className="hunt-list">
        {hunts.map((hunt) => (
          <div key={hunt.id} className="hunt-card">
            <h3>{hunt.animal}</h3>
            <p>{hunt.location}</p>
            <p className="date">{hunt.date}</p>
          </div>
        ))}
      </div>
    </div>
  );
}
