import React, { useEffect, useState } from "react";
import { MapContainer, TileLayer, Marker, Popup } from "react-leaflet";
import "leaflet/dist/leaflet.css";
import L from "leaflet";

// Fix für die Standard-Marker-Icons in React-Leaflet
delete L.Icon.Default.prototype._getIconUrl;
L.Icon.Default.mergeOptions({
  iconRetinaUrl:
    "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.9.4/images/marker-icon-2x.png",
  iconUrl:
    "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.9.4/images/marker-icon.png",
  shadowUrl:
    "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.9.4/images/marker-shadow.png",
});

/**
 * MapComponent - Wiederverwendbare Karten-Komponente mit OpenStreetMap
 *
 * @param {Object} props
 * @param {number} props.latitude - Breitengrad für die Marker-Position
 * @param {number} props.longitude - Längengrad für die Marker-Position
 * @param {number} [props.zoom=13] - Zoom-Level der Karte (optional, Standard: 13)
 * @param {string} [props.width="100%"] - Breite der Karte (optional, Standard: "100%")
 * @param {string} [props.height="400px"] - Höhe der Karte (optional, Standard: "400px")
 * @param {string} [props.popupText] - Text für das Popup beim Klick auf den Marker (optional)
 * @param {string} [props.className] - CSS-Klasse für zusätzliches Styling (optional)
 * @param {boolean} [props.interactive=true] - Ob die Karte interaktiv sein soll (optional, Standard: true)
 */
const MapComponent = ({
  latitude,
  longitude,
  zoom = 13,
  width = "100%",
  height = "400px",
  popupText,
  className = "",
  interactive = true,
}) => {
  const [mapKey, setMapKey] = useState(0);

  // Validierung der Koordinaten
  if (typeof latitude !== "number" || typeof longitude !== "number") {
    return (
      <div style={{ width, height }} className={`map-error ${className}`}>
        <div
          style={{
            display: "flex",
            alignItems: "center",
            justifyContent: "center",
            height: "100%",
            backgroundColor: "#f0f0f0",
            border: "1px solid #ddd",
            borderRadius: "4px",
            color: "#666",
          }}
        >
          Ungültige Koordinaten: Breitengrad und Längengrad müssen Zahlen sein
        </div>
      </div>
    );
  }

  // Überprüfung ob Koordinaten im gültigen Bereich sind
  if (latitude < -90 || latitude > 90 || longitude < -180 || longitude > 180) {
    return (
      <div style={{ width, height }} className={`map-error ${className}`}>
        <div
          style={{
            display: "flex",
            alignItems: "center",
            justifyContent: "center",
            height: "100%",
            backgroundColor: "#f0f0f0",
            border: "1px solid #ddd",
            borderRadius: "4px",
            color: "#666",
          }}
        >
          Koordinaten außerhalb des gültigen Bereichs
        </div>
      </div>
    );
  }

  const position = [latitude, longitude];

  return (
    <div style={{ width, height }} className={className}>
      <MapContainer
        key={mapKey}
        center={position}
        zoom={zoom}
        style={{ height: "100%", width: "100%" }}
        zoomControl={interactive}
        dragging={interactive}
        touchZoom={interactive}
        doubleClickZoom={interactive}
        scrollWheelZoom={interactive}
        boxZoom={interactive}
        keyboard={interactive}
      >
        <TileLayer
          attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
          url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
        />
        <Marker position={position}>
          {popupText && <Popup>{popupText}</Popup>}
        </Marker>
      </MapContainer>
    </div>
  );
};

export default MapComponent;
