using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZoneRenderingTrigger : MonoBehaviour
{

    [SerializeField] private List<GameObject> mapZonesToActive = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) ZoneRenderingController.Instance.SetActiveZones(mapZonesToActive);
    }

}
