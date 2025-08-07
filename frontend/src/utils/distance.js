/**
 * Berechnet die Distanz zwischen zwei GPS-Koordinaten in Metern
 * @param {number} lat1 - Breite des ersten Punkts
 * @param {number} lon1 - Länge des ersten Punkts
 * @param {number} lat2 - Breite des zweiten Punkts
 * @param {number} lon2 - Länge des zweiten Punkts
 * @returns {number} Distanz in Metern
 */
export function getDistanceFromLatLonInMeters(lat1, lon1, lat2, lon2) {
  const R = 6371000; // Erdradius in Metern
  const dLat = ((lat2 - lat1) * Math.PI) / 180;
  const dLon = ((lon2 - lon1) * Math.PI) / 180;

  const a =
    Math.sin(dLat / 2) * Math.sin(dLat / 2) +
    Math.cos((lat1 * Math.PI) / 180) *
      Math.cos((lat2 * Math.PI) / 180) *
      Math.sin(dLon / 2) *
      Math.sin(dLon / 2);

  const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
  return R * c;
}
