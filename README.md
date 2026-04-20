# JU Doctors League - Unity Trivia Game

## Overview

This project is a Unity-based trivia game developed for the Jordan University's annual medical championship.

Players progress through multiple stages of medical quiz rounds, answer timed multiple-choice questions, and receive immediate feedback with audio and UI transitions.

## ScreenShots
<p align="center">
	<img width="380" alt="Screenshot 2026-04-20 063520" src="https://github.com/user-attachments/assets/77296ee4-c091-489f-8214-77d5f7632e72" />
	<img width="380" alt="Screenshot 2026-04-20 063526" src="https://github.com/user-attachments/assets/542b04a8-fb35-4376-b98e-4421faeb0e5d" />

</p>

<p align="center">
  <img width="380" alt="Screenshot 2026-04-20 063533" src="https://github.com/user-attachments/assets/107d768e-1827-409f-adee-b2d9d6d21ddc" />
  
</p>

<p align="center">
    <img width="380" alt="Screenshot 2026-04-20 063602" src="https://github.com/user-attachments/assets/e8fed93b-a9ef-4991-9479-081c9279bcfa" />
  <img width="380" alt="Screenshot 2026-04-20 063556" src="https://github.com/user-attachments/assets/ebac2922-cea0-48b4-b072-178ee5f0a88e" />
</p>


## Project Highlights

- Multi-stage quiz experience with dedicated scenes for menu, stages, and stage transitions.
- Timed question rounds with timeout handling.
- Category/point-based question flow in early stages.
- Final stage with separate UI and question manager.
- Audio feedback system for correct, wrong, timeout, timer, and transitions.
- Support for text and image-based questions.

## Tech Stack

- Engine: Unity 2021.3.33f1 (LTS)
- Language: C#
- UI: Unity UI + TextMeshPro

## Game Flow

1. Player starts from the main menu.
2. Player navigates to quiz stages.
3. A question appears with answer options and optional image support.
4. Timer runs for each question.
5. Player answer is evaluated and feedback is shown.
6. Game moves to the next question/round/scene.

## Scenes

- `main menu.unity`
- `stages.unity`
- `stage 1.unity`
- `stage 2.unity`
- `stage 3.unity`

Location: `Assets/Scenes/`

## Core Scripts

Location: `Assets/scripts/`

- `QuizManager.cs`
	- Handles question pools for stage 1 and stage 2.
	- Selects questions, tracks correctness, and exposes the right answer.

- `UI_manager.cs`
	- Controls stage 1/2 quiz UI states.
	- Displays questions/options and updates remaining question counters.

- `Stage3Manager.cs`
	- Manages final stage question selection and answer validation.

- `Stage3UI.cs`
	- Controls final stage UI flow, question presentation, and answer feedback.

- `Timer.cs`
	- Handles countdown logic, timer visuals, timeout transitions, and timer audio.

- `SoundManager.cs`
	- Singleton audio manager for SFX playback.

- `MySceneManager.cs`
	- Provides scene loading and quit actions.

## Folder Structure (Important Parts)

```text
JU doctors League/
|- Assets/
|  |- Scenes/
|  |- scripts/
|  |- sounds/
|  |- sprites/
|  |- Resources/
|  |- Text/
|  |- questions photos/
|- Packages/
|- ProjectSettings/
```

## Configuration Notes

- Questions are configured in serialized lists inside quiz manager components:
	- `QuizManager` for stage 1/2 question pools.
	- `Stage3Manager` for final stage questions.
- Question images are optional and shown through the UI image toggle.
- Ensure audio clips are assigned in the `SoundManager` inspector.
- Ensure scene indices/build settings are configured for scene navigation.

## Project Purpose

This game was built to support an engaging, competitive, and educational trivia format for Jordan University's medical championship event.
