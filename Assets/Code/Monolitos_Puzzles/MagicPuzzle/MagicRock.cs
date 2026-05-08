using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MagicRock : MonoBehaviour
{

    private MagicPuzzle _magicPuzzle;

    [SerializeField] private float _succedYOffset;
    [SerializeField] private float _succedMovementTime;
    private float _initialYPosition;
    private bool _isActive = false;

    private void Awake()
    {
        _magicPuzzle = GetComponentInParent<MagicPuzzle>();
    }

    private void Start()
    {
        _initialYPosition = transform.position.y;
    }


    public void ActiveRock()
    {
        if (!_isActive)
        {
            _isActive = true;
            transform.DOMoveY(_initialYPosition + _succedYOffset, _succedMovementTime).SetEase(Ease.InOutSine).OnComplete(() => _magicPuzzle.AddActiveRock(this.gameObject));
        }
    }

    public void DeactivateRock()
    {
        _isActive = false;
        transform.DOMoveY(_initialYPosition, _succedMovementTime).SetEase(Ease.InExpo);

    }

    public void PuzzleCompleted()
    {
        transform.DOMoveY(_initialYPosition, _succedMovementTime).SetEase(Ease.InExpo);
    } 

}
