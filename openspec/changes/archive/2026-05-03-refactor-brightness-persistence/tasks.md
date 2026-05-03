## 1. Cleanup & Preparation

- [x] 1.1 Remove `BrightnessPanel.cs` as its logic will be merged into the new persistent manager.
- [x] 1.2 Identify and remove the legacy UI `BrightnessPanel` Image from the `Options_Redesign.unity` and any other scenes (to be handled in code if possible to avoid scene corruption, but primarily we will stop referencing it).

## 2. Global Settings Manager Implementation

- [x] 2.1 Refactor `BrightnessManager.cs` to be a root-level Singleton that survives scene loads.
- [x] 2.2 Add URP Post-Processing namespace and volume control logic to `BrightnessManager.cs`.
- [x] 2.3 Implement logic to find or create a global `Volume` component on startup.
- [x] 2.4 Add `Color Adjustments` override handling to control `Post Exposure`.

## 3. Persistence & UI Integration

- [x] 3.1 Update `Load` logic to retrieve brightness from `PlayerPrefs` at the very beginning of the game.
- [x] 3.2 Update `Update_brightnessPanel` (rename to `UpdateBrightness`) to modify the Post-Processing volume instead of a UI Image.
- [x] 3.3 Ensure the Slider in the Options menu correctly re-links to the new persistent `BrightnessManager` instance.

## 4. Verification

- [ ] 4.1 Verify that brightness persists when moving from Main Menu to a gameplay scene.
- [ ] 4.2 Verify that the `DontDestroyOnLoad` error is resolved by ensuring the manager is a root object.
- [ ] 4.3 Verify that brightness settings are saved and loaded correctly between game sessions.
