## Context

The current slime movement implementation uses a fixed `AddForce` call for jumping. This works well for random wandering but results in poor accuracy when slimes are targeting objects (food for regular slimes, prey for corrupt slimes). When a target is closer than the distance covered by a full-force jump, the slime overshoots it.

## Goals / Non-Goals

**Goals:**
- Scale the horizontal component of the jump force based on target distance.
- Maintain consistent vertical jump height to preserve the visual identity of slime movement.
- Improve slime accuracy when targeting food or prey.
- Ensure the logic is applied consistently across standard and corrupt slimes.

**Non-Goals:**
- Pathfinding or obstacle avoidance.
- Changing the jump frequency or timing.
- Modifying the "flee" behavior of scared slimes (fleeing should still use full force for maximum distance).

## Decisions

### 1. Parabolic Scaling Logic
To land exactly on a target while maintaining a consistent "slime feel," we will use kinematic parabolic equations. Assuming constant gravity and constant vertical force (to keep jump height and "hang time" the same), the horizontal distance is directly proportional to the horizontal force.

- **Vertical Force ($F_y$):** Remains constant (`transform.up * jumpForce`).
- **Horizontal Force ($F_x$):** Scales linearly (`transform.forward * jumpForce * forceMultiplier`).
- **Multiplier ($M$):** Calculated as `distanceToTarget / maxPhysicsJumpDistance`.

**Calibration:** We will add a `maxPhysicsJumpDistance` field. This represents the actual distance (in meters) the slime travels during a standard jump with 100% force. If the slime is overshooting, this value should be increased to match the actual physics result.

### 2. Implementation in `ScaredSlimeMovement.cs`
- Store the horizontal distance to food in `GoJump()`.
- Pass this distance to `Jump()`.
- If no target or fleeing, `multiplier = 1.0`.
- Clamp multiplier to `[0.3, 1.0]` to prevent "hopping in place" if the food is touching the slime.

### 3. Implementation in `CorruptSlimeMovement.cs`
- Calculate distance to the closest prey/player.
- Apply the same scaling logic inside the rotation tween's `OnComplete` callback.

## Risks / Trade-offs

- **[Risk] Slimes feeling "weak" at close range** → Mitigation: Implement a minimum force multiplier (e.g., 0.3) to ensure slimes still look like they are jumping and not just hopping in place.
- **[Risk] Physics inconsistencies** → Mitigation: Use the existing `jumpDistance` parameter as the reference for scaling, ensuring the math aligns with current level design.
