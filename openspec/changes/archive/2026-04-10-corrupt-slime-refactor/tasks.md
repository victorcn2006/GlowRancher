## 1. Refactor IEatable

- [x] 1.1 Refactor `IEatable.cs` from `MonoBehaviour` to `interface`.
- [x] 1.2 Implement `IEatable` in `BasicSlime.cs`.

## 2. Corrupt Slime Movement

- [x] 2.1 Create `CorruptSlimeMovement.cs` based on `BasicSlimeMovement.cs` but with aggressive targeting.
- [x] 2.2 Implement Player detection in `CorruptSlimeMovement`.
- [x] 2.3 Implement `IEatable` detection in `CorruptSlimeMovement`.
- [x] 2.4 Ensure `CorruptSlimeMovement` ignores standard food and prioritizes Player/Prey.

## 3. Interaction & Predation

- [x] 3.1 Update `Mouth.cs` or create a predatory version to handle `IEatable` consumption.
- [x] 3.2 Implement `Attack` animator trigger call in predation logic.
- [x] 3.3 Update `CorruptSlime.cs` to use the new movement script and ensure no gem system is active.

## 4. Animation & Polish

- [x] 4.1 Update Animator Controller for Corrupt Slime to include the `Attack` trigger. (User must do this in Unity Editor)
- [x] 4.2 Verify random jumping when no target is present. (User verification required)
- [x] 4.3 Verify aggressive jumping towards Player/Prey when in range. (User verification required)
