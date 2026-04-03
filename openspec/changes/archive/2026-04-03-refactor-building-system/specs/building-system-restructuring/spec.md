## ADDED Requirements

### Requirement: BuildingSystem Parent Structure
Each building prefab MUST have a root GameObject named `BuildingSystem`. This object SHALL serve as the container for all building-related components and the visual model.

#### Scenario: Building Prefab Hierarchy
- **WHEN** a building is instantiated
- **THEN** it has a root object named `BuildingSystem` containing the `EditBuilding` script and the building's visual models as children

### Requirement: Interaction Trigger
The `BuildingSystem` root object SHALL have a `BoxCollider` component with `isTrigger` set to true.

#### Scenario: Player Detection
- **WHEN** the player enters the `BuildingSystem` trigger
- **THEN** the `EditBuilding` script detects the player's presence, allowing interaction

### Requirement: Building State Toggle
The system MUST allow the player to toggle between "Building" (static) and "Editing" (hologram) states by pressing the 'C' key while inside the trigger.

#### Scenario: Toggle to Hologram
- **WHEN** the player is inside the trigger and presses 'C'
- **THEN** the static building model is hidden, and the hologram model is shown at the player's target position

#### Scenario: Toggle to Static
- **WHEN** the player is in hologram mode and presses 'C' at a valid location
- **THEN** the building parent moves to the hologram's position, the static model is shown, and the hologram is hidden
