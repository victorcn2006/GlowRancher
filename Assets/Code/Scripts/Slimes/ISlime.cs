using UnityEngine;

public interface ISlime : IDamageable
{
    Animator animator { get; }
    Mouth mouth { get; }
    FoodDetector foodDetector { get; }
    HungerSystem hungerSystem { get; }
}
