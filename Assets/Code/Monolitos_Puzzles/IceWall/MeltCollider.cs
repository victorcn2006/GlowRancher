using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<HeatFont>(out HeatFont heatFont)) GetComponentInParent<IceWall>().Melt();

    }
}
