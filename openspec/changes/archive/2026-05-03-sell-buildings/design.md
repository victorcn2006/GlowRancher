## Context

The building system currently allows players to place and move buildings using the `EditBuilding` component. It uses Unity's Input System for interactions. 'C' is currently the key for the "Building/Editing" toggle. While the player can move existing buildings, there is currently no mechanism for removing them from the world.

## Goals / Non-Goals

**Goals:**
- Implement a "Sell" input action bound to the 'S' key.
- Allow the player to sell (destroy) a building by pressing 'S' while inside its interaction trigger.
- Ensure the building is NOT in "Editing" (hologram) mode when being sold.
- Refund 50% of the building's base price to the player's wallet upon selling.
- Standardize building identification using a `BuildingType` enum.

**Non-Goals:**
- Redesigning the core building hierarchy or trigger setup.

## Decisions

### 1. Building Type Enum
Create a `BuildingType` enum in `Assets/Code/Scripts/Systems/BuildingType.cs` to standardize building identification:
- `None`, `Incinerator`, `Greenhouse`, `Silo`, `SlimeCage`, `Fusioner`.

### 2. `EditBuilding` Price Metadata
Instead of hardcoding prices in each script, `EditBuilding.cs` will use:
- `[SerializeField] private BuildingType _buildingType`
- It will look up the price dynamically from `PanelShopController.instance.GetBuildingPrice(_buildingType)`.

### 3. New Input Action: `Sell`
A new action named `Sell` will be added to the `Player` action map in the `Controls.inputactions` asset.
- **Action Type**: Button
- **Binding**: 'S' (Keyboard)

### 4. `InputManager` Extension
The `InputManager` will be updated to handle the new `Sell` action:
- Add a `SerializeField` for the `Sell` action reference.
- Add a `UnityEvent` named `OnSellPerformed`.
- Subscribe to the action's `performed` event and invoke the `UnityEvent`.

### 5. `EditBuilding` Selling & Refund Logic
The `EditBuilding` component will implement the selling logic:
- Subscribe to `InputManager.Instance.OnSellBuildingPerformed` during `OnEnable`.
- Implement `HandleSellInput()`:
  - Check if the player is inside the interaction trigger (`_isPlayerInside`).
  - Check if the building is NOT currently being edited (`!_isEditing`).
  - If both conditions are met, call `SellBuilding()`.
- Implement `SellBuilding()`:
  - Get `basePrice` from `PanelShopController`.
  - Calculate `refundAmount = basePrice * 0.5f`.
  - Call `WalletCurrency.instance.Score(refundAmount)` to update the player's bank.
  - Log the action and refund amount for debugging.
  - Destroy the `BuildingSystem` root GameObject.

### 6. Specification Synchronization
Update `openspec/specs/building-system.md` to include the selling requirement:
- **Requirement**: Building Removal (Selling) with Refund
- **Scenario**: Sell Building and Receive 50% Refund
- **Scenario**: Prevent Selling During Editing

## Risks / Trade-offs

- **[Risk] Accidental Selling**: Since 'S' is the standard key for backward movement, a player might accidentally sell a building while trying to navigate away from its trigger.
  - **Mitigation**: The selling logic only triggers if the player is inside the building's trigger. We will rely on the user's specific key choice but note this as a potential UX issue for future refinement (e.g., adding a hold-to-sell interaction).
- **[Risk] Conflict with Move Action**: The 'Sell' action and 'Move' (down) action share the same key.
  - **Decision**: Unity's Input System allows multiple actions to be bound to the same key. The `Move` action is continuous (Value), while `Sell` is discrete (Button/Performed). `EditBuilding` will only react to the discrete event when the specific proximity condition is met.
