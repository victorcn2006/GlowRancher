using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSlime : MonoBehaviour, IAspirable, ISlime, IEatable
{
    // --------------------------------------------LINKED SCRIPTS------------------------------------------------- \\
    [Header("SLIME SCRIPTS")]
    [SerializeField] private HungerSystem _hungerSystem;
    [SerializeField] private FoodDetector _foodDetector;
    [SerializeField] private GemSystem _gemSystem;
    [SerializeField] private Mouth _mouth;
    [SerializeField] private BasicSlimeMovement _movementBehaviour;
    [SerializeField] private Animator _animator;

    // Interface Implementation
    public HungerSystem hungerSystem => _hungerSystem;
    public FoodDetector foodDetector => _foodDetector;
    public Mouth mouth => _mouth;
    public Animator animator => _animator;

    // Direct access for movement and gems
    public GemSystem gemSystem => _gemSystem;
    public BasicSlimeMovement movementbehaviour => _movementBehaviour;

    public void BeingAspired()
    {
        _movementBehaviour.SetBeingAspired(true);

    }

    public void StopBeingAspired()
    {
        _movementBehaviour.SetBeingAspired(false);
    }
}
