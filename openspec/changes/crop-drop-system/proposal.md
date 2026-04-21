## Why

The current harvesting system is functional but lacks physical feedback and robust state management. Mature plants currently instantiate items at their position without any physical "pop" effect, and the `CropField` does not correctly reset its state after harvest, preventing reuse of the plot. Centralizing the drop logic will ensure consistent behavior, visual polish (physical ejection), and reliable crop rotation.

## What Changes

- **Physical Drop Feedback**: Items (Food and Seeds) will now be "ejected" from the plant when harvested using physical forces (impulse and random spread).
- **Pooling Integration**: Drops will be retrieved from the `PoolManager` instead of being instantiated raw, improving performance.
- **Harvest State Cleanup**: The `VegetableData` will notify the `CropField` when it is destroyed/harvested, allowing the field to accept new seeds.
- **Vacuum Interaction**: Refine `IAspirable` behavior for newly spawned drops to ensure they are immediately interactable with the vacuum.

## Capabilities

### New Capabilities
- `crop-harvesting`: Defines the behavior of mature plants when vacuumed, including drop spawning and field cleanup.
- `physical-drop-system`: A utility system to handle the "ejection" of items from a point with physical forces and pooling.

### Modified Capabilities
- None.

## Impact

- `VegetableData.cs`: Logic for harvesting and state management.
- `CropField.cs`: Fix for plot reuse.
- `Aspirator.cs`: Interaction with mature plants.
- `PoolManager.cs` / `VegetablesPool.cs`: Used for item retrieval.
- Prefabs: Ensure Seed and Food prefabs have `Rigidbody` and `IAspirable` components correctly configured.
