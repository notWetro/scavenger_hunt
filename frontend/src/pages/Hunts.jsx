import "./Hunts.css";

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
  return (
    <div className="hunts-container">
      <h2>🦌 Deine Jagden</h2>
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
