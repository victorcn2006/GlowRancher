## ADDED Requirements

### Requirement: Scared Slime Fleeing Behavior
The Scared Slime SHALL flee from the player when they are within detection range.

#### Scenario: Player enters detection range
- **WHEN** the player comes within the detection radius of a Scared Slime
- **THEN** the slime calculates a direction directly away from the player's position
- **AND** the slime moves in that direction until the player is outside the detection range or it hits an obstacle

#### Scenario: Player exits detection range
- **WHEN** the player moves outside the detection radius of a Scared Slime
- **THEN** the slime resumes its normal wandering or idle behavior
