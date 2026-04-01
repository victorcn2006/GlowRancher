## ADDED Requirements

### Requirement: Cat Slime Healing Behavior
The Cat Slime SHALL heal the player when they are within healing range.

#### Scenario: Player is within healing range
- **WHEN** the player enters the healing radius of a Cat Slime
- **THEN** it heals the player for a small amount over time (e.g., every 1 second)
- **AND** it plays a positive visual effect (e.g., green sparkles or hearts)

#### Scenario: Player leaves healing range
- **WHEN** the player exits the healing radius of a Cat Slime
- **THEN** the periodic healing stops
