using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredSlime : MonoBehaviour, IAspirable, ISlime, IEatable
{
    // --------------------------------------------LINKED SCRIPTS------------------------------------------------- \\
    [Header("SLIME SCRIPTS")]
    [SerializeField] private HungerSystem _hungerSystem;
    [SerializeField] private FoodDetector _foodDetector;
    [SerializeField] private GemSystem _gemSystem;
    [SerializeField] private Mouth _mouth;
    [SerializeField] private ScaredSlimeMovement _movementBehaviour;
    [SerializeField] private ScaredSlimePlayerDetector _playerDetector;
    [SerializeField] private Animator _animator;

    // Interface Implementation
    public HungerSystem hungerSystem => _hungerSystem;
    public FoodDetector foodDetector => _foodDetector;
    public Mouth mouth => _mouth;
    public Animator animator => _animator;

    // Direct access for movement, gems and player detection
    public GemSystem gemSystem => _gemSystem;
    public ScaredSlimeMovement movementbehaviour => _movementBehaviour;
    public ScaredSlimePlayerDetector playerDetector => _playerDetector;

    public void BeingAspired()
    {
        _movementBehaviour.SetBeingAspired(true);

    }

    public void StopBeingAspired()
    {
        _movementBehaviour.SetBeingAspired(false);
    }
}
