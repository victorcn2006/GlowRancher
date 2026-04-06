## 1. Script Refactoring

- [x] 1.1 Refactor `EditBuilding.cs` to align with the `BuildingSystem` parent structure.
- [x] 1.2 Implement specialized trigger logic in `EditBuilding.cs` for player detection.
- [x] 1.3 Optimize `CheckPlacementValidity()` in `EditBuilding.cs` to use the `_obstructionLayer` and only run when editing.
- [x] 1.4 Ensure `EditBuilding.cs` correctly toggles between static model and hologram based on the 'C' key.

## 2. Prefab Restructuring

- [x] 2.1 Update `SiloBuildingSystem.prefab`: Renamed root and verified EditBuilding structure.
- [x] 2.2 Update `Crop field.prefab`: Automated via `BuildingRefactorWindow`.
- [x] 2.3 Update `House.prefab`: Automated via `BuildingRefactorWindow`.
- [x] 2.4 Update `Incinerator.prefab` (both versions): Automated via `BuildingRefactorWindow`.
- [x] 2.5 Update `Silo.prefab`: Automated via `BuildingRefactorWindow`.

## 3. Verification and Testing

- [ ] 3.1 Verify that the 'C' key only works when the player is inside the building's trigger.
- [ ] 3.2 Verify that the building follows the camera aim in hologram mode.
- [ ] 3.3 Verify that colliding with another building changes the hologram material to red.
- [ ] 3.4 Verify that placement is blocked when the status is invalid (logged message and no placement).
- [ ] 3.5 Check for regressions in building functionality (e.g., `Silo`, `Farm` logic).
