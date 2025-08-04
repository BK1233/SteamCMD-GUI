# Copilot Instructions

## Overview

This is a Visual Basic .NET Windows Forms application that provides a graphical user interface (GUI) for the SteamCMD command-line tool. The application is built using .NET Framework 4.8.

## Architecture

The application follows a modular architecture, with the main UI logic in `MainMenu.vb` and other functionalities separated into manager classes:

*   `RconClient.vb`: Handles RCON communication with game servers.
*   `BackupManager.vb`: Manages server backups and restores.
*   `WorkshopManager.vb`: Interacts with the Steam Workshop.
*   `ServerManager.vb`: Manages the game server process.
*   `UpdateManager.vb`: Handles game server updates.

The main form, `MainMenu.vb`, instantiates these manager classes and responds to their events.

## Key Files

*   `Source/SteamCMD GUI/MainMenu.vb`: The main form and central controller of the application.
*   `Source/SteamCMD GUI/SteamCMD GUI.vbproj`: The project file. Note that this project targets .NET Framework 4.8.
*   `Source/SteamCMD GUI/My Project/AssemblyInfo.vb`: Contains the application's version number.
*   `Source/SteamCMD GUI/Resources/`: Contains resource files, including localized strings.

## Development Workflow

### Building the Project

This project is a .NET Framework application and must be built on a Windows machine with Visual Studio and the .NET Framework 4.8 Developer Pack installed. The build will fail in environments without these dependencies.

### Versioning

The application's version is defined in `Source/SteamCMD GUI/My Project/AssemblyInfo.vb`. When preparing a new release, this is the only file that needs to be updated with the new version number.

### Localization

UI strings are stored in resource files located in `Source/SteamCMD GUI/Resources/`. The `ApplyLocalizedStrings` method in `MainMenu.vb` is responsible for loading these strings and applying them to the UI controls.

## Conventions and Patterns

*   **Manager Classes**: All major functionalities are encapsulated in their own manager classes. These classes are instantiated in `MainMenu.vb` and communicate with the main form through events.
*   **Event-Driven**: The application is heavily event-driven. The main form subscribes to events raised by the manager classes to update the UI and respond to user actions.
*   **Settings Persistence**: User settings are saved and loaded using `My.Settings`. The `LoadSettings` and `SaveSettings` methods in `MainMenu.vb` handle this functionality.
