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
        StartCoroutine(WaitAnimationTime());
        transform.DOMoveY(transform.position.y - _openOffset, _timeToOpen).SetEase(Ease.InExpo);     
    }

    private IEnumerator WaitAnimationTime()
    {
        yield return new WaitForSeconds(25f);
    }

}
