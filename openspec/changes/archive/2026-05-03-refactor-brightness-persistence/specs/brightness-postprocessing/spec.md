## ADDED Requirements

### Requirement: Global Brightness Volume
The system MUST include a global Post-Processing Volume that exists across all scenes to apply brightness adjustments.

#### Scenario: Volume Persistence
- **WHEN** the game starts in the Main Menu
- **THEN** a global Post-Processing Volume is instantiated and marked with `DontDestroyOnLoad`

### Requirement: Post-Processing Brightness Control
The system SHALL use the `Post Exposure` parameter of the `Color Adjustments` effect to control screen brightness.

#### Scenario: Adjusting Brightness
- **WHEN** the user changes the brightness slider in the options menu
- **THEN** the `Post Exposure` value in the global volume is updated immediately

### Requirement: Persistent Brightness Setting
The brightness level MUST be saved to `PlayerPrefs` and loaded automatically when the game starts.

#### Scenario: Loading saved brightness
- **WHEN** the game is launched
- **THEN** the system loads the brightness value from `PlayerPrefs` and applies it to the global volume before the first scene is fully visible

### Requirement: Root-Level Singleton Manager
The `BrightnessManager` MUST be a root-level GameObject to correctly support `DontDestroyOnLoad`.

#### Scenario: Singleton Enforcement
- **WHEN** a second instance of the `BrightnessManager` is created (e.g., by reloading the main menu)
- **THEN** the new instance MUST be destroyed to maintain a single global manager
