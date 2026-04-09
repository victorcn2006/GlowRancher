# Slime Animation Standards

This specification defines the standard animator settings for Slime entities to ensure consistent responsiveness and visual polish across all types.

## 🏃 Transition Responsiveness

To ensure slimes react immediately to gameplay events (like jumping or eating), specific transition rules must be followed.

### 1. Idle → Action (Jump, Eat, Attack)
Transitions from a looping state (Idle) to an immediate action state must be instantaneous in logic but smooth in visuals.

*   **Has Exit Time**: `false` (Unchecked)
    *   *Rationale*: Prevents the slime from waiting for the Idle animation loop to finish before acting.
*   **Transition Duration**: `0.1s`
    *   *Rationale*: Provides a tiny blend to prevent "teleporting" frames while maintaining high responsiveness.

### 2. Action → Idle (Recovery)
Transitions from an action back to Idle should prioritize visual smoothness.

*   **Has Exit Time**: `true` (Checked)
    *   *Rationale*: Allows the action animation (like the landing of a jump) to complete its natural motion.
*   **Transition Duration**: `0.25s`
    *   *Rationale*: Creates a soft blend between the action's end-pose and the Idle loop, avoiding robotic snaps.

## 🛠️ Implementation Checklist
- [ ] `Has Exit Time` disabled for all trigger-based actions.
- [ ] `Transition Duration` set to `0.25s` for all transitions returning to `Idle`.
- [ ] Animator parameters match the standard set: `Jump` (Bool), `Eat` (Bool), `DropGem` (Bool).
