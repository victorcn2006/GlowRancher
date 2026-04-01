## Why

Enhancing slime diversity and environmental interactions to make the gameplay more engaging and challenging. Each slime type will have unique behaviors that affect how the player interacts with them and the environment.

## What Changes

- **Scared Slime**: Implementation of a fleeing behavior when the player is detected.
- **Fire Slime**: Implementation of a damaging interaction when touching the player.
- **Rock Slime**: Implementation of lava immunity to allow it to traverse hazardous areas.
- **Lava**: Implementation of a lethal environmental hazard for the player.
- **Corrupt Slime**: Implementation of an aggressive behavior that targets the player upon detection.
- **Cat Slime**: Implementation of a beneficial healing behavior for nearby players.

## Capabilities

### New Capabilities
- `slime-behavior-scared`: Defines fleeing logic when player is within a certain range.
- `slime-behavior-fire`: Defines damage-on-touch logic targeting the player.
- `slime-behavior-rock`: Defines immunity to lava-based damage or destruction.
- `slime-behavior-corrupt`: Defines aggressive chasing and attacking logic when player is detected.
- `slime-behavior-cat`: Defines healing-over-time logic for players in proximity.
- `lava-hazard`: Defines the lethal interaction between lava and the player, and safe interaction with rock slimes.

### Modified Capabilities
- None

## Impact

- Slime AI systems and detection logic.
- Player health and damage systems.
- Environment interaction and hazard detection.
