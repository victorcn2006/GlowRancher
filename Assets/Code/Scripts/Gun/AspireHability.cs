using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AspireHability : MonoBehaviour
{
    [SerializeField] private GameObject AspirePoint;

    [SerializeField] private float aspireForce;

    List<GameObject> Slimes = new List<GameObject>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Aspire();
        }
    }

    private void Aspire()
    {
        foreach (GameObject slime in Slimes) {
            slime.GetComponent<Rigidbody>().AddForce((AspirePoint.transform.position - slime.transform.position).normalized * aspireForce, ForceMode.Force);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Slime"))
        {
            Slimes.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Slime"))
        {
            Slimes.Remove(other.gameObject);
        }
    }

}
