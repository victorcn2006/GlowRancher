using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FloatingFX : MonoBehaviour
{

    [Header("FLOATING VARIABLES")]
    [SerializeField] private float _movementOffset;
    [SerializeField, Tooltip("tiempo que va a tardar en reproducirse el ease")] private float _easeTime;
    [SerializeField] private Ease _easeType;

    private float _initialYPosition;

    [Header("ROTATION VARIABLES")]
    [SerializeField, Tooltip("booleano para decidir si el objeto rota o no")]
    private bool _rotation;

    [SerializeField, Tooltip("booleano para decidir hacia que lado rota(true = derecha, false = izquierda)")]
    private bool _rotateToRight;

    [SerializeField] private float _fullRotationTime;

    private void Start()
    {
        _initialYPosition = transform.position.y;
        transform.DOMoveY(_initialYPosition + _movementOffset, _easeTime).SetEase(_easeType).SetLoops(-1, LoopType.Yoyo);

        if (_rotation)
        {
            if (!_rotateToRight)transform.DORotate(new Vector3(0, -360, 0), _fullRotationTime, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);

            else transform.DORotate(new Vector3(0, 360, 0), _fullRotationTime, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        }

    }


}
