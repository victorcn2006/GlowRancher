## 1. Core Infrastructure

- [x] 1.1 Create `IDamageable.cs` interface in `Assets/Code/Scripts/Interfaces/`
- [x] 1.2 Update `Character.cs` to implement `IDamageable`
- [x] 1.3 Update `Player.cs` to implement `IDamageable` and ensure `TakeDamage` and `Heal` work as expected

## 2. Environmental Hazards

- [x] 2.1 Create `ILavaImmune.cs` or `LavaImmune.cs` component to mark immunity
- [x] 2.2 Create `LavaHazard.cs` script with trigger-based damage logic
- [x] 2.3 Implement lethal damage in `LavaHazard` for players and non-immune entities

## 3. Slime Behaviors

- [x] 3.1 Refine `ScaredSlimeMovement.cs` to ensure it flee-jumps away from the player transform
- [x] 3.2 Create `SlimeBehaviorFire.cs` to deal damage on contact with `IDamageable`
- [x] 3.3 Create `SlimeBehaviorCorrupt.cs` with aggressive chase and damage logic
- [x] 3.4 Create `SlimeBehaviorCat.cs` with periodic healing logic for nearby `IDamageable` players

## 4. Prefab Setup & Verification

- [x] 4.1 Update `FireSlime` prefab with `SlimeBehaviorFire`
- [x] 4.2 Update `RockSlime` prefab with `LavaImmune` component
- [x] 4.3 Update `ScaredSlime` prefab and ensure `ScaredSlimePlayerDetector` is correctly linked
- [x] 4.4 Create/Update `CorruptSlime` and `CatSlime` prefabs with their respective behaviors
- [x] 4.5 Verify all behaviors in-game: damage, healing, fleeing, and lava immunity
