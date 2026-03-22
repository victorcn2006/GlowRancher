using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MagicRock : MonoBehaviour
{

    private MagicPuzzle _magicPuzzle;

    [SerializeField] private float _succedYOffset;
    [SerializeField] private float _succedMovementTime;
    private float initialYPosition;

    private void Awake()
    {
        _magicPuzzle = GetComponentInParent<MagicPuzzle>();
    }

    private void Start()
    {
        initialYPosition = transform.position.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActiveRock();
        }
    }

    private void ActiveRock()
    {
        transform.DOMoveY(initialYPosition + _succedYOffset, _succedMovementTime).SetEase(Ease.InOutSine).OnComplete(() => _magicPuzzle.AddActiveRock(this.gameObject));
    }

    public void DeactivateRock()
    {
        transform.DOMoveY(initialYPosition, _succedMovementTime);
    }


}
