## Context

The `CorruptSlime` is intended to be a predator that targets other slimes (prey) and the player. Currently, it uses a placeholder `IEatable` MonoBehaviour and shares movement logic with the peaceful `BasicSlime`. This design replaces these with a proper interface-based predation system and specialized AI.

## Goals / Non-Goals

**Goals:**
- Refactor `IEatable` into a C# interface for better decoupling.
- Implement a `CorruptSlimeMovement` script that handles aggressive targeting.
- Enable `CorruptSlime` to detect and "eat" `IEatable` entities (Basic Slimes).
- Trigger "Attack" animations during predation and player interaction.
- Ensure `CorruptSlime` ignores standard food.

**Non-Goals:**
- Modifying the existing `HungerSystem`'s internal timer logic (it will be bypassed or minimally used for predation).
- Adding complex pathfinding (staying within the jumping-based movement pattern).
- Implementing "Corrupt" versions of other slimes.

## Decisions

### 1. `IEatable` as an Interface
- **Decision**: Change `IEatable.cs` from `public class IEatable : MonoBehaviour` to `public interface IEatable`.
- **Rationale**: Interfaces allow any class (Slimes, Chickens, etc.) to be marked as prey without requiring a specific component. This aligns with SOLID principles (Interface Segregation).

### 2. Specialized `CorruptSlimeMovement`
- **Decision**: Create a new `CorruptSlimeMovement` script instead of modifying `BasicSlimeMovement`.
- **Rationale**: The movement patterns are fundamentally different (Targeting vs. Food/Wandering). Trying to merge them would lead to complex conditional logic ("God Script" violation).

### 3. Predation via `Mouth.cs` and `IEatable`
- **Decision**: Update `Mouth.cs` to check for `IEatable` components on trigger enter.
- **Rationale**: Reuses existing collision infrastructure. If an object is `IEatable`, the predator triggers its attack sequence.

### 4. Animator Trigger: `Attack`
- **Decision**: Use a Trigger parameter named `Attack` for the aggressive bite/hit animation.
- **Rationale**: Distinct from the `Eat` boolean used for peaceful feeding. It allows for faster, more aggressive visual feedback.

## Risks / Trade-offs

- **[Risk]** Predation might happen too fast or too often. → **Mitigation**: Implement a simple cooldown or detection radius in `CorruptSlimeMovement`.
- **[Risk]** `BasicSlime` might be extinct too quickly in a scene. → **Mitigation**: (Future scope) Ensure spawners or safe zones exist, but out of scope for this task.
- **[Risk]** `Mouth.cs` is shared; predatory logic might leak into basic slimes. → **Mitigation**: Check if the owner of the mouth is a `CorruptSlime` or has a predatory flag.

## Migration Plan

1.  **Interface Refactor**: Delete `IEatable.cs` (class) and create `IEatable.cs` (interface).
2.  **Basic Slime Update**: Add `IEatable` to `BasicSlime.cs`.
3.  **Corrupt Slime AI**: Create `CorruptSlimeMovement.cs` and attach it to the Corrupt Slime prefab, replacing the old movement script.
4.  **Interaction Update**: Update `Mouth.cs` and/or `CorruptSlime.cs` to handle the `IEatable` consumption logic.
5.  **Animation**: Update the Animator Controller for Corrupt Slime to include the `Attack` trigger.
