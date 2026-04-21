## Why

Allowing players to sell their existing buildings provides more flexibility and resource management in the game. It enables players to reclaim space or get back resources when a building is no longer needed or was misplaced during initial construction.

## What Changes

- **Input System**: A new `Sell` input action will be added to the `Player` action map, bound to the 'S' key.
- **InputManager**: The `InputManager` will be updated to expose the `Sell` action and provide an `OnSellPerformed` event for other systems to listen to.
- **EditBuilding**: The `EditBuilding` component will be updated to listen for the `Sell` action. When the player is inside the building's interaction trigger and NOT in editing mode, pressing 'S' will destroy the building and refund 50% of its base price to the player's wallet.
- **Wallet System Integration**: The selling process will communicate with `WalletCurrency` to add the refunded amount.
- **Building Identification**: A new `BuildingType` enum will be used to standardize building identification and price lookup.

## Capabilities

### New Capabilities
- `sell-building`: Defines the logic for removing an instantiated building from the world and refunding 50% of its value to the player's wallet.

### Modified Capabilities
- `building-system`: Updated to include building removal as a core interaction within the building's trigger area.

## Impact

- **Input Configuration**: The 'S' key will have a dual purpose (movement and selling). This requires careful implementation to ensure it doesn't interfere with normal movement when not intended.
- **Building Persistence**: Once sold, the building is removed from the scene.
- **Player Feedback**: Immediate visual/audio feedback should be provided when a building is sold to confirm the action.
