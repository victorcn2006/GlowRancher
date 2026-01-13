using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RMouth : MonoBehaviour
{
    private RSlime _RSlime;
    void Start()
    {
        _RSlime = GetComponent<RSlime>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_RSlime.foodDetector.GetInRangeFoodList().Contains(other.gameObject))
        {
            _RSlime.hungerSystem.Eat(other.gameObject);
        }
    }
}
