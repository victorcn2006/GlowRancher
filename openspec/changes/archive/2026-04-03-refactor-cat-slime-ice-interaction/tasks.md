# Tasks: Cat Slime Ice Interaction

## 1. Core Logic & Interfaces
- [x] Create `IIceArea` interface in `Assets/Code/Scripts/Interfaces/`.
- [x] Refactor `IceArea.cs` to implement `IIceArea` and use dynamic `currentIcePoints`.
- [x] Refactor `IceStormArea.cs` to implement `IIceArea` and use dynamic `currentIcePoints`.

## 2. Cat Slime Refactor
- [x] Modify `SlimeBehaviorCat.cs`:
    - [x] Remove healing logic.
    - [x] Add `resistanceRadius` and `resistanceValue`.
    - [x] Implement detection of nearby `IIceArea` components and apply resistance.

## 3. Integration & Testing
- [ ] Verify `IceArea` damage is nullified when Cat Slime is near.
- [ ] Verify `IceStormArea` damage is reduced but still active (at 1 point) when Cat Slime is near.
- [ ] Verify `IceArea` damage resumes when Cat Slime leaves or player leaves Cat Slime aura.

## 4. Cleanup
- [x] Remove unused `healAmount` and `healCooldown` fields from `SlimeBehaviorCat`.
