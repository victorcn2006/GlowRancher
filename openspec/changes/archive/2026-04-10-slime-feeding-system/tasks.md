# Tasks: Slime Feeding System

Implement the detection and movement logic for slimes to seek food.

- [x] Create `FoodDetector.cs` in `Assets/Code/Scripts/Slimes/SlimeFunctions/`
    - Implement `OnTriggerEnter` and `OnTriggerExit` to track food objects.
    - Add a method `GetClosestFood(Vector3 position)` to return the nearest `Transform`.
- [x] Modify `BasicSlime.cs`
    - Add a public reference to `FoodDetector`.
    - Uncomment or properly link the existing hunger-related references if needed.
- [x] Modify `BasicSlimeMovement.cs`
    - Update the `GoJump()` method to check for food if the slime is hungry.
    - If food is detected, use the target direction for the jump.
- [x] Modify `ScaredSlime.cs`
    - Add a public reference to `FoodDetector`.
- [x] Modify `ScaredSlimeMovement.cs`
    - Update the `GoJump()` method to check for food if the slime is hungry AND not scared.
    - If food is detected, use the target direction for the jump.
- [ ] (Validation) Manual verification
    - Verify that slimes jump towards food only when `IsHungry()` is true.
    - Confirm they eat the food and produce a gem.
