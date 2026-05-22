# ⏳ ChronoShift
### *Control Time, Solve the Mystery*

> A third-person puzzle-action game built in **Unity 6 (URP)** where shifting between past and future is the core mechanic. Evade an AI-driven mummy, collect cubes, and uncover what lies at the edge of time.

---

## 📋 Table of Contents

- [About](#about)
- [Gameplay](#gameplay)
- [Levels](#levels)
- [Controls](#controls)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Installation](#installation)
- [Build](#build)
- [Team](#team)

---

## About

ChronoShift is a single-player, third-person puzzle-action game developed as a university project using **Unity 6**. The player wields a time-shift device to switch between the past and the future — different objects exist in each era, turning time itself into both a tool and a threat.

Inspired by *Portal* and *Braid*, ChronoShift delivers its own take on the past-future duality across three hand-crafted levels, each with a distinct gameplay style.

---

## Gameplay

| Concept | Description |
|---|---|
| **Time Shift** | Press `Q` to toggle between past and future. Objects, walls, and platforms change instantly. |
| **Survival** | In Level 2, the time-shift is disabled. Run, navigate, and reach the portal before the mummy catches you. |
| **Puzzle** | In Level 3, collect 4 yellow cubes scattered across a geometric room and return to the goal zone. |

---

## Levels

### Level 1 — Introduction
A simple corridor environment where the player learns the basic controls and the time-shift mechanic. The objective is to reach the **FinishGate**.

### Level 2 — Mummy Section 🏺
An Egypt-themed ancient corridor. Once the scenario is triggered:
- The **Q key is disabled** — no time shifting allowed.
- **Mummy_Mon** activates and chases the player using NavMesh AI.
- The player must reach **FinishGate1** without being caught. Getting caught resets the entire scenario.

### Level 3 — Puzzle Section 🧩
A geometric puzzle room where jumping is disabled.
- Collect **4 yellow cubes** spread across the map.
- Avoid **blue spheres** — contact sends you back to spawn.
- Return to the **GoalZone** to win.

---

## Controls

| Key | Action |
|---|---|
| `WASD` | Move |
| `Mouse` | Rotate camera |
| `Shift` | Sprint |
| `Space` | Jump *(disabled in Level 3)* |
| `Q` | Time shift *(disabled during mummy chase)* |
| `E` | Close story panel / interact |

---

## Features

- ⏱️ **Real-time time shift** — instant past/future object switching via `Q`
- 🎥 **Cinemachine** third-person camera with shoulder offset
- 🤖 **NavMesh AI enemy** with arc jumps, stuck detection, and catch-reset logic
- 📜 **Typewriter narrative panel** with fade-in/out sequencing
- 🔊 **Full audio system** — footsteps, sprint, jump, transition, and ambient music (DontDestroyOnLoad)
- 🧱 **Three distinct levels** with unique mechanics and atmosphere
- 🏆 **Win / lose conditions** with proper UI feedback
- 🎮 **Unity New Input System** integration

---

## Tech Stack

| Tool | Version / Detail |
|---|---|
| **Engine** | Unity 6 — `6000.3.11f1` |
| **Render Pipeline** | Universal Render Pipeline (URP) |
| **Camera** | Cinemachine 3rd Person Follow |
| **Physics** | CharacterController + NavMeshAgent |
| **Input** | Unity New Input System |
| **Base Movement** | Starter Assets Third Person Controller |
| **Language** | C# |
| **Platform** | PC — Windows |

---

## Project Structure

```
Assets/
├── Scenes/
│   ├── MainMenu.unity
│   ├── Level1.unity
│   ├── Level2.unity
│   └── Level3.unity
├── Scripts/
│   ├── AudioManager.cs
│   ├── PlayerSoundController.cs
│   ├── TimeManager.cs
│   ├── TimeTransitionEffect.cs
│   ├── NarrativePanel.cs
│   ├── MummyFollow.cs
│   ├── MummyCatch.cs
│   ├── QuestionMarkTrigger.cs
│   ├── LevelTransition.cs
│   ├── CollectibleCube.cs
│   ├── GoalZone.cs
│   ├── GameManager3.cs
│   ├── SpawnManager.cs
│   ├── SphereRespawn.cs
│   ├── SphereMover.cs
│   ├── DisableJump.cs
│   └── MainMenuUI.cs
├── Audio/
│   ├── arkaPlan.mp3
│   ├── adim.mp3
│   ├── kosma.mp3
│   ├── ziplama.mp3
│   ├── soruis.mp3
│   ├── gecis.mp3
│   └── teleport.mp3
└── Materials/
```

---

## Installation

> **Requirements:** Unity 6 (`6000.3.11f1`) with URP and Cinemachine packages installed.

1. Clone the repository:
   ```bash
   git clone https://github.com/YOUR_USERNAME/ChronoShift.git
   ```
2. Open the project in **Unity Hub** using Unity version `6000.3.11f1`.
3. Let Unity import all assets and packages.
4. Open `Assets/Scenes/MainMenu.unity` as the starting scene.
5. Press **Play**.

---

## Build

To build for Windows:

1. Go to `File → Build Settings`
2. Ensure the scene order is:

   | Index | Scene |
   |---|---|
   | 0 | `Assets/Scenes/MainMenu.unity` |
   | 1 | `Assets/Scenes/Level1.unity` |
   | 2 | `Assets/Scenes/Level2.unity` |
   | 3 | `Assets/Scenes/Level3.unity` |

3. Select **PC, Mac & Linux Standalone** → **Windows**
4. Click **Build**

---

*ChronoShift — Game Design Document v1.00 — 2026*
