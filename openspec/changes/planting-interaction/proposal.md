## Why

Currently, players cannot plant crops in the `CropField` as it lacks the interaction logic to receive seeds and start the growth process. This functionality is essential for the farm management loop.

## What Changes

- **CropField Interaction**: Add logic to `CropField.cs` to allow planting by detecting interaction while holding a seed.
- **Vegetable Lifecycle Integration**: Connect seed input to the growth system managed by `VegetableData.cs`.
- **Planting State**: Update `CropField` to manage the presence of a planted vegetable.

## Capabilities

### New Capabilities
- `planting-system`: Handles seed-to-plant interaction, validation, and initialization in crop fields.

### Modified Capabilities
- `building-system`: Updating the `CropField` component to support planting interactions.

## Impact

- **Affected code**: `Assets/Code/Scripts/Buildings/CropField.cs`.
- **Systems**: Updates the farm management loop by enabling active planting.
