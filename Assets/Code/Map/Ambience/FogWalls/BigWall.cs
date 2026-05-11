using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BigWall : MonoBehaviour
{

    [SerializeField] private float _openOffset;
    [SerializeField] private float _timeToOpen;
    public void OpenDoor()
    {
        transform.DOMoveY(transform.position.y - _openOffset, _timeToOpen).SetEase(Ease.InExpo);
        StartCoroutine(WaitAnimationTime());
    }

    private IEnumerator WaitAnimationTime()
    {
        yield return new WaitForSeconds(25f);
    }

}
