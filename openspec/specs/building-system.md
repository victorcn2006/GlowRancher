# Building System

This specification defines the architectural and behavioral requirements for the building placement and editing system.

## 🏗️ Hierarchy and Structure

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

## 🛠️ Editing and Placement

### Requirement: Building State Toggle
The system MUST allow the player to toggle between "Building" (static) and "Editing" (hologram) states by pressing the 'C' key while inside the trigger.

#### Scenario: Toggle to Hologram
- **WHEN** the player is inside the trigger and presses 'C'
- **THEN** the static building model is hidden, and the hologram model is shown at the player's target position

#### Scenario: Toggle to Static
- **WHEN** the player is in hologram mode and presses 'C' at a valid location
- **THEN** the building parent moves to the hologram's position, the static model is shown, and the hologram is hidden

### Requirement: Hologram Movement
While in "Editing" (hologram) mode, the building's hologram MUST follow the player's aim, projected onto the ground layer within a defined distance.

#### Scenario: Follow Player Aim
- **WHEN** the player moves the camera in hologram mode
- **THEN** the hologram follows the point where the camera ray hits the ground

### Requirement: Obstruction Detection
The system MUST detect if the hologram's current position overlaps with other building colliders or environmental obstructions.

#### Scenario: Valid Placement
- **WHEN** the hologram does not collide with any restricted objects
- **THEN** the placement status is valid, and the visual feedback reflects this

#### Scenario: Invalid Placement
- **WHEN** the hologram collides with another building or obstruction
- **THEN** the placement status is invalid, and the visual feedback reflects this

### Requirement: Visual Feedback
The hologram's material MUST change to reflect the placement validity.

#### Scenario: Invalid Visual Feedback
- **WHEN** the placement status is invalid
- **THEN** the hologram's material changes to a pre-defined red "invalid" material

### Requirement: Blocking Invalid Placement
The system SHALL prevent the player from finalizing building placement if the current position is invalid.

#### Scenario: Prevent Placing on Obstruction
- **WHEN** the player attempts to finalize placement (pressing 'C') while the status is invalid
- **THEN** the building remains in hologram mode, and an "Invalid placement" message is logged
