## Setup

- [x] Research existing pooling patterns in `SlimesPool.cs` and `GemsPool.cs`.
- [x] Analyze `PoolManager.cs` to ensure global registration compatibility.

## Refactoring

- [x] Update `VegetablesPool.cs`:
  - [x] Define `vegetablesType` and `seedsType` enums.
  - [x] Add prefab references for all vegetable and seed types.
  - [x] Implement separate pool lists for each item type.
  - [x] Update `Start()` to register all items in the global `PoolManager`.
  - [x] Implement `GetVegetable()` and `GetSeed()` with proper pooling logic.
- [x] Update `Vegetable.cs` (ScriptableObject):
  - [x] Add a reference to the `VegetablesPool.vegetablesType` enum.

## Implementation

- [x] Update `VegetableData.cs`:
  - [x] Add the `IAspirable` interface to the class.
  - [x] Implement `BeingAspired()` to trigger harvesting only when in the `RECOLECT` state.
  - [x] Implement `Harvest()`:
    - [x] Retrieve vegetable and seed instances from `VegetablesPool`.
    - [x] Apply upward impulse on spawn for a "popping" effect.
    - [x] Reset the plant state to `STATES.SEED`.
  - [x] Implement `GetSeedType()` mapping function to link vegetables to their seeds.

## Verification

- [ ] (Editor) Verify all vegetable and seed prefabs are assigned in the `VegetablesPool` component.
- [ ] (Editor) Verify all `Vegetable` ScriptableObjects have their `type` correctly assigned.
- [ ] (Play Mode) Verify that mature plants are harvested when sucked by the vacuum.
- [ ] (Play Mode) Verify that harvesting spawns both a vegetable and a seed.
- [ ] (Play Mode) Verify that vegetables can be "launched" using the vacuum's launch tool (requires `PoolManager` registration).
