## Context

The `CropField` is currently a passive container. We need to introduce an interaction mechanism so that the `CropField` can "consume" a seed (which is currently a GameObject) and instantiate a `VegetableData` component to initiate growth.

## Goals / Non-Goals

**Goals:**
- Implement `IInteractable` in `CropField`.
- Add logic to accept a seed object into the `CropField`.
- Instantiate the growth prefab upon successful planting.

**Non-Goals:**
- Creating a full UI-based inventory planting system (focus is on world-based interaction).

## Decisions

### 1. `CropField` Interaction
The `CropField` will use the existing `Interact` pattern (as seen in `DoorSilo`). When the player interacts with an empty `CropField` while having a seed "selected" or available, the seed will be consumed and the plant initialized.

### 2. Planting Logic
- `CropField` will hold a reference to the `VegetableData` prefab (or a mapping of seeds to growth prefabs).
- On `Interact()`:
  - Check if `CropField` has an active child object.
  - If empty, request a seed instance from the player/inventory, instantiate the plant, and set it to `STATES.SEED`.

## Risks / Trade-offs

- **[Risk] State Consistency**: Ensuring the plant prefab correctly initializes its growth cycle in the `CropField`.
  - **Mitigation**: Standardize initialization through `VegetableData.SetState(STATES.SEED)`.
