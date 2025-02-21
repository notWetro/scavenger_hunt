# Projekt AR-Schnitzeljagd

## Einführung

Hier befindet sich das Repository für das im Rahmen einer Projektarbeit entwickelte System einer Web-Schnitzeljagd und der Erweiterung dieser.

**Autoren**: Lukas Steckbauer, Rosario Aranzulla, Felix Kurz und Felix Biedenbacher

**Betreuer**: Dr. Marc Hermann

**Einrichtung**: _Hochschule-Aalen Fakultät Informatik und Elektrotechnik_

## Quicklinks

### Projekte

In den einzelnen Unterverzeichnissen befinden sich Readme-Dateien, die die Aufgaben der Ordner sowie deren Benutzung erklären.  

Zum groben Überblick hier die Aufgaben der einzelnen verlinkten Ordner:

- **Backend** und **API** [Hunt-API](src/be-hunt-api/README.md): Wie der Name schon suggeriert, beinhaltet dieser Ordner das Backend. Hier befindet sich die Logik zur Verwaltung von Spielen und Spielern sowie die REST-APIs. Außerdem wird der Node-Server hier verwaltet. 
- **Web-Game** [Hunt-Game](src/fe-hunt-web-game/README.md): In diesem Ordner befindet sich das Frontend des Spiels. Hier können Schnitzeljagden gespielt und ihnen beigetreten werden.
- **Web-App** [Hunt-Editor](src/fe-hunt-editor/README.md): Diese Webanwendung dient zum Erstellen und Verwalten von Schnitzeljagden.
- **Hunt-Gui** [Hunt-Gui](src/fe-hunt-gui/README.md):Dieses Tool bietet eine einfache Möglichkeit, das Backend und die beiden Frontend-Anwendungen zu verwalten. Mithilfe der GUI kann zentral auf die Webanwendungen zugegriffen werden, ohne manuell z. B. Docker-Container starten zu müssen.


### Dokumentation

Die Dokumentation der Projektarbeiten befindet sich im Verzeichnis `.\docs\` [hier](docs/).

### Demos

Im Verzeichnis [demos](demos/) befinden sich:  
- Im Unterordner [videos](demos/videos) **Demovideos** zur Verwendung der Anwendung.  
- Im Unterordner [demo](demos/demo) **Videos, Bilder, Audios, Texte, QR-Codes und Standorte** mit einer Anleitung zum Erstellen einer Schnitzeljagd für Erstsemester-Studierende.  


### Utilities

Für das Testen (und für die Erstellung des QR-Code-Generators innerhalb des Projekts) wurde eine Utility erstellt, die mittels Kommandozeilen-Aufruf einen QR-Code basierend auf einen Input-String erstellt. Der resultierende QR-Code wird im `Downloads` Verzeichnis des Benutzers angelegt und ist über einen Timestamp identifizierbar (bspw. `QRCode_20240323113550.png`). Die utility ist [hier](utils/qrcode-generator/) einsehbar.

## Funktion und Benutzung des Spiels

Um eine Schnitzeljagd zu erstellen, empfehlen wir Ihnen, die vorhandene [Hunt-GUI](src/fe-hunt-gui/README.md) zu verwenden. Falls Sie dennoch ohne GUI eine Hunt erstellen möchten, finden Sie die Anleitung dazu im Ordner [Hunt-Editor](src/fe-hunt-editor/README.md).  

Um ein Spiel zu starten, muss das [Hunt-API](src/be-hunt-api/README.md) zur Verwaltung der Daten laufen – dies wird bei der Verwendung der GUI automatisch erledigt.  

Zum Spielen einer Hunt kann der im Editor erstellte QR-Code oder Link verwendet werden. Eine erstellte Hunt kann jedoch auch über die GUI oder über das [Hunt-Game](src/fe-hunt-web-game/README.md) getestet werden.




Test Commit