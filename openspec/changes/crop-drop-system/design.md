## Context

Currently, `VegetableData.cs` handles its own harvest logic by instantiating food and seeds directly. This results in a static spawn without physical feedback. Additionally, `CropField.cs` becomes "locked" because it doesn't know when the plant it spawned has been removed.

## Goals / Non-Goals

**Goals:**
- Implement a physical "pop" effect for harvested items.
- Integrate `PoolManager` for all crop-related drops.
- Fix the plot reuse bug in `CropField`.
- Ensure newly spawned items are immediately vacuumable.

**Non-Goals:**
- Implementing a general-purpose loot table system (this is specifically for crops).
- Redesigning the entire inventory system.
- Adding complex VFX (focus is on physical motion).

## Decisions

### 1. Centralized `DropSystem` Utility
**Decision**: Create a `DropUtils` (or similar) static class to handle physical ejection.
**Rationale**: Keeps `VegetableData` clean and allows other systems (like slimes or chests) to reuse the same "pop" logic in the future.
**Alternatives**: 
- Putting logic in `PoolManager`: Violates SRP (Single Responsibility Principle).
- Putting logic in `VegetableData`: Prevents reuse and bloats the class.

### 2. `CropField` Reference in `VegetableData`
**Decision**: `CropField` will pass a reference to itself when instantiating a plant. `VegetableData` will call `field.ClearHarvestState()` on harvest.
**Rationale**: Simple and direct. Decouples the field state from the plant's existence.
**Alternatives**:
- `OnDestroy` hook: Risky in Unity as it triggers during scene changes or application quit.
- Event-based: Overkill for a 1-to-1 relationship.

### 3. Immediate Suction Logic
**Decision**: Ensure `Aspirator` continues to detect the new objects by making sure they are spawned inside the vacuum's detection zone and have the correct components from the start.
**Rationale**: New items need to be active and have `IAspirable` to be picked up by the `Aspirator`'s `Update` loop.

## Risks / Trade-offs

- **[Risk]** Items popping through terrain → **Mitigation**: Use small impulse values and ensure initial spawn position is slightly above the ground.
- **[Risk]** Pool exhaustion → **Mitigation**: `PoolManager` already handles dynamic expansion if the pool is empty.
- **[Risk]** Race condition with Vacuum → **Mitigation**: The vacuum loop runs in `Update`, ensuring it will see the new object in the next frame at the latest.
