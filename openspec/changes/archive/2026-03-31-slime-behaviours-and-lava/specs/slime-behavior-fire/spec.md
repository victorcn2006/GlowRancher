## ADDED Requirements

### Requirement: Fire Slime Damage Behavior
The Fire Slime SHALL deal damage to the player upon contact.

#### Scenario: Player touches Fire Slime
- **WHEN** the player's collider touches a Fire Slime's collider
- **THEN** the player receives a pre-defined amount of damage
- **AND** a visual or audio cue is played to indicate damage

#### Scenario: Fire Slime touches a non-player IDamageable
- **WHEN** a Fire Slime touches an object that implements IDamageable but is not the player
- **THEN** it MAY deal damage if the target is not specifically immune to fire
