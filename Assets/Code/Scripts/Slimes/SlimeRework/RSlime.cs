using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSlime : MonoBehaviour
{
    // --------------------------------------------LINKED SCRIPTS------------------------------------------------- \\
    // Acabar de crear y agregar todos los RFunction \\
    [Header("SLIME SCRIPTS")]
    [SerializeField] private RSlimeStateMachine stateMachine ;
    [SerializeField] private HungerSystem hungerSystem;
    [SerializeField] private FoodDetector foodDetector;
    [SerializeField] private GemSystem gemSystem;
    [SerializeField] private Mouth mouth;
    [SerializeField] private RSlimeMovementBehaviour movementbehaviour;
}
