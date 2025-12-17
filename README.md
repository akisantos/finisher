# ⚔️ FINISHER

> **A High-Fidelity Character Controller & Combat System built with Unity.**

## 🎮 Gameplay Overview

**Finisher** is a third-person action template designed for fluidity and responsiveness. The core gameplay loop revolves around mastering momentum-based movement and precise combat interactions.

### 🏃 Advanced Locomotion System
The character is not just a capsule moving around; it's a physics-driven entity with realistic weight and inertia.

* **Grounded Movement:**
    * **Variable Speeds:** Seamless transitions between Walking, Running, and Sprinting based on input intensity.
    * **Inertia Stops:** The character doesn't stop instantly. Depending on the speed (Walk vs. Sprint), different stopping animations and deceleration forces (Light, Medium, Hard Stop) are applied.
    * **Sliding:** Crouch-slide mechanics to maintain momentum while dodging high attacks or navigating under obstacles.
    * **Dashing:** A quick burst of speed for evasive maneuvers.

* **Airborne Physics:**
    * **Variable Jump Height:** Tap for a hop, hold for a high jump.
    * **Air Control:** Limited influence over trajectory while airborne for precise platforming.
    * **Gravity Modifiers:** Custom fall gravity multipliers for a "snappy" landing feel (no floaty physics).

### ⚔️ Combat Mechanics (WIP)
The combat system is built to be modular and scalable, focusing on melee engagements.

* **Weapon State Management:**
    * **Unarmed / Armed Modes:** Distinct state handling for when the weapon is sheathed vs. drawn.
    * **Sword Handling:** Dedicated logic for weapon equipping and grip animations.
* **Attacking:**
    * **Melee Combos:** Support for multi-stage attack chains (e.g., Light -> Light -> Heavy).
    * **Input Buffering:** (Planned) Allows for smoother combo execution by registering button presses slightly before the current animation ends.

---

## 🛠️ Technical Architecture

This project is not just about gameplay; it's a showcase of **Clean Code** and **Scalable Architecture**.

### 1. Finite State Machine (FSM)
Logic is strictly decoupled into discrete states, preventing "Spaghetti Code."
* **Movement States:** `PlayerGroundedState`, `PlayerAirborneState`, `PlayerDashState`, `PlayerSlideState`.
* **Combat States:** `SwordHoldingState`, `SwordAttackingState`.

### 2. Data-Driven Design (ScriptableObjects)
Designers can tweak gameplay feel without touching a single line of code. All key parameters are exposed via **ScriptableObjects**:
* `PlayerSO`: Base stats.
* `PlayerGroundedData`: Walk/Run speeds, slope limits.
* `PlayerAirborneData`: Jump force, fall multipliers.
* `PlayerCombatData`: Damage values, attack cooldowns.

---

## 📂 Installation

1.  **Clone the Repo:**
    ```bash
    git clone [https://github.com/akisantos/FirstMovementStateMachine.git](https://github.com/akisantos/FirstMovementStateMachine.git)
    ```
2.  **Open in Unity:** (Recommended Version: 2022.3 LTS or higher).
3.  **Play:** Open `Scenes/Playground` and hit Play.

or download executable file directly from itch.io here: https://akkii0609.itch.io/finisher-level-up-phase
## 🕹️ Controls

| Action | Input (Keyboard/Mouse) |
| :--- | :--- |
| **Move** | `W`, `A`, `S`, `D` |
| **Jump** | `Space` |
| **Sprint/Walk** | Toggle `Left Shift` |
| **Slide** | Hold `Left Ctrl` (while moving) |
| **Attack** | `Left Mouse Button` |
| **Throw melee/ Recall** | `F` or `Right Mouse Button` |

---
## 🙏 Acknowledgements

Special thanks to **Indie Wafflus** for the amazing tutorial series on State Machine Architecture.
This project was built upon the core concepts shared in the video, with additional customizations for:
* Slide mechanics
* ScriptableObject integration

🎥 **Original Tutorial:** https://www.youtube.com/watch?v=kluTqsSUyN0

## 📝 License

This project is created for educational purposes. Feel free to use it as a reference for your own FSM implementations.

---
*Maintained by [akistd](https://github.com/akisantos)*
