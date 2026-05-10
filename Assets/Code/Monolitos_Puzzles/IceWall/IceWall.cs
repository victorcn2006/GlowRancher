using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class IceWall : MonoBehaviour
{

    [SerializeField] private float _movementOffset;
    [SerializeField] private float _timeToMove;

    [SerializeField] private ParticleSystem _smokeParticleFX;

    private bool _melted = false;

    public void Melt()
    {
        if (!_melted)
        {
            _smokeParticleFX.Play();
            transform.DOMoveY(transform.position.y - _movementOffset, _timeToMove).SetEase(Ease.InQuint);
        }
        _melted = true;
    }

}
