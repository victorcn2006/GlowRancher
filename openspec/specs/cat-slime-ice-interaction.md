# Spec: Cat Slime Ice Interaction

## 🧪 Requirement: Cat Slime Resistance Aura
The Cat Slime MUST provide an aura that reduces the severity of nearby ice hazards.

### Scenario: Neutralizing Ice Area
- **WHEN** a Cat Slime is within its `resistanceRadius` of an `IceArea`
- **THEN** the `IceArea`'s active freeze points SHALL be reduced by the Cat Slime's `resistanceValue` (typically 1)
- **AND** if points reach 0, the `IceArea` SHALL NOT deal damage

### Scenario: Reducing Ice Storm Area
- **WHEN** a Cat Slime is within its `resistanceRadius` of an `IceStormArea`
- **THEN** the `IceStormArea`'s active freeze points SHALL be reduced by 1
- **AND** the `IceStormArea` SHALL continue to deal damage as long as active points remain > 0

## 🧪 Requirement: No Healing
The Cat Slime MUST NOT provide healing to the player.

### Scenario: Proximity to Player
- **WHEN** a Cat Slime is near the player
- **THEN** it SHALL NOT trigger any healing events or increase player health

## 🧪 Requirement: Area Reset
Ice hazards MUST return to their base severity when no Cat Slime is providing resistance.

### Scenario: Cat Slime Leaves Range
- **WHEN** a Cat Slime moves out of range of an `IceArea` or `IceStormArea`
- **THEN** the area's active freeze points SHALL reset to their default base value
