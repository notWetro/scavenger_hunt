# Project AR Scavenger Hunt

## Introduction

This repository contains the system for a web-based scavenger hunt developed as part of a project and its extensions.

**Authors**: Lukas Steckbauer, Rosario Aranzulla, Felix Kurz, Felix Biedenbacher, Niklas Fichtner and Hikmet Gözaydin

**Supervisor**: Dr. Marc Hermann

**Institution**: _Aalen University, Faculty of Computer Science and Electrical Engineering_


### Project Quick Links

Each subdirectory contains a README file explaining the purpose and usage of the respective folder.

For an overview, here are the tasks of the linked folders:

- **Backend** and **API** [Hunt-API](src/be-hunt-api/README.md): As the name suggests, this folder contains the backend. It includes the logic for managing games and players as well as the REST APIs. Additionally, the Node server is managed here.
- **Web-Game** [Hunt-Game](src/fe-hunt-web-game/README.md): This folder contains the game's frontend, where scavenger hunts can be played and joined.
- **Web-App** [Hunt-Editor](src/fe-hunt-editor/README.md): This web application is used to create and manage scavenger hunts.
- **Hunt-GUI** [Hunt-GUI](src/fe-hunt-gui/README.md): This tool provides an easy way to manage the backend and both frontend applications. With the GUI, all web applications can be accessed centrally without having to manually start, for example, Docker containers.

### Documentation

The documentation of the project work can be found in the `./docs/` directory [here](docs/).

### Demos

The [demos](demos/) directory contains:
- The [videos](demos/videos) subfolder with **demo videos** showing how to use the application.
- The [demo](demos/demo_hunt) subfolder with **videos, images, audio files, texts, QR codes, and locations**, including instructions for creating a scavenger hunt for first-year students.
- The [images](demos/images) subfolder containes images and screenshots of the application.
- Pdf with manual and game instructions.
- Pdf with installation instructions.

### Utilities

For testing purposes (and for generating QR codes within the project), a utility was created that generates a QR code based on an input string via a command-line call. The resulting QR code is saved in the user's `Downloads` folder and is identifiable by a timestamp (e.g., `QRCode_20240323113550.png`). The utility can be found [here](utils/qrcode-generator/).

## Function and Usage of the Game

To create a scavenger hunt, we recommend using the installer [installation Guide](demos/Installationsanleitung.pdf). However, if you prefer to create a hunt without a GUI, you can find instructions in the [Hunt-Editor](src/fe-hunt-editor/README.md) folder.

To start a game, the [Hunt-API](src/be-hunt-api/README.md) must be running to manage the data – this is automatically handled when using the GUI.

To play a hunt, the QR code or link generated in the editor can be used. A created hunt can also be tested via the GUI or the [Hunt-Game](src/fe-hunt-web-game/README.md).
