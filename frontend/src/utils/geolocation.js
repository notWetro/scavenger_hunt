/**
 * Fragt den aktuellen Standort des Gerätes ab
 * @param {Function} showAlert - Optional: Funktion zum Anzeigen von Fehlermeldungen
 * @returns {Promise<{latitude: number, longitude: number}>} Promise mit Längen- und Breitengrad
 * @throws {Error} Wenn Geolocation nicht verfügbar ist oder verweigert wird
 */
export const getCurrentLocation = (showAlert = null) => {
  return new Promise((resolve, reject) => {
    // Prüfen ob Geolocation API verfügbar ist
    if (!navigator.geolocation) {
      const errorMessage =
        "Geolocation wird von diesem Browser nicht unterstützt";
      if (showAlert) showAlert(errorMessage);
      reject(new Error(errorMessage));
      return;
    }

    // Optionen für die Standortabfrage
    const options = {
      enableHighAccuracy: true, // Hohe Genauigkeit anfordern
      timeout: 10000, // 10 Sekunden Timeout
      maximumAge: 5000, // Cache für 5 Sekunden akzeptieren
    };

    // Standort abfragen
    navigator.geolocation.getCurrentPosition(
      (position) => {
        // Erfolg: Koordinaten zurückgeben
        resolve({
          latitude: position.coords.latitude,
          longitude: position.coords.longitude,
          accuracy: position.coords.accuracy, // Zusätzlich: Genauigkeit in Metern
        });
      },
      (error) => {
        // Fehler behandeln
        let errorMessage;
        switch (error.code) {
          case error.PERMISSION_DENIED:
            errorMessage = "Berechtigung für Standortzugriff wurde verweigert";
            break;
          case error.POSITION_UNAVAILABLE:
            errorMessage = "Standortinformationen sind nicht verfügbar";
            break;
          case error.TIMEOUT:
            errorMessage = "Zeitüberschreitung bei der Standortabfrage";
            break;
          default:
            errorMessage = "Unbekannter Fehler bei der Standortabfrage";
            break;
        }
        if (showAlert) showAlert(errorMessage);
        reject(new Error(errorMessage));
      },
      options,
    );
  });
};
