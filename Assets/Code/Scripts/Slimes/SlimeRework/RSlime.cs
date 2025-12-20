using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSlime : MonoBehaviour
{
    // --------------------------------------------LINKED SCRIPTS------------------------------------------------- \\
    // Acabar de crear y agregar todos los RFunction \\
    [Header("SLIME SCRIPTS")]
    [SerializeField] private RSlimeMovementStateMachine stateMachine ;
    [SerializeField] private RHungerSystem hungerSystem;
    [SerializeField] private RFoodDetector foodDetector;
    [SerializeField] private RGemSystem gemSystem;
    [SerializeField] private RMouth mouth;
    [SerializeField] private RSlimeMovementBehaviour movementbehaviour;



}
