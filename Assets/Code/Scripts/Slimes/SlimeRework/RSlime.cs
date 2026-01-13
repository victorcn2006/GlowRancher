using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSlime : MonoBehaviour, IAspirable
{
    // --------------------------------------------LINKED SCRIPTS------------------------------------------------- \\
    // Acabar de crear y agregar todos los RFunction \\
    [Header("SLIME SCRIPTS")]
    public RHungerSystem hungerSystem;
    public RFoodDetector foodDetector;
    public RGemSystem gemSystem;
    public RMouth mouth;
    public RSlimeMovementBehaviour movementbehaviour;

    public void BeingAspired()
    {
        movementbehaviour.SetBeingAspired(true);

    }

    public void StopBeingAspired()
    {
        movementbehaviour.SetBeingAspired(false);
    }
}
