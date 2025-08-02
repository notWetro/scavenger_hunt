// nur kurz zum schwischenspeichern (muss auf ein anderes Gerät wechseln)
//
// Und hier ist ein Beispiel, wie du die Komponente verwenden kannst:
//
import React from 'react';
import MapComponent from './MapComponent';
import './MapComponent.css';

/**
 * Beispiel für die Verwendung der MapComponent
 */
const MapExample = () => {
  // Beispiel-Koordinaten (Berlin)
  const berlinCoords = {
    latitude: 52.5200,
    longitude: 13.4050
  };

  // Beispiel-Koordinaten (München)
  const munichCoords = {
    latitude: 48.1351,
    longitude: 11.5820
  };

  return (
    <div style={{ padding: '20px' }}>
      <h2>Karten-Beispiele</h2>

      <h3>Standard-Karte (Berlin)</h3>
      <MapComponent
        latitude={berlinCoords.latitude}
        longitude={berlinCoords.longitude}
        popupText="Berlin, Deutschland"
        className="map-container"
      />

      <br />

      <h3>Kleine Karte mit weniger Zoom (München)</h3>
      <MapComponent
        latitude={munichCoords.latitude}
        longitude={munichCoords.longitude}
        zoom={10}
        height="300px"
        popupText="München, Deutschland"
        className="map-container map-small"
      />

      <br />

      <h3>Nicht-interaktive Karte</h3>
      <MapComponent
        latitude={berlinCoords.latitude}
        longitude={berlinCoords.longitude}
        interactive={false}
        height="200px"
        className="map-container"
      />
    </div>
  );
};

export default MapExample;



// So kannst du die MapComponent in deinen anderen Komponenten verwenden:
//
import React from 'react';
import MapComponent from './MapComponent';

const MyOtherComponent = () => {
  // Deine Koordinaten (z.B. von einer API oder Props)
  const myLatitude = 48.1351;
  const myLongitude = 11.5820;

  return (
    <div>
      <h2>Meine Position</h2>
      <MapComponent
        latitude={myLatitude}
        longitude={myLongitude}
        zoom={15}
        height="400px"
        popupText="Hier bin ich!"
        className="map-container"
      />
    </div>
  );
};

export default MyOtherComponent;


// Wichtige Schritte:

//1. **Installiere die Abhängigkeiten:**
   ```bash
   cd scavenger_hunt/frontend
   npm install leaflet react-leaflet
   ```
/*
2. **Die MapComponent akzeptiert folgende Props:**
   - `latitude` (required): Breitengrad
   - `longitude` (required): Längengrad
   - `zoom` (optional): Zoom-Level (Standard: 13)
   - `width` (optional): Breite (Standard: "100%")
   - `height` (optional): Höhe (Standard: "400px")
   - `popupText` (optional): Text für Popup
   - `className` (optional): CSS-Klasse
   - `interactive` (optional): Ob die Karte interaktiv ist (Standard: true)

3. **Fehlerbehandlung:** Die Komponente validiert die Koordinaten und zeigt Fehlermeldungen bei ungültigen Werten.

4. **Responsive Design:** Die Karte passt sich automatisch an verschiedene Bildschirmgrößen an.

Die Komponente ist vollständig wiederverwendbar und du kannst sie überall in deiner App mit verschiedenen Koordinaten aufrufen!
*/

// package.json
"dependencies": {
  "@hello-pangea/dnd": "^18.0.1",
  "i18next": "^25.3.2",
  "react": "^19.1.0",
  "react-dom": "^19.1.0",
  "react-i18next": "^15.6.0",
  "react-icons": "^5.5.0",
  "react-leaflet": "^4.2.1",
  "react-router-dom": "^7.7.0"
},

// Zum testen auf localhost
