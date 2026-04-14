using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSlime : MonoBehaviour, IAspirable
{
    // --------------------------------------------LINKED SCRIPTS------------------------------------------------- \\
    [Header("SLIME SCRIPTS")]
    public HungerSystem hungerSystem;
    //public RFoodDetector foodDetector;
    public GemSystem gemSystem;
    public Mouth mouth;
    public BasicSlimeMovement movementbehaviour;
    public Animator animator;

    public void BeingAspired()
    {
        movementbehaviour.SetBeingAspired(true);

    }

    public void StopBeingAspired()
    {
        movementbehaviour.SetBeingAspired(false);
    }
}
