using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredSlime : MonoBehaviour, IAspirable
{
    // --------------------------------------------LINKED SCRIPTS------------------------------------------------- \\
    [Header("SLIME SCRIPTS")]
    public HungerSystem hungerSystem;
    //public RFoodDetector foodDetector;
    public GemSystem gemSystem;
    public Mouth mouth;
    public ScaredSlimeMovement movementbehaviour;
    public ScaredSlimePlayerDetector playerDetector;

    public void BeingAspired()
    {
        movementbehaviour.SetBeingAspired(true);

    }

    public void StopBeingAspired()
    {
        movementbehaviour.SetBeingAspired(false);
    }
}
