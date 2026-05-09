## 1. Infrastructure & Core Logic

- [x] 1.1 Create `ConstructionRangeManager.cs` in `Assets/Code/Scripts/Systems/`.
- [x] 1.2 Implement the Singleton pattern and define serializable fields for `_baseRadius`, `_radiusStep`, and `_upgradeCost`.
- [x] 1.3 Implement `FindHouse()` to cache the central point position on `Start`.
- [x] 1.4 Implement `IsPositionWithinRange(Vector3 position)` to check distance from the House.
- [x] 1.5 Add `OnDrawGizmos()` to `ConstructionRangeManager` to visualize the radius in the Unity Editor.

## 2. Building System Integration

- [x] 2.1 Modify `Assets/Code/Scripts/Buildings/EditBuilding.cs`:
    - Integrate `ConstructionRangeManager.Instance.IsPositionWithinRange()` into the `ValidatePlacement()` method.
    - Ensure `_isValidPlacement` is only true if both obstruction and range checks pass.
- [x] 2.2 Verify Visuals: Confirm that the hologram material changes to the "invalid" (red) material when moved outside the allowed radius.

## 3. Shop & Upgrade Implementation

- [x] 3.1 Implement `UpgradeRange()` in `ConstructionRangeManager.cs`:
    - Verify sufficient funds in `WalletCurrency`.
    - Deduct the cost and increment the radius.
    - Log the new radius and upgrade level.
- [x] 3.2 Update `Assets/Code/Scripts/Shop/HouseShopController.cs`:
    - Add a public method `BuyRangeUpgrade()` that calls the manager's upgrade logic.

## 4. Verification & Testing

- [ ] 4.1 **Basic Constraint Test**: Try to place a building within the initial radius (should succeed) and outside it (should fail).
- [ ] 4.2 **Upgrade Verification**: Purchase a range upgrade through the House shop (or by manually calling the method) and verify that previously unreachable areas are now valid for construction.
- [ ] 4.3 **Boundary Edge Test**: Place a building exactly at the edge of the radius to ensure floating-point precision issues are handled (using a small epsilon or simple `<=` check).
