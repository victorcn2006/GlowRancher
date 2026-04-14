# Design: Slime Feeding System

Implement the "Follow and Eat" behavior by introducing a detection layer and modifying the movement logic.

## Components

### 1. `FoodDetector.cs`
- **Location**: `Assets/Code/Scripts/Slimes/SlimeFunctions/`
- **Role**: Mantains a list of food objects within a specific radius.
- **Implementation**:
    - Use `OnTriggerEnter` and `OnTriggerExit` with a `SphereCollider` (isTrigger) to track food.
    - Public method `GetClosestFood(Vector3 position)` to return the nearest `Transform`.

### 2. `BasicSlimeMovement.cs` (Modification)
- **Role**: Use the `FoodDetector` to determine the jump direction.
- **Logic**:
    - Check if the slime `IsHungry()` via `HungerSystem`.
    - If hungry, ask `FoodDetector` for the closest food.
    - If food is found, jump towards it instead of a random direction.

### 3. `BasicSlime.cs` (Modification)
- **Role**: Add reference to the new `FoodDetector`.

## Flow
1. `BasicSlimeMovement` update timer reaches 0.
2. If `HungerSystem.IsHungry()` is true, call `FoodDetector.GetClosestFood()`.
3. If a target exists, `jumpDirection = (target.position - transform.position).normalized`.
4. Otherwise, use `jumpDirection = RandomDirection()`.
5. Execute `GoJump()` with the chosen direction.
6. When the slime reaches the food, the `Mouth` trigger (already implemented) will detect the "Food" tag and trigger `HungerSystem.Feed()`.

## Considerations
- **Detection Radius**: Should be configurable (e.g., 5-10 units).
- **Update Frequency**: `OnTriggerEnter/Exit` is efficient as it only updates when objects move in/out.
- **Food Validity**: Ensure food is not already `isBeingEaten` before targeting it (optional but recommended).
