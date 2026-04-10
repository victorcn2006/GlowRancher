## ADDED Requirements

### Requirement: IEatable Interface
The system SHALL provide an `IEatable` interface that can be attached to any game object to indicate it is valid prey for predators.

#### Scenario: Interface is detected
- **WHEN** a `CorruptSlime`'s detection system finds an object with `IEatable`
- **THEN** it identifies it as a valid target for pursuit and consumption.

### Requirement: IEatable Implementation on Basic Slimes
All `BasicSlime` entities SHALL implement the `IEatable` interface.

#### Scenario: Basic Slime identified as prey
- **WHEN** a `CorruptSlime` encounters a `BasicSlime`
- **THEN** it treats it as a target to pursue and eat.
