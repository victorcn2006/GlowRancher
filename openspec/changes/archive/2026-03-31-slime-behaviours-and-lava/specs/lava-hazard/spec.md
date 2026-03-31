## ADDED Requirements

### Requirement: Lava Lethality
The Lava SHALL deal high damage or kill the player and non-immune slimes upon contact.

#### Scenario: Player enters lava
- **WHEN** the player's collider enters a Lava trigger
- **THEN** it deals lethal damage (maxHealth) to the player
- **AND** the player's death or respawn logic is triggered

#### Scenario: Non-immune slime enters lava
- **WHEN** a slime without lava immunity enters a Lava trigger
- **THEN** it is destroyed or takes lethal damage

#### Scenario: Rock Slime enters lava
- **WHEN** a Rock Slime enters a Lava trigger
- **THEN** no damage is applied
- **AND** it can traverse the lava safely
