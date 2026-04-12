## 1. Standard Slime Movement Update (`ScaredSlimeMovement.cs`)

- [x] 1.1 Store the distance to the target food in `GoJump()` when a food item is detected.
- [x] 1.2 Update the `Jump()` coroutine to calculate a `forceMultiplier` based on the stored target distance relative to `jumpDistance`.
- [x] 1.3 Implement a minimum `forceMultiplier` (e.g., 0.3) to maintain forward momentum.
- [x] 1.4 Ensure the vertical component (`transform.up`) always uses the full `jumpForce`.
- [x] 1.5 Verify that random jumps and fleeing jumps (`_scared == true`) still use the full `jumpForce` (multiplier 1.0).

## 2. Corrupt Slime Movement Update (`CorruptSlimeMovement.cs`)

- [x] 2.1 Calculate the distance to the closest target in `GoJump()` using the results from `FindClosestTarget()`.
- [x] 2.2 Modify the `AddForce` logic inside the rotation tween's `OnComplete` callback to apply scaled horizontal force.
- [x] 2.3 Apply the same `forceMultiplier` logic (clamped between 0.3 and 1.0) based on `JUMP_DISTANCE`.
- [x] 2.4 Ensure vertical force (`transform.up`) remains constant at `JUMP_FORCE`.

## 3. Verification

- [x] 3.1 Test that standard slimes jump precisely toward food without overshooting.
- [x] 3.2 Test that corrupt slimes accurately chase and "bite" the player or prey.
- [x] 3.3 Confirm that random wandering behavior remains unchanged (full force jumps).
- [x] 3.4 Verify that vertical jump height is identical for both scaled and full-force jumps.
