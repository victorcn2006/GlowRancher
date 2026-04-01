## ADDED Requirements

### Requirement: Corrupt Slime Aggressive Behavior
The Corrupt Slime SHALL chase and attack the player when they are within detection range.

#### Scenario: Player enters detection range
- **WHEN** the player comes within the detection radius of a Corrupt Slime
- **THEN** the slime moves towards the player's current position
- **AND** it attempts to reach the player to deal damage

#### Scenario: Corrupt Slime touches Player
- **WHEN** the Corrupt Slime's collider touches the player's collider
- **THEN** it deals damage to the player
- **AND** it may play an aggressive visual or audio effect
