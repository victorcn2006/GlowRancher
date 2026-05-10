## 1. Core Systems Preparation

- [ ] 1.1 Implement `DropUtils` static class for physical ejection and pooling retrieval.
- [ ] 1.2 Update `CropField.cs` to accept a reference reset from its child plants.
- [ ] 1.3 Update `IAspirable.cs` or ensure all drop types correctly implement it.

## 2. Vegetable Data Refactor

- [ ] 2.1 Update `VegetableData.cs` to store a reference to its parent `CropField`.
- [ ] 2.2 Modify `Harvest()` in `VegetableData.cs` to use `DropUtils` instead of `Instantiate`.
- [ ] 2.3 Ensure `Harvest()` notifies `CropField` before the plant is destroyed.

## 3. Interaction & Polish

- [ ] 3.1 Verify `Aspirator.cs` detection for newly ejected items.
- [ ] 3.2 Configure Seed and Food prefabs (Rigidbodies, Colliders, ItemPickUp).
- [ ] 3.3 Test full loop: Plant -> Grow -> Vacuum Harvest -> Re-plant.
