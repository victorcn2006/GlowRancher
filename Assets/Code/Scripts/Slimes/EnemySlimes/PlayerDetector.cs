using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{

    [SerializeField] private GameObject _playerReference;
    private bool _playerRangeCheck;

    private void Start()
    {
        _playerReference = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _playerReference) 
        {
            _playerRangeCheck = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _playerReference)
        {
            _playerRangeCheck = false;
        }
    }

    public bool CheckPlayerInRange()
    {
        return _playerRangeCheck;
    }

    public GameObject GetPlayer()
    {
        return _playerReference;
    }
}
