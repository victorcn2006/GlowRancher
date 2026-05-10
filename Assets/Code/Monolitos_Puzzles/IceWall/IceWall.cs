using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class IceWall : MonoBehaviour
{

    [SerializeField] private float _movementOffset;
    [SerializeField] private float _timeToMove;

    [SerializeField] private ParticleSystem _smokeParticleFX;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<HeatFont>(out HeatFont heatFont))
        {
            _smokeParticleFX.Play();
            Melt();
        }
    }

    private void Melt()
    {
        transform.DOMoveY(transform.position.y - _movementOffset, _timeToMove).SetEase(Ease.InQuint);
    }

}
