# Handbuch

## Vor dem Spielen
Bevor das Spiel gestartet wird, sollte eine Hunt erstellt werden, wie sie beispielsweise in `demos\demo_hunt\ErstsemesterSchnitzeljagd.pdf` beschrieben ist.

### Achtung!
Schnitzeljagden haben eine begrenzte Laufzeit, in der sie aktiv spielbar sind. Deshalb muss die Schnitzeljagd kurz vor dem Spielen erstellt werden.

Folgende Voraussetzungen müssen erfüllt sein:
- Der Rechner, auf dem das Backend läuft, muss eingeschaltet sein.
- Die Docker-Container müssen aktiv sein.
- Das Hunt-Web-Game muss im Modus `npm run preview -- --open --host` gestartet werden.
- Der Port 4173 muss auf dem Host-Rechner geöffnet sein.

## Teilen einer Schnitzeljagd
Zum Teilen einer Schnitzeljagd kann der **Share-Button** im Editor genutzt werden.

Es gibt zwei Möglichkeiten:
1. Einen QR-Code herunterladen, der die Adresse zur Schnitzeljagd enthält.
2. Einen Link in die Zwischenablage kopieren.

Die Teilnehmenden müssen anschließend nur den QR-Code scannen oder den Link im Browser eingeben.

## Spielanleitung
Zum Starten muss der geteilte QR-Code gescannt oder der Link eingegeben werden. Die daraufhin erscheinende Seite ist die Registrierungsseite für die Hunt.

**Registrierung:**
- Benutzername und Passwort eingeben.
- Das Passwort muss mindestens **acht Zeichen**, einen **Großbuchstaben** und eine **Zahl** enthalten.
- Wenn sich ein Benutzer mit denselben Zugangsdaten bei mehreren Schnitzeljagden registriert, erscheinen diese alle in der Spielübersicht.

Nach erfolgreicher Registrierung und dem Bestätigen des **„Play“**-Buttons wird der Benutzer eingeloggt. In der Spielübersicht werden nun alle registrierten Hunts angezeigt.

**Spielablauf:**
1. Wählen Sie eine Schnitzeljagd aus und klicken Sie auf **„Play Hunt“**.
2. Sie erhalten nach und nach verschiedene Aufgaben. Diese können beispielsweise beinhalten:
   - Einen bestimmten Ort auf einem Bild zu finden.
   - Einen Standort mithilfe eines Audio-Hinweises zu entdecken.
3. Sobald eine Aufgabe gelöst wurde, klicken Sie auf **„Answer found“**.
4. Abhängig von der Aufgabenstellung gibt es verschiedene Lösungsmöglichkeiten:
   - Einen Text eingeben.
   - Einen QR-Code scannen.
   - Sich in der Nähe eines bestimmten Standortes befinden.
5. Nach erfolgreichem Lösen wird die nächste Aufgabe freigeschaltet.
6. Beim Abschluss der letzten Aufgabe gibt es eine **mysteriöse Überraschung**. 🎉

**Viel Spaß beim Spielen und Entdecken neuer Orte!**

