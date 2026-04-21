## Context

The current `VegetablesPool.cs` was an incomplete copy-paste of `GemsPool.cs` with undefined variables and broken logic. The `VegetableData.cs` handled growth states but had no implementation for harvesting resources or interacting with the player's vacuum. Seeds existed as prefabs but were not integrated into the pooling or harvesting systems.

## Goals / Non-Goals

**Goals:**
- Refactor `VegetablesPool.cs` to correctly pool both food and seeds for all vegetable types (Carrot, Tomato, Pumpkin, Eggplant).
- Link each `Vegetable` ScriptableObject to its specific pool type.
- Implement the `Harvest()` method in `VegetableData.cs` to spawn both a vegetable (food) and a seed when the plant is in the `RECOLECT` state.
- Enable harvesting via the player's vacuum by implementing `IAspirable` in `VegetableData.cs`.
- Ensure all pooled objects are registered with the global `PoolManager` for compatibility with existing launch logic in `Aspirator.cs`.

**Non-Goals:**
- Implementing a completely new planting system if one is not already established (focus is on the food/seed lifecycle).
- Modifying the core building or shop logic.

## Decisions

### 1. `VegetablesPool` Structure
The `VegetablesPool` will maintain separate lists for each vegetable and seed type. It will use a singleton instance for easy access and delegate object registration to the global `PoolManager` during `Start()`.

### 2. Enums for Types
Two enums will be used within `VegetablesPool`:
- `vegetablesType`: `CARROT`, `EGGPLANT`, `PUMPKIN`, `TOMATO`.
- `seedsType`: `CARROT_SEED`, `EGGPLANT_SEED`, `PUMPKIN_SEED`, `TOMATO_SEED`.

### 3. `Vegetable` ScriptableObject Update
Add `public VegetablesPool.vegetablesType type` to the `Vegetable` class to allow `VegetableData` to identify which pool to use on harvest.

### 4. Harvesting Logic (`VegetableData.cs`)
- Implement `IAspirable` to detect suction from the `Aspirator`.
- Implement `Harvest()`:
  - Check if `_currentState == STATES.RECOLECT`.
  - Retrieve the vegetable and seed instances from `VegetablesPool.Instance`.
  - Position them at the plant's location and apply a small upward impulse to simulate "popping" out.
  - Reset the plant state to `STATES.SEED` to allow for the next growth cycle.

### 5. `PoolManager` Integration
The `RegisterItem` method in `VegetablesPool` will ensure each prefab is added to the global `PoolManager`. This is essential because `Aspirator.LanzarObjeto()` retrieves objects from `PoolManager` using their name string (from `ItemPickUp.nombre`).

## Risks / Trade-offs

- **[Risk] Resource Overload**: Frequent harvesting could lead to many active objects in the scene.
  - **Mitigation**: Object pooling in `VegetablesPool` ensures that objects are reused rather than instantiated, and they should be picked up by the player or eventually despawned by other systems.
- **[Risk] Static Interaction**: Since plants are static, the `Aspirator`'s force logic won't affect them.
  - **Decision**: Plants will only react to `BeingAspired()` by triggering `Harvest()`. No physical movement force will be applied to the plant itself.
