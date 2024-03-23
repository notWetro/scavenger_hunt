# Einführung

Repository für die Projektarbeit "AugmentedReality-Schnitzeljagd" von Lukas und Rosario an der Hochschule Aalen.

# Verzeichnisstruktur

- `-\demos\`: Beispiel-Anwendungen um die Verwendung verschiedener Technologien zu testen. Der hier enthaltene Source-Code dient lediglich zum austesten.
- `.\docs\`: Dokumentation der Projektarbeit und eventuelle Literatur.
- `.\libs\`: Bibliotheken, die im Projekt verwendung finden. Beispielsweise `.dll`-Dateien.
- `.\src\`: Projekt-Source-Code für den Schnitzeljagd-Editor bzw. die AR-Mobile-App.
- `.\utils\`: Nützliche Tools die im Rahmen des Projekts Verwendung finden / gefunden haben.

# Inbetriebnahme der Anwendung(en)

### Generieren von QR-Codes mit dem _qrcode-generator_

Für das Testen (und für die Erstellung des QR-Code-Generators innerhalb des Projekts) wurde eine Utility erstellt, die mittels Kommandozeilen-Aufruf einen QR-Code basierend auf einen Input-String erstellt. Der resultierende QR-Code wird im `Downloads` Verzeichnis des Benutzers angelegt und ist über einen Timestamp identifizierbar (bspw. `QRCode_20240323113550.png`).

### Testen der REST-API / Schnittstelle

#### Anleitung

1) Backend-Anwendung compilieren und starten.
2) (Localhost) Adresse und Port notieren.
3) In Projekt `ScavEditor.Api.Tests` die Datei `Program.cs` öffnen.
4) In `Program.cs` Datei die Variable `baseUrl` auf die in Schritt 2 notierte Adresse setzen.
5) Test-Anwendung starten, evtl. Tastatur-Input eingeben.

```cs
string baseUrl = "http://localhost:5282";
```

#### Beispiel-Ausgabe Test-Programm

```
Populating Database with Dummy-Data.
AddUsers: Request was successful!
AddUsers: Request was successful!
AddUsers: Request was successful!
AddUsers: Request was successful!
AddUsers: Request was successful!
AddUsers: Request was successful!
AddUsers: Request was successful!
AddUsers: Request was successful!
AddUsers: Request was successful!
AddUsers: Request was successful!

C:\...\ScavEditor.Api.Tests.exe (process 21984) exited with code 0.
Press any key to close this window . . .
```

#### Beispiel-Ausgabe Backend-Anwendung

```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5282
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\...\ScavEditor.Api
warn: Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionMiddleware[3]
      Failed to determine the https port for redirect.
info: Microsoft.EntityFrameworkCore.Update[30100]
      Saved 1 entities to in-memory store.
info: Microsoft.EntityFrameworkCore.Update[30100]
      Saved 1 entities to in-memory store.
info: Microsoft.EntityFrameworkCore.Update[30100]
      Saved 1 entities to in-memory store.
info: Microsoft.EntityFrameworkCore.Update[30100]
      Saved 1 entities to in-memory store.
info: Microsoft.EntityFrameworkCore.Update[30100]
      Saved 1 entities to in-memory store.
info: Microsoft.EntityFrameworkCore.Update[30100]
      Saved 1 entities to in-memory store.
info: Microsoft.EntityFrameworkCore.Update[30100]
      Saved 1 entities to in-memory store.
info: Microsoft.EntityFrameworkCore.Update[30100]
      Saved 1 entities to in-memory store.
info: Microsoft.EntityFrameworkCore.Update[30100]
      Saved 1 entities to in-memory store.
info: Microsoft.EntityFrameworkCore.Update[30100]
      Saved 1 entities to in-memory store.
```
