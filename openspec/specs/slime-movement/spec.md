# slime-movement Specification

## Purpose
Specification for base slime movement behaviors, including dynamic jump force calculations.

## Requirements

### Requirement: Dynamic Jump Force for Targeted Movement
The slime movement system SHALL calculate jump force based on the distance to the target when jumping towards an object (e.g., food).

#### Scenario: Target within default jump distance
- **WHEN** a slime is jumping toward a target (e.g., food) that is closer than its `jumpDistance`
- **THEN** the applied jump force SHALL be scaled down proportionally to the distance, while maintaining its vertical component.

#### Scenario: Target at or beyond default jump distance
- **WHEN** a slime is jumping toward a target that is at or beyond its `jumpDistance`
- **THEN** the slime SHALL apply its full `jumpForce`.
