## Context

The current brightness implementation relies on a UI `Image` component (a black panel with variable alpha) to simulate brightness. This panel is managed by `BrightnessManager.cs` and `BrightnessPanel.cs`. The use of `DontDestroyOnLoad` on a non-root UI object (inside a Canvas) causes Unity errors and prevents the panel from being persistent across scenes correctly. Furthermore, the UI-based approach is a legacy technique that doesn't integrate with modern Post-Processing pipelines.

## Goals / Non-Goals

**Goals:**
- Replace UI-based brightness with URP Post-Processing (Color Adjustments).
- Create a persistent global manager that handles brightness settings across all scenes.
- Ensure brightness is applied at startup by loading the last saved value.
- Fix the `DontDestroyOnLoad` issue by using a proper singleton pattern on a root GameObject.

**Non-Goals:**
- Implementing other Post-Processing effects (Bloom, Vignette, etc.) beyond what's needed for brightness.
- Refactoring the entire Options UI layout (only the brightness slider logic will be updated).

## Decisions

- **Decision 1: Use URP Volume for Brightness.** Instead of an alpha-blended UI panel, we will use a global `Volume` with `Color Adjustments`. Brightness will be controlled via the `Post Exposure` or `Contrast/Color Filter` settings (specifically `Post Exposure` for a natural brightness effect).
  - *Rationale:* Post-processing is more efficient, looks better, and is the standard way to handle screen-wide color/exposure adjustments in URP.
  - *Alternatives:* Continuing with UI panels (rejected due to persistence and visual quality issues).

- **Decision 2: Root-level Global Manager.** Create a `GlobalSettingsManager` (or refactor `BrightnessManager`) that exists at the root of the first scene (MainMenu) and uses `DontDestroyOnLoad`.
  - *Rationale:* To use `DontDestroyOnLoad`, the object must be a root object in the hierarchy.
  - *Alternatives:* Having a Canvas in every scene with the panel (redundant and hard to sync).

- **Decision 3: Use ScriptableObject for State (Optional but recommended).** 
  - *Rationale:* While PlayerPrefs is used for persistence, having a runtime state that UI and Post-Processing can both reference makes the system cleaner.
  - *Alternatives:* Directly reading/writing PlayerPrefs in every update (inefficient).

## Risks / Trade-offs

- **[Risk]** Post-Processing might not be enabled in all scenes or on the Main Camera. → **[Mitigation]** Ensure all scene cameras have "Post Processing" enabled and there is a Global Volume in the base scene or a persistent one.
- **[Risk]** Conflict with other Post-Processing effects. → **[Mitigation]** Use a dedicated high-priority volume for brightness or ensure it's integrated into the existing volume stack.
