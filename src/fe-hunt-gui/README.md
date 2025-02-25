# GUI Scavenger Hunt

Welcome to the **GUI Scavenger Hunt**! This README will guide you through the setup process and help you get started.
---

## Setup Instructions

```diff - If you want to Automaticly install the Game Hunt please visit: [Installationsanleitung.pdf](../../demos/Installationsanleitung.pdf). @@

Follow these steps to prepare your environment:

### 1. Create the Hunt Folder Structure
1. Navigate to your home directory.
2. Locate the `ProgramData` folder (you need to have hidden elements in your folder structure on).
3. Inside the `ProgramData` folder, create a new folder named `Hunt`.

### 2. Clone the Repository
1. Open a terminal or Git-enabled command prompt.
2. Change your current working directory to the `Hunt` folder you created:
   ```bash
   cd ~/ProgramData/Hunt
   ```
3. Clone the GUI Scavenger Hunt repository into the `Hunt` folder using the following command:
   ```bash
   git clone "https://github.com/BrrrUzi/HSAA_Projektarbeit.git"
   ```
---

## File Structure
After completing the setup, your file structure should look like this:

```
Home Directory
├── ProgramData
    ├── Hunt
        ├── Logfile *
        |   └── logfile.txt *
        ├── HSAA_Projektarbeit
	     ├── src
	          ├── fe-hunter-gui
			├── bin
				├── Debug
				|   └── fe-hunt-gui.exe
```
* Will be automaticly created on first Start)

---

## Running the GUI Scavenger Hunt
Once the setup is complete:
1. Locate and run the file "fe-hunt-gui.exe" in "C:\ProgramData\Hunt\HSAA_Projektarbeit\src\fe-hunt-gui\bin\Release" provided in the repository to start the scavenger hunt.

---

## Troubleshooting
If you encounter any issues:
1. Verify that the folder structure matches the specified setup.
2. Ensure you have the necessary permissions to create folders and files in the `ProgramData` directory.
3. Confirm that Git is installed and properly configured on your system.
4. Check your internet connection if cloning the repository fails.

For additional support, refer to the documentation provided in the repository or contact the project maintainer.

---

Enjoy your scavenger hunt!

