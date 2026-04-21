## ADDED Requirements

### Requirement: CropField supports planting
The `CropField` MUST allow players to plant a seed if the field is empty.

#### Scenario: Successful planting
- **WHEN** the player interacts with an empty `CropField` while holding a seed
- **THEN** the `CropField` consumes the seed and spawns the growth prefab set to `STATES.SEED`

#### Scenario: Failed planting
- **WHEN** the player interacts with an already occupied `CropField`
- **THEN** the interaction is ignored

### Requirement: Vegetable growth initialization
The instantiated plant MUST initialize its lifecycle correctly upon planting.

#### Scenario: Plant initialization
- **WHEN** the plant prefab is instantiated in the `CropField`
- **THEN** it enters the `STATES.SEED` state and starts the growth timer
