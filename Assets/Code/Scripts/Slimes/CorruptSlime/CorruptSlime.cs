using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptSlime : MonoBehaviour, IAspirable
{
    // --------------------------------------------LINKED SCRIPTS------------------------------------------------- \\
    [Header("SLIME SCRIPTS")]
    [SerializeField] private Mouth _mouth;
    [SerializeField] private CorruptSlimeMovement _movementBehaviour;
    [SerializeField] private Animator _animator;

    // Interface Implementation
    public Mouth mouth => _mouth;
    public Animator animator => _animator;

    public CorruptSlimeMovement movementbehaviour => _movementBehaviour;

    public void BeingAspired()
    {
        _movementBehaviour.SetBeingAspired(true);

    }

    public void StopBeingAspired()
    {
        _movementBehaviour.SetBeingAspired(false);
    }
}
