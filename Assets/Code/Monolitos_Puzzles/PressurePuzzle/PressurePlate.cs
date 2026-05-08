using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private int _pressurePlateIndex;
    [SerializeField] private float _pressMovementTime;

    [SerializeField] private float _pressedYOffset;
    private float initialYPosition;

    private PressurePuzzle _pressurePuzzle;

    private void Awake()
    {
        _pressurePuzzle = GetComponentInParent<PressurePuzzle>();
    }

    private void Start()
    {
        initialYPosition = transform.position.y;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Slime") || other.CompareTag("Player"))
        {
            _pressurePuzzle.SetActivePlate(_pressurePlateIndex, true);
            transform.DOMoveY(initialYPosition - _pressedYOffset, _pressMovementTime).SetEase(Ease.InExpo);
        }


        //dotween de emisividad activada
    }

    private void OnTriggerExit(Collider other)
    {


        if (other.CompareTag("Slime") || other.CompareTag("Player"))
        {
            _pressurePuzzle.SetActivePlate(_pressurePlateIndex, false);
            transform.DOMoveY(initialYPosition, _pressMovementTime).SetEase(Ease.InExpo);
        }
        //dotween de emisividad desactivada
    }



}
