# Design: Cat Slime Ice Interaction

## 🏗️ Architecture

### `CatSlimeResistance` Component
A new component (or refactor of `SlimeBehaviorCat`) that defines the resistance aura.
- `float resistanceRadius`: Range of the effect.
- `int resistanceValue`: Points subtracted from ice areas (default: 1).

### `IIceArea` Interface (New)
To allow `CatSlime` to interact with different types of ice areas uniformly.
- `void ApplyResistance(int value)`
- `void ResetResistance()`

### `IceArea` and `IceStormArea` (Refactored)
- Implement `IIceArea`.
- Maintain a `currentIcePoints` variable instead of a `const ICE_POINTS`.
- Check for nearby `CatSlime` instances or receive signals from them.

## 🛠️ Interaction Logic
1. `IceArea` starts with `baseIcePoints` (1 for `IceArea`, 2 for `IceStormArea`).
2. `currentIcePoints` is initialized to `baseIcePoints`.
3. During `Update` or via a detection system (e.g., `OverlapSphere` in `IceArea` or `CatSlime` signaling), the `currentIcePoints` is reduced by the `resistanceValue` of any nearby Cat Slimes.
4. If no Cat Slime is present, `currentIcePoints` resets to `baseIcePoints`.
5. Damage is only applied if `currentIcePoints > 0`.

## 🎨 Visual Feedback
- Add a visual indicator (e.g., a faint blue/warm aura or particle effect) to show the resistance is active.
- (Optional) Change the visual intensity of the Ice Area when neutralized.
