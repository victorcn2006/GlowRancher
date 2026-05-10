## ADDED Requirements

### Requirement: Physical Drop Ejection
The system SHALL eject dropped items with an upward impulse and a slight horizontal randomization to simulate a "pop" effect.

#### Scenario: Dropping Items on Harvest
- **WHEN** an item is spawned as a drop
- **THEN** it receives a vertical force between a defined range (e.g., 2 to 5 units)
- **AND** it receives a small random horizontal force in the XZ plane

### Requirement: Pooling for Drops
The drop system MUST retrieve item instances from the `PoolManager` instead of using standard instantiation.

#### Scenario: Reusing Drop Objects
- **WHEN** multiple crops are harvested rapidly
- **THEN** the system requests objects from the `PoolManager` by their ID
- **AND** objects are re-enabled and placed at the harvest position

### Requirement: Drop Visual Orientation
Ejected items SHALL have their rotation randomized upon spawning to provide visual variety.

#### Scenario: Random Rotation
- **WHEN** an item is spawned
- **THEN** its Y-axis rotation is set to a random value between 0 and 360 degrees
