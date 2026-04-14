## MODIFIED Requirements

### Requirement: Corrupt Slime Aggressive Pursuit
The `CorruptSlime` SHALL prioritize pursuing the Player or entities with the `IEatable` interface over random wandering, using a dynamic jump force when near the target.

#### Scenario: Target detected within pursuit range
- **WHEN** a Player or `IEatable` entity is within detection range but beyond the slime's `JUMP_DISTANCE`
- **THEN** the `CorruptSlime` jumps towards the target's position using full `JUMP_FORCE`.

#### Scenario: Target detected at close range
- **WHEN** a Player or `IEatable` entity is within the slime's `JUMP_DISTANCE`
- **THEN** the `CorruptSlime` jumps towards the target's position with horizontal force scaled down proportionally to the distance.
