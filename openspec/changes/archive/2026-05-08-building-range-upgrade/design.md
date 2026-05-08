## Context

The building system in `EditBuilding.cs` currently validates placement based solely on physical obstructions (`_obstructionLayer`). The player can move the building hologram anywhere within `_maxPlaceDistance` of their current position. The goal is to add a global constraint: all buildings must be placed within a specific radius of the `House`.

## Goals / Non-Goals

**Goals:**
- Implement a global construction radius centered on the House.
- Ensure `EditBuilding` respects this radius during placement validation.
- Provide a mechanism to upgrade this radius via the House shop.
- Provide visual feedback when the radius is exceeded.

**Non-Goals:**
- Implementing multiple construction zones.
- Changing the core selling or moving mechanics.

## Decisions

### 1. `ConstructionRangeManager.cs`
A new Singleton script will be created to manage the construction boundary.
- **Path**: `Assets/Code/Scripts/Systems/ConstructionRangeManager.cs`
- **Responsibilities**:
  - Track the current `ConstructionRadius`.
  - Cache the `House` position as the center point.
  - Provide a validation method: `bool IsPositionWithinRange(Vector3 targetPos)`.
  - Handle range upgrades and cost calculation.

### 2. Upgrading the Range
The `ConstructionRangeManager` will manage its own upgrade state:
- `[SerializeField] private float _baseRadius = 15f;`
- `[SerializeField] private float _radiusStep = 10f;`
- `[SerializeField] private float _upgradeCost = 100f;`
- `private int _upgradeLevel = 0;`

The `UpgradeRange()` method will:
1. Check if the player has enough currency via `WalletCurrency.instance`.
2. Deduct the cost.
3. Increase the `_upgradeLevel`.
4. Update the current radius.

### 3. `EditBuilding.cs` Integration
Modify `ValidatePlacement()` to include the range check:
```csharp
private void ValidatePlacement()
{
    // ... existing obstruction check ...

    bool isWithinRange = true;
    if (ConstructionRangeManager.Instance != null)
    {
        isWithinRange = ConstructionRangeManager.Instance.IsPositionWithinRange(_hologramModel.transform.position);
    }

    _isValidPlacement = (hitColliders.Length == 0) && isWithinRange;

    // ... existing visual feedback ...
}
```

### 4. `HouseShopController.cs` Extension
Add a method `BuyRangeUpgrade()` that the House UI can call.
- This method will delegate to `ConstructionRangeManager.Instance.UpgradeRange()`.

### 5. Visualizing the Boundary
To help the player, `ConstructionRangeManager` will draw a Gizmo in the editor. 
*Future consideration*: Adding a visual circle (e.g., using a `LineRenderer` or a custom shader) in-game when the player enters "Editing" mode.

## Risks / Trade-offs

- **[Risk] Missing House**: If the player hasn't placed a House or if it's destroyed, the system might fail.
  - **Mitigation**: `ConstructionRangeManager` will default to `Vector3.zero` or the player's starting position if no House is found, and will log a warning.
- **[Risk] UI Integration**: Adding a new shop item requires UI work which can be complex in Unity without direct scene manipulation.
  - **Decision**: We will provide the backend logic and a hook in `HouseShopController`. The actual UI button assignment should be done in the Unity Editor by the user.
