# Projekt AR-Schnitzeljagd

## Einführung

Hier befindet sich das Repository für das im Rahmen einer Projektarbeit entwickelte System einer AR-Schnitzeljagd.

**Autoren**: Lukas Steckbauer und Rosario Aranzulla

**Betreuer**: Dr. Marc Hermann

**Einrichtung**: _Hochschule-Aalen Fakultät Informatik und Elektrotechnik_

## Quicklinks

### Projekte

- **Backend** und **API**: [Hunt-API](src/be-hunt-api/README.md)
- **Mobile-App**: der aktuellste Stand der AR App befindet sich im Branch `feature/ar-mobile-app`
- **Web-Game**: [Hunt-Game](src/fe-hunt-game-web/README.md)
- **Web-App**: [Hunt-Editor](src/fe-hunt-editor/README.md)
- **Participation-App**: [Participation-App](src/fe-hunt-participation/README.md)

### Dokumentation

Die Dokumentation der Projektarbeit befindet sich im Verzeichnis `.\docs\` [hier](docs/dokumentation-projektarbeit/INDEX.tex).

# Utilities

## QR-Code Generator

Für das Testen (und für die Erstellung des QR-Code-Generators innerhalb des Projekts) wurde eine Utility erstellt, die mittels Kommandozeilen-Aufruf einen QR-Code basierend auf einen Input-String erstellt. Der resultierende QR-Code wird im `Downloads` Verzeichnis des Benutzers angelegt und ist über einen Timestamp identifizierbar (bspw. `QRCode_20240323113550.png`). Die utility ist [hier](utils/qrcode-generator/) einsehbar.
