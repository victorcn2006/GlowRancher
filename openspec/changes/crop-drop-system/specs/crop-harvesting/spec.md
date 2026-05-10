## ADDED Requirements

### Requirement: Harvesting Vacuum Interaction
When a plant is in the `RECOLECT` state, it SHALL respond to the vacuum (IAspirable) by triggering its harvest sequence.

#### Scenario: Vacuuming a Mature Plant
- **WHEN** the player uses the Aspirator on a plant in the `RECOLECT` state
- **THEN** the plant calls its harvest method
- **AND** the plant produces drops and clears its state

### Requirement: Crop Field Reset
The `VegetableData` MUST notify its parent `CropField` when it is harvested or destroyed so that the plot can be used again.

#### Scenario: Clearing a Plot After Harvest
- **WHEN** a plant is harvested from a `CropField`
- **THEN** the `CropField` resets its internal `_plantCreated` flag
- **AND** the `CropField` becomes available for a new seed to be planted

### Requirement: Immediate Collection Eligibility
Drops produced during harvest SHALL be immediately eligible for collection by the same vacuum stream that triggered the harvest.

#### Scenario: Chain Collection
- **WHEN** a plant is vacuumed and produces drops
- **THEN** the produced drops are caught in the vacuum's suction force immediately
