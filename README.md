# Projekt AR-Schnitzeljagd

## Einführung

Hier befindet sich das Repository für das im Rahmen einer Projektarbeit entwickelte System einer AR-Schnitzeljagd und der Erweiterung dieser.

**Autoren**: Lukas Steckbauer, Rosario Aranzulla, Felix Kurz und Felix Biedenbacher

**Betreuer**: Dr. Marc Hermann

**Einrichtung**: _Hochschule-Aalen Fakultät Informatik und Elektrotechnik_

## Quicklinks

### Projekte

- **Backend** und **API**: [Hunt-API](src/be-hunt-api/README.md)
- **Web-Game**: [Hunt-Game](src/fe-hunt-web-game/README.md)
- **Web-App**: [Hunt-Editor](src/fe-hunt-editor/README.md)
- **Hunt-Gui**: [Hunt-Gui](src/fe-hunt-gui/README.md)

### Dokumentation

Die Dokumentation der Projektarbeiten befindet sich im Verzeichnis `.\docs\` [hier](docs/).

### Funktion und Benutzung des Spiels

Um eine Schnitzeljagt zu erstellen empfehlen wir ihnen die vorhandene [Hunt-Gui](src/fe-hunt-gui/README.md) zu verwenden. Falls sie dennoch ohne Gui eine Hunt erstellen wollen finden sie die anleitung dazu im Ordner [Hunt-Editor](src/fe-hunt-editor/README.md). Um ein spiel zu starten muss das [Hunt-API](src/be-hunt-api/README.md) zum verwalten der Daten laufen dies wird beim verwenden der Gui automatisch gemacht. Zum Spielen einer Hunt kann der im Editor erstellte QR-Code oder Link verwendet werden. Eine erstellte Hunt kann aber auch wieder über die Gui oder über die [Hunt-Game](src/fe-hunt-web-game/README.md) getestet werden.

# Utilities

## QR-Code Generator

Für das Testen (und für die Erstellung des QR-Code-Generators innerhalb des Projekts) wurde eine Utility erstellt, die mittels Kommandozeilen-Aufruf einen QR-Code basierend auf einen Input-String erstellt. Der resultierende QR-Code wird im `Downloads` Verzeichnis des Benutzers angelegt und ist über einen Timestamp identifizierbar (bspw. `QRCode_20240323113550.png`). Die utility ist [hier](utils/qrcode-generator/) einsehbar.

Test Commit