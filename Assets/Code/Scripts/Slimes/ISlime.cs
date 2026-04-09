using UnityEngine;

public interface ISlime
{
    Animator animator { get; }
    Mouth mouth { get; }
    FoodDetector foodDetector { get; }
    HungerSystem hungerSystem { get; }
}
