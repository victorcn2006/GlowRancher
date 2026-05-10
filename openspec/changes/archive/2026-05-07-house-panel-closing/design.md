# Implementation Plan - House UI Panel Closing

Implement a UI panel management system for the `House` building, following the pattern used in the `InteractiveShop` system. This includes opening the panel on interaction, closing it with the pause key or interaction key (when already open), pausing the game, and managing camera/cursor states.

## User Preferences
- **Panel Reference Type:** `HouseShopController`
- **Camera Control:** Yes, include `PlayerCameraMovement` reference.

## Proposed Changes

### 1. `Assets/Code/Scripts/Shop/HouseShopController.cs`
- Add basic activation/deactivation methods to match the `PanelShopController` pattern.
- Add a reference to the actual panel `GameObject`.

### 2. `Assets/Code/Scripts/Buildings/House.cs`
- Update the class to handle the full lifecycle of the UI panel.
- **Fields:**
    - `_houseUIContainer` (GameObject) - The root UI container.
    - `_houseShopPanel` (HouseShopController) - The controller for the house shop.
    - `_cameraControl` (PlayerCameraMovement) - Reference to control camera/cursor.
    - `_isPanelActive` (bool) - Tracking state.
    - `_timeSinceLastToggle` (float) and `_toggleCooldown` (const float) - To prevent rapid toggling.
- **Methods:**
    - `OnEnable`/`OnDisable`: Subscribe/Unsubscribe to `InputManager` events (`OnInteractPerformed`, `OnPausePerformed`).
    - `OnInteract`: Call `OpenPanel`.
    - `HandleKeyboardToggle`: Close the panel if it's already active (called via `OnInteractPerformed`).
    - `OpenPanel`: Activate UI, pause game, unlock cursor, disable camera control.
    - `ClosePanel`: Deactivate UI, resume game, lock cursor, enable camera control.
    - `UpdateGameState`: Helper to centralize game state changes.
    - `ToggleDelay`: Coroutine to handle the cooldown.

## Verification & Testing

### Manual Testing (in Unity Editor)
1. **Assign References:** Ensure `House` object in the scene has references to the UI container, `HouseShopController`, and the `PlayerCameraMovement` (usually on the Player/Camera).
2. **Interaction:** Walk up to the house and interact. The panel should open.
3. **Game State:** Verify time is paused (e.g., animations stop) and cursor is visible/free.
4. **Closing (Interact Key):** Press the interaction key while the panel is open. It should close.
5. **Closing (Pause Key):** Press the pause/esc key while the panel is open. It should close.
6. **Resuming State:** Verify time resumes and camera control is restored after closing.
7. **Cooldown:** Rapidly pressing the interact key shouldn't cause flickering.
