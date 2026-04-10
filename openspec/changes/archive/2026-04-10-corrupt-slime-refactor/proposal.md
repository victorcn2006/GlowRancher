## Why

The current `IEatable` implementation is a placeholder `MonoBehaviour` rather than an interface, preventing it from being properly used to identify prey for the `CorruptSlime`. Additionally, the `CorruptSlime` currently shares movement logic with `BasicSlime`, which doesn't fit its aggressive nature. This change will establish a proper predation system and unique AI for the Corrupt Slime.

## What Changes

- **Refactor `IEatable`**: Convert from `MonoBehaviour` to a proper C# interface.
- **Implement `IEatable` on `BasicSlime`**: Allow Corrupt Slimes to identify and target Basic Slimes.
- **New `CorruptSlimeMovement`**: Dedicated movement script for Corrupt Slimes that prioritizes players and eatable slimes over random wandering, and removes food-seeking behavior.
- **Predation Logic**: Corrupt Slimes will now target and "eat" (attack) entities implementing `IEatable`.
- **Attack Animation**: Trigger the "Attack" animation parameter when the Corrupt Slime eats a slime or hits the player.

## Capabilities

### New Capabilities
- `corrupt-slime-ai`: Defines the aggressive movement and targeting behavior for Corrupt Slimes, including predation of `IEatable` entities.
- `eatable-interface`: Defines the contract for entities that can be consumed or attacked by predators like the Corrupt Slime.

### Modified Capabilities
- `slime-animations`: Update to include the "Attack" parameter and its trigger conditions for Corrupt Slimes.

## Impact

- `IEatable.cs`: Converted to interface.
- `BasicSlime.cs`: Now implements `IEatable`.
- `CorruptSlime.cs`: Logic updated to use new movement and attack triggers.
- `CorruptSlimeMovement.cs`: New script created.
- `BasicSlimeMovement.cs`: Referenced for initial logic but decoupled from Corrupt Slime.
- `Mouth.cs`: May need updates to handle `IEatable` predation.
