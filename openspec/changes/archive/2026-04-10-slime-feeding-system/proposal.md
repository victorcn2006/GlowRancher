# Proposal: Slime Feeding System (Follow and Eat Food)

Implement a system that allows slimes to detect nearby food, jump towards it when they are hungry, and eat it upon contact.

## Problem
Currently, slimes jump randomly and only eat food if they happen to collide with it. This makes the feeding process passive and slow. Slimes should actively seek out food to make the world feel more alive and the gameplay more interactive.

## Solution
1. **Active Searching**: Implement a `FoodDetector` component that scans the surrounding area for objects tagged as "Food".
2. **Targeted Movement**: Modify `BasicSlimeMovement` to prioritize jumping towards detected food if the `HungerSystem` reports that the slime is hungry.
3. **Feeding Integration**: Utilize the existing `HungerSystem` and `Mouth` scripts to handle the actual eating animation and gem production once the slime reaches the food.

## Goals
- Slimes should detect food within a configurable radius.
- Slimes should only seek food when they are hungry.
- Slimes should jump towards the closest piece of food.
- The system should be modular and easy to attach to different slime types.
