# Proposal: Cat Slime Ice Interaction Refactor

## 📝 Background
The current Cat Slime heals the player when nearby. This behavior is being refactored to provide a protective aura against cold environmental hazards (IceArea and IceStormArea).

## 🎯 Goal
Refactor the Cat Slime to subtract "freeze points" from nearby Ice and Ice Storm areas, providing a localized safe zone for the player.

## 💡 Solution
- Remove the healing behavior from the Cat Slime.
- Implement an "Ice Resistance" aura on the Cat Slime.
- Modify `IceArea` and `IceStormArea` to account for nearby Cat Slimes when calculating active freeze points.
- If a Cat Slime is near, `IceArea` (1 point) will be neutralized (0 points), and `IceStormArea` (2 points) will be reduced to 1 point (survivable/reduced danger).
- If no Cat Slime is near, the areas reset to their default freeze points.

## ✅ Expected Outcomes
- Player survives `IceArea` while near a Cat Slime.
- Player experiences reduced danger (1 point instead of 2) in `IceStormArea` while near a Cat Slime.
- No more automatic healing from Cat Slimes.
