## Why

Currently, both standard slimes (seeking food) and `CorruptSlime` (seeking prey) use a constant jump force and distance. This causes them to over-jump their targets when they are close, making their movement look stiff and unnatural. Implementing a dynamic jump force that scales down when the target is within the default jump range will improve movement accuracy and visual quality.

## What Changes

- Implement dynamic jump force calculation for all slime types.
- Scale down the horizontal jump force proportionally when the target distance is less than the slime's default `jumpDistance`.
- Ensure slimes maintain their vertical jump component to keep the "bouncy" feel.
- Apply this logic to standard slimes when jumping toward food.
- Apply this logic to `CorruptSlime` when jumping toward the Player or `IEatable` entities.

## Capabilities

### New Capabilities
- `slime-movement`: Defines the base jumping behavior for slimes, including the new dynamic force calculation when targeting objects.

### Modified Capabilities
- `corrupt-slime-ai`: Update the pursuit requirements to include dynamic jump force scaling when the target is within reach.

## Impact

- `ScaredSlimeMovement.cs`: The `GoJump()` and `Jump()` logic will be updated to handle distance-based force.
- `CorruptSlimeMovement.cs`: The `GoJump()` method will be modified to calculate jump force based on the distance to the target.
- Potential impact on slime "feel" and difficulty (slimes will be more accurate at close range).
