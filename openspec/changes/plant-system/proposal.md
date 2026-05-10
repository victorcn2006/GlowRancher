## Why

Adding a plant system allows players to grow their own food for slimes, creating a sustainable gameplay loop. Players can collect seeds, plant them in crop fields, and harvest mature vegetables. This adds a layer of resource management and progression to the farming aspect of the game.

## What Changes

- **Pooling System**: A new `VegetablesPool` will be created to manage the pooling of both vegetables (food) and seeds. This ensures efficient instantiation and reuse of these frequent items.
- **Vegetable Data Structure**: The `Vegetable` ScriptableObject will be updated to include a `vegetablesType` enum, linking each vegetable to its pool configuration.
- **Growth and Harvesting Logic**: The `VegetableData` component will be updated to handle the transition between growth states and the harvesting process.
- **Suction Integration**: `VegetableData` will implement `IAspirable` to allow players to harvest mature plants using the vacuum (Aspirator).
- **Resource Generation**: Harvesting a mature plant will now spawn both a vegetable (food for slimes) and a seed (to replant), both retrieved from the `VegetablesPool`.
- **Global Pool Registration**: All vegetables and seeds will be registered with the global `PoolManager` to allow other systems (like the Aspirator's launch logic) to access them by name.

## Capabilities

### New Capabilities
- `vegetable-pooling`: Efficiently manages the lifecycle of vegetable and seed objects using object pooling.
- `plant-harvesting`: Defines the logic for collecting resources from mature plants using the vacuum tool.

### Modified Capabilities
- `plant-system`: Core system updated to support the full growth-to-harvest lifecycle with resource generation.

## Impact

- **Performance**: Object pooling reduces the overhead of frequent instantiation and destruction during harvesting.
- **Gameplay Balance**: Players now have a reliable source of food and seeds, enabling long-term ranch management.
- **Interaction**: The vacuum tool gains a new interaction with mature plants, consistent with existing gameplay mechanics for collecting slimes and gems.
