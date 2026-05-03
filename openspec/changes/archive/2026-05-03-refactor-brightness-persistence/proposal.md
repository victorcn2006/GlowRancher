## Why

The current brightness management system uses a UI panel with `DontDestroyOnLoad` that fails when it's not a root object. Additionally, the brightness effect is applied via UI alpha blending, which is less performant and visually consistent than Post-Processing. We need a persistent system that loads from the first scene and applies brightness globally using URP Post-Processing.

## What Changes

- Implement a global, persistent Brightness Manager that survives scene loads.
- Move brightness adjustment from UI Overlay to URP Post-Processing (Color Adjustments).
- Centralize brightness state and persistence (PlayerPrefs).
- Ensure the brightness effect is applied immediately upon game start and remains consistent across all scenes.

## Capabilities

### New Capabilities
- `brightness-postprocessing`: Implementation of brightness control using URP Post-Processing volumes.
- `global-settings-persistence`: A persistent manager system for handling global game settings like brightness.

### Modified Capabilities
<!-- Existing capabilities whose REQUIREMENTS are changing (not just implementation).
     Only list here if spec-level behavior changes. Each needs a delta spec file.
     Use existing spec names from openspec/specs/. Leave empty if no requirement changes. -->

## Impact

- `BrightnessManager.cs`: Will be refactored to interface with Post-Processing instead of a direct UI Image reference.
- `BrightnessPanel.cs`: Likely redundant or will be transformed into a global Post-Processing controller.
- All scenes: Will now be affected by the global Post-Processing brightness setting.
- UI Options: Slider in `Options_Redesign.unity` will need to link to the new persistent manager.
