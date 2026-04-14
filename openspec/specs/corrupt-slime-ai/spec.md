# corrupt-slime-ai Specification

## Purpose
Specification for the behavior of Corrupt Slimes, focusing on aggressive pursuit and predation.
## Requirements
### Requirement: Corrupt Slime Aggressive Pursuit
The `CorruptSlime` SHALL prioritize pursuing the Player or entities with the `IEatable` interface over random wandering, using a dynamic jump force when near the target.

#### Scenario: Target detected within pursuit range
- **WHEN** a Player or `IEatable` entity is within detection range but beyond the slime's `JUMP_DISTANCE`
- **THEN** the `CorruptSlime` jumps towards the target's position using full `JUMP_FORCE`.

#### Scenario: Target detected at close range
- **WHEN** a Player or `IEatable` entity is within the slime's `JUMP_DISTANCE`
- **THEN** the `CorruptSlime` jumps towards the target's position with horizontal force scaled down proportionally to the distance.

### Requirement: Corrupt Slime Random Movement
The `CorruptSlime` SHALL perform random jumps only when no Player or `IEatable` targets are detected.

#### Scenario: No target detected
- **WHEN** no Player or `IEatable` entity is in range
- **THEN** the `CorruptSlime` performs jumps in random horizontal directions.

### Requirement: Removal of Food Seeking
The `CorruptSlime` SHALL NOT seek or move towards standard "Food" objects (e.g., fruits, vegetables).

#### Scenario: Food object nearby
- **WHEN** a "Food" object is near a `CorruptSlime`
- **THEN** the `CorruptSlime` ignores it and continues its current behavior (pursuit or random).

### Requirement: Predation Attack
The `CorruptSlime` SHALL "eat" (deactivate/consume) an `IEatable` entity upon collision with its mouth/trigger.

#### Scenario: Collision with prey
- **WHEN** a `CorruptSlime`'s mouth trigger contacts an `IEatable` entity
- **THEN** the entity is consumed and the "Attack" animation is triggered.
