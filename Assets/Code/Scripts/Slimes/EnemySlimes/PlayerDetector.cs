using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{

    [SerializeField] private GameObject playerReference;
    private bool playerInRangeCheck;

    private void Start()
    {
        playerReference = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerReference) 
        {
            playerInRangeCheck = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerReference)
        {
            playerInRangeCheck = false;
        }
    }

    public bool CheckPlayerInRange()
    {
        return playerInRangeCheck;
    }

    public GameObject GetPlayer()
    {
        return playerReference;
    }
}
