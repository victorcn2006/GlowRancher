## 1. Specification & Infrastructure

- [x] 1.1 Update `openspec/specs/building-system.md` to formally define the building removal (selling) requirement and its constraints.
- [x] 1.2 Create `BuildingType.cs` with the `BuildingType` enum in `Assets/Code/Scripts/Systems/`.
- [x] 1.3 Update the `Assets/Code/Inputs/Controls.inputactions` asset:
  - Add a new `Sell` action to the `Player` action map bound to 'S'.

## 2. Input Manager & Shop Extension

- [x] 2.1 Modify `InputManager.cs` to include the `Sell` action reference and `OnSellBuildingPerformed` event.
- [x] 2.2 Update `PanelShopController.cs`:
  - Update `GetBuildingPrice` to accept `BuildingType` enum.
  - Fix default return value to `0f`.

## 3. Building System Implementation

- [x] 3.1 Update `EditBuilding.cs`:
  - Add `[SerializeField] private BuildingType _buildingType`.
  - Subscribe to `InputManager.Instance.OnSellBuildingPerformed`.
- [x] 3.2 Implement `HandleSellInput()` in `EditBuilding.cs`.
- [x] 3.3 Implement `SellBuilding()` in `EditBuilding.cs`:
  - Calculate `refundAmount = basePrice * 0.5f`.
  - Call `WalletCurrency.instance.Score(refundAmount)`.
  - Destroy the building GameObject.
- [x] 3.4 Update Building Prefabs: Set the `_buildingType` in the `EditBuilding` component for all building prefabs.

## 4. Verification & Testing

- [x] 4.1 Verify Backward Movement: Confirm that pressing 'S' still allows the player to move backward normally.
- [x] 4.2 Verify Selling & Refund Behavior:
  - Enter a building's interaction trigger and press 'S'. The building should be destroyed, and the wallet should increase by 50% of the building's cost.
  - Stand outside the building's trigger and press 'S'. The building should NOT be destroyed.
- [x] 4.3 Log Audit: Verify console logs.
