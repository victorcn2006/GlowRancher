## Why

The current building system needs a more consistent and performant structure. By centralizing the editing logic into a `BuildingSystem` parent GameObject and optimizing how collisions and states are handled, we can improve maintainability and performance during building placement.

## What Changes

- **Building Structure Refactor**: Each building will now have a parent GameObject named `BuildingSystem`.
- **Script Migration**: The `EditBuilding` script will be attached to the `BuildingSystem` parent.
- **Trigger-Based Activation**: The `BuildingSystem` will contain a trigger collider. Interaction (switching to hologram mode) will only be possible when the player is within this trigger.
- **Hologram Interaction**: Pressing the 'C' input while inside the trigger will toggle the building into a hologram state.
- **Placement Validation**: Improved collision detection for the hologram. If it overlaps with another building, the material will change to red, and placement will be blocked.
- **Performance Optimization**: Optimize the placement validity checks to reduce overhead in the `Update` loop.

## Capabilities

### New Capabilities
- `building-system-restructuring`: Implementation of the new parent-child hierarchy for all building types.
- `hologram-placement-validation`: Refined logic for hologram movement and collision detection with visual feedback.

### Modified Capabilities
<!-- No existing specs found in openspec/specs/ -->

## Impact

- **Prefabs**: All building prefabs (`Silo`, `Farm`, `Crop Field`, etc.) will need their hierarchy updated to include the `BuildingSystem` parent.
- **Scripts**: `EditBuilding.cs` will be refactored to align with the new structure and performance goals.
- **Input**: The 'C' key interaction logic will be tied strictly to the `BuildingSystem` trigger context.
