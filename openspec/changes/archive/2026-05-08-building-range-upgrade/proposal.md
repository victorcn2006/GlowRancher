## Why

Currently, players can build anywhere within their reach, which lacks a sense of progression and spatial management. Adding a construction limit centered around the player's home base (the House) introduces a new gameplay loop of expanding their territory through upgrades. This provides a clear path for expansion and rewards players for investing in their farm's infrastructure.

## What Changes

- **Construction Range Management**: Implementation of a system to track the current allowable building radius. This will likely be a singleton or a globally accessible script that stores the `CurrentRange` and provides methods to upgrade it.
- **Placement Validation**: The `EditBuilding.cs` component will be updated to include a distance check. Before finalizing placement, the system will verify if the building's target position is within the allowed radius from the House.
- **Visual Feedback**: The building hologram will use the "invalid" visual state (red material) when the target position is outside the construction range, even if there are no physical obstructions.
- **Shop Integration**: The `HouseShopController` (or a related UI component) will be updated to offer a "Range Upgrade" item. Purchasing this item will increase the global construction radius.
- **Central Point Definition**: The House object will be designated as the center of the construction circle.

## Capabilities

### New Capabilities
- `construction-limit`: Defines and enforces a circular boundary for building placement, centered on a specific world point (the House).

### Modified Capabilities
- `building-system`: Updated to incorporate the spatial limit check into the existing placement validation logic.

## Impact

- **Gameplay Progression**: Players start with a small, manageable area and must earn currency to expand their building territory.
- **User Interface**: New UI elements or shop entries will be required to represent the range upgrade.
- **Feedback**: Players receive immediate visual feedback if they attempt to build too far from home.
