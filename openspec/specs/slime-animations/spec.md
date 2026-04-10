# slime-animations Specification

## Purpose
TBD - created by archiving change corrupt-slime-refactor. Update Purpose after archive.
## Requirements
### Requirement: Attack Animator Parameter
The Slime Animator SHALL include an `Attack` (Trigger) parameter to represent aggressive actions (e.g., biting or charging).

#### Scenario: Attack triggered
- **WHEN** the `Attack` parameter is triggered in the animator
- **THEN** the "Attack" animation state is played according to standard responsiveness rules.

### Requirement: Corrupt Slime Attack Triggering
The `CorruptSlime` SHALL trigger the `Attack` animation when it successfully consumes an `IEatable` entity or hits the player.

#### Scenario: Consumption triggers attack
- **WHEN** an `IEatable` entity is consumed by a `CorruptSlime`
- **THEN** the `Attack` trigger is sent to the animator.

#### Scenario: Player collision triggers attack
- **WHEN** a `CorruptSlime` collides with the player in an offensive manner
- **THEN** the `Attack` trigger is sent to the animator.

