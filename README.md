# Einführung Repository `projektarbeit-hsaa`
Repository für die Projektarbeit "AugmentedReality-Schnitzeljagd" von Lukas und Rosario an der Hochschule Aalen.

# Verzeichnisstruktur

- `.\docs\` beinhaltet Dokumente, wie beispielsweise die Dokumentation der Projektarbeit, Literatur, etc.
- `.\libs\` beinhaltet Bibliotheken, die im Projekt Verwendung finden, wie beispielsweise `.dll` Dateien
- `.\src` beinhaltet den Source-Code des Projekts, wie etwa C# Code, etc.

# Inbetriebnahme der Anwendung(en)

### Testen der REST-APi / Schnittstelle

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

### Inbetriebnahme Anwendung 1 TODO

1) Todo.
1) Todo.
1) Todo.

### Inbetriebnahme Anwendung 2 TODO

1) Todo.
1) Todo.
1) Todo.
