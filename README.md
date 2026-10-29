# Drone vs Enemy Combat Simulation

![Engine](https://img.shields.io/badge/Engine-Unity-black?logo=unity&logoColor=white)
![Platform](https://img.shields.io/badge/Platform-Windows-0078D6?logo=windows&logoColor=white)
![Type](https://img.shields.io/badge/Type-Event%20Project-FF6B35)
![Org](https://img.shields.io/badge/Org-IPAGEUMS-6C5CE7)
![ShaderLab](https://img.shields.io/badge/ShaderLab-59.8%25-9E1F63)
![C%23](https://img.shields.io/badge/C%23-29.2%25-239120?logo=csharp&logoColor=white)
![HLSL](https://img.shields.io/badge/HLSL-11%25-1E90FF)
![Release](https://img.shields.io/badge/Latest-v1.0-brightgreen)
![Status](https://img.shields.io/badge/Status-Released-success)

> A 3D drone combat simulation built in Unity where the player pilots a drone against AI enemies that patrol via NavMesh and waypoints. Developed as an event project for **IPAGEUMS** and reused as an internship test submission, the simulation demonstrates enemy AI, a centralized input system, projectile combat, and a deliberate bug-fix pass on the physics/camera layer.

---

## Screenshots & Demo

<p align="center">
  <img width="1920" height="1080" alt="Screenshot (104)" src="https://github.com/user-attachments/assets/083ab58a-2e70-40f2-80a9-f518c26f1e4a" />
<img width="1920" height="1080" alt="Screenshot (106)" src="https://github.com/user-attachments/assets/8b79ab25-57dd-4016-82bb-0ee34b2b0a23" />
<img width="1920" height="1080" alt="Screenshot (107)" src="https://github.com/user-attachments/assets/e26373b5-6ef8-4a7a-b828-3f5dcf5cd5f1" />
<img width="1920" height="1080" alt="Screenshot (108)" src="https://github.com/user-attachments/assets/2e314cdb-55bb-4dbf-b58a-7d968a180f5f" />
<img width="1920" height="1080" alt="Screenshot (109)" src="https://github.com/user-attachments/assets/210636eb-19f6-444c-b5ee-375a974023ff" />
</p>

> Replace this block with in-game captures once available. Recommended format: a 1280×720 PNG or a short MP4/GIF loop showing the drone, patrolling enemies, and a missile hit.

**Demo Video:** [Watch on Google Drive](https://drive.google.com/file/d/1tOBkUSpqKls0rHYptmIGnxcUioetzmdG/view?usp=sharing)

---

## Gameplay

You pilot a drone inside a closed 50×50-unit environment. AI soldier enemies patrol using NavMesh and waypoints, and open fire when the player enters their engagement range. The player fires missiles to destroy enemies; enemies fire back. The simulation restarts on demand.

### Controls

| Input        | Action                          |
|--------------|---------------------------------|
| `W` `A` `S` `D` | Move Forward / Left / Backward / Right |
| `Q` / `E`    | Descend / Ascend                |
| `Space`      | Fire missile                    |
| `R`          | Restart simulation              |

---

## Key Features

- **NavMesh + Waypoint Enemy Patrol** — Enemies navigate the environment using Unity's NavMesh with waypoint routes, giving them believable patrol behavior without bespoke pathfinding code.
- **Range-Based Enemy Engagement** — Enemies detect the player within a configured range and begin firing. The engagement logic is decoupled from the patrol logic, so tuning aggression is a one-variable change.
- **Centralized Input System** — All player input (movement, ascend/descend, fire, restart) is routed through a single script. This keeps action mapping consistent, makes rebinding trivial, and avoids the scattered `Input.GetKey` calls that grow into tech debt in larger projects.
- **Projectile Combat with Hit Resolution** — Player-fired missiles destroy enemies on impact. Enemy-fired missiles can damage the player. Hit resolution is symmetric and uses the same collision pipeline for both sides.
- **Capped Flight Ceiling** — A maximum flight height prevents the drone from leaving the play space vertically, keeping engagements inside the designed combat area.

---

## Bugs Fixed

The simulation went through a deliberate stabilization pass. Each fix below addresses a real defect that surfaced during playtesting — listed here because the *kinds* of bugs fixed are often more telling to a reviewer than the features themselves.

1. **Player clipping through walls and ground** — fixed via collision constraints so the drone respects world geometry.
2. **Missiles not destroyed on player hit** — fixed so enemy projectiles resolve correctly on impact with the player.
3. **Enemy missiles self-destructing on spawn and killing the enemy** — fixed spawn-frame collision handling so a missile no longer collides with its own origin.
4. **Particle effects playing at object creation time** — fixed so VFX only trigger on intended events (impact, destruction), not on instantiation.
5. **Missiles unable to destroy the player** — fixed damage resolution so enemy projectiles apply damage correctly.
6. **Camera clipping through walls and obstacles** — fixed camera collision so the view stays inside valid space.

---

## Known Limitations

Documented honestly — these are scoped out, not unknown:

1. The player drone cannot rotate.
2. The player is bound to a closed-space environment of 50 × 50 units.
3. Enemies cannot fire missiles in upward or downward directions (engagement is horizontally constrained).

---

## Assets Used

| Asset                              | Author               |
|------------------------------------|----------------------|
| Particle Effect (Starter pack)     | Unity Technologies   |
| Missiles / Projectiles             | BenjaTheMaker        |
| Soldiers                           | Polygon Blacksmith   |
| Drone                              | GameAnime            |

---

## Built With

| Layer            | Technology                              |
|------------------|-----------------------------------------|
| Engine           | Unity                                   |
| Platform         | Windows                                 |
| Gameplay Code    | C#                                      |
| Shaders          | ShaderLab + HLSL                        |
| AI Navigation    | Unity NavMesh + Waypoints               |
| Input            | Centralized InputSystem (single script) |
| Focus Areas      | Enemy AI, Combat Resolution, Input Architecture, Stability Pass |

---

## Project Structure

```
Drone-vs-Enemy-Combat-Simulation/
├── Assets/                  # Game assets: scenes, scripts, shaders, art, audio
│   ├── Scenes/              # Unity scenes (simulation scene)
│   ├── Scripts/             # C# code (input system, drone controller, enemy AI, missiles)
│   ├── Shaders/             # ShaderLab + HLSL custom shaders
│   └── ...                  # Art, audio, prefabs, particle effects, settings
├── Packages/                # Unity package manifest and dependencies
├── ProjectSettings/         # Unity project configuration (graphics, physics, input, NavMesh)
├── .gitattributes           # Git LFS / line-ending rules
├── .gitignore               # Unity-aware ignore rules
└── .vsconfig                # Visual Studio workspace config
```

---

## How to Run

1. **Download** the latest release build from the [Releases](#releases) section below.
2. **Unzip** the downloaded archive to a folder of your choice.
3. **Run** the extracted executable.
4. Use **WASD** to move, **Q/E** to descend/ascend, **Space** to fire, and **R** to restart.

No additional dependencies are required — the build is self-contained for Windows. A demo video is available on [Google Drive](https://drive.google.com/file/d/1tOBkUSpqKls0rHYptmIGnxcUioetzmdG/view?usp=sharing).

---

## Releases

| Version           | Tag           | Date           | Highlights                                              |
|-------------------|---------------|----------------|---------------------------------------------------------|
| **v1.0** (latest) | `version-1.0` | Sep 23, 2026   | First build of the event project for IPAGEUMS. Windows. |

> The release ships a ready-to-play Windows build. Download the zip, extract, and run the executable.

---

## Purpose of This Project

Drone vs Enemy Combat Simulation was built to serve three overlapping goals:

1. **Event project for IPAGEUMS** — a complete, shipped 3D simulation delivered for the IPAGEUMS event, demonstrating end-to-end Unity development under a fixed brief.
2. **Internship test submission** — reused as a portfolio artifact for internship evaluation, showcasing AI, input architecture, and a structured bug-fix pass.
3. **Personal portfolio piece** — a public showcase of combat-AI and stability work, available at [github.com/stellar009/Drone-vs-Enemy-Combat-Simulation](https://github.com/stellar009/Drone-vs-Enemy-Combat-Simulation).



---

## Author

**stellar009** — Unity developer focused on AI, combat resolution, and stability-driven iteration.
GitHub: [@stellar009](https://github.com/stellar009) · Repository: [Drone-vs-Enemy-Combat-Simulation](https://github.com/stellar009/Drone-vs-Enemy-Combat-Simulation)

---

> *Drone vs Enemy — NavMesh AI, centralized input, shipped stable.*
