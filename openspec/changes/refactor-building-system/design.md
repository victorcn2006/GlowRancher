## Context

The current building system uses the `EditBuilding` script to handle both player interaction and building placement. The structure of buildings is not fully standardized, and there are performance concerns with constant placement validation checks.

## Goals / Non-Goals

**Goals:**
- Standardize building hierarchy with a `BuildingSystem` parent.
- Improve performance by optimizing collision checks.
- Enhance visual feedback for invalid placements.
- Ensure 'C' input interaction is only possible when the player is within the building's trigger.

**Non-Goals:**
- Adding new building types.
- Modifying building functionality beyond placement/editing.

## Decisions

- **Hierarchical Parent-Child Structure**: Introduce `BuildingSystem` as the root GameObject. This decouples the core editing logic from the specific building visuals and data.
  - **Rationale**: Simplifies prefab management and ensures a consistent interface for all buildings.
- **Trigger-Based Interaction**: Use a `BoxCollider` (isTrigger) on the `BuildingSystem` root for player detection.
  - **Rationale**: Efficiently limits when the 'C' input is processed, avoiding unnecessary input checks for buildings far from the player.
- **Optimized Placement Validation**:
  - **Rationale**: Instead of `OverlapBox` every frame, we could switch to a system that only re-validates when the hologram's position changes significantly, or use a dedicated trigger-based system on the hologram model. However, for initial refactor, we will keep `OverlapBox` but ensure it only runs when `_isBuilding` is true.
- **Material Feedback**: Use a dedicated `_invalidMaterial` for the hologram when placement is blocked.
  - **Rationale**: Provides clear, immediate visual feedback to the player.

## Risks / Trade-offs

- **[Risk] Hierarchy Breaking Existing References** → **Mitigation**: Audit scripts like `Silo.cs`, `Farm.cs`, and `Building.cs` to ensure they don't depend on a specific parent structure. Use `GetComponentInChildren` where necessary.
- **[Risk] Multiple Player Colliders** → **Mitigation**: Use the existing `HashSet<Collider>` in `EditBuilding` to track player entry/exit accurately.
- **[Risk] Performance of OverlapBox** → **Mitigation**: Ensure `OverlapBox` is only active during the editing phase and uses a specific `_obstructionLayer` to minimize the number of collisions checked.
