using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneRenderingTrigger : MonoBehaviour
{

    [SerializeField] private List<GameObject> mapZonesToActive = new List<GameObject>();

    private void OnTriggerEnter()
    {
        //ZoneRenderingController.Instance.SetActiveZones(mapZonesToActive);
    }


}
