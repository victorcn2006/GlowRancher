## Context

The project currently uses a `Character` base class for health management and specific slime scripts (e.g., `BasicSlime`, `ScaredSlime`) for behavior. Some slimes like `ScaredSlime` already have a basic player detection and fleeing logic, but it needs to be fully functional and extended for other slime types.

## Goals / Non-Goals

**Goals:**
- Implement unique behaviors for Scared, Fire, Rock, Corrupt, and Cat slimes.
- Implement a lethal Lava hazard for the player and non-immune slimes.
- Adhere to SOLID principles and project-specific patterns (like `IState` and component-based design).
- Use `IDamageable` interface for decoupling health systems.

**Non-Goals:**
- Redesigning the entire slime base architecture.
- Implementing new player movement mechanics beyond reacting to damage/healing.

## Decisions

### 1. Unified Damage Interface
Implement `IDamageable` interface to allow any object (Player, Slimes, Environment) to receive damage or healing without knowing the source.
```csharp
public interface IDamageable {
    void TakeDamage(int damage);
    void Heal(int amount);
}
```
Existing `Character` and `Player` will implement this.

### 2. Slime Behavior Implementation
Instead of giant monolithic classes, behaviors will be implemented as modular components or states.

- **Scared Slime (`SlimeBehaviorScared`)**: Refine `ScaredSlimeMovement` to calculate a fleeing direction away from the player's current position when detected.
- **Fire Slime (`SlimeBehaviorFire`)**: Attach to a slime to deal damage on `OnCollisionEnter` or `OnTriggerEnter` if the other object is `IDamageable`.
- **Rock Slime (`SlimeBehaviorRock`)**: Add a tag or a `LavaImmune` component that the `LavaHazard` script checks before applying damage.
- **Corrupt Slime (`SlimeBehaviorCorrupt`)**: Aggressive AI that uses `NavMeshAgent` (if available) or simple `Rigidbody` force to chase the player and deal damage on contact.
- **Cat Slime (`SlimeBehaviorCat`)**: Use `OverlapCircle` periodically to find the player and call `Heal()` on their `IDamageable` component.

### 3. Lava Hazard (`LavaHazard`)
A component attached to lava triggers.
- It will have a `damageAmount` (set to lethal value for players).
- It will check if the entering object has a `LavaImmune` component or is a `Rock Slime`.
- If not immune and has `IDamageable`, apply damage.

### 4. Detection Logic
Standardize player detection using a shared `PlayerDetector` component that uses `OverlapCircle` or `Trigger` to provide the player's transform to behaviors.

## Risks / Trade-offs

- **[Risk]** High frequency of proximity checks for many slimes.
  - **Mitigation**: Run detection checks at a lower frequency (e.g., every 0.1s - 0.2s) rather than every frame in `Update`.
- **[Risk]** Slimes getting stuck in fleeing/chasing loops.
  - **Mitigation**: Add randomization to the movement vectors and ensure they respect environment boundaries (grounded checks).
- **[Trade-off]** Using `OnCollisionEnter` for damage might be less precise than a dedicated damage system.
  - **Decision**: Acceptable for this prototype level, as it's simple to implement and fits the Unity workflow.
