## ADDED Requirements

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
