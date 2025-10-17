using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AspireHability : MonoBehaviour
{
    [SerializeField] private string goodSlimeTag;

    [SerializeField] private GameObject AspirePoint;

    [SerializeField] private float aspireForce;

    List<GameObject> Slimes = new List<GameObject>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Aspire();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopAspire();
        }
    }

    private void Aspire()
    {
        foreach (GameObject slime in Slimes) {
            slime.GetComponent<Rigidbody>().useGravity = false;
            slime.GetComponent<Rigidbody>().AddForce((AspirePoint.transform.position - slime.transform.position).normalized * aspireForce, ForceMode.Force);
        }
    }

    private void StopAspire()
    {
        foreach (GameObject slime in Slimes)
        {
            slime.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(goodSlimeTag))
        {
            Debug.Log(other.gameObject.name + " enter");
            Slimes.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(goodSlimeTag))
        {
            Slimes.Remove(other.gameObject);
            Debug.Log(other.gameObject.name + " exit");
        }
    }

}
