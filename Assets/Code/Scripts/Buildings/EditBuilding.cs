using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class EditBuilding : MonoBehaviour
{
    [SerializeField] private GameObject _hologram;
    [SerializeField] private GameObject _building;

    private bool _contact = false;

    private void OnEnable()
    {
        if(InputManager.Instance != null) InputManager.Instance.OnBuildPerformed.AddListener(Build);
    }

    private void Start()
    {
        if (_building == null) return;
        if(_hologram == null) return;
        ActiveBuilding();
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null) InputManager.Instance.OnBuildPerformed.RemoveListener(Build);
    }

    private void Build() {
        InputManager.Instance.IsBuildingPressed = !InputManager.Instance.IsBuildingPressed;

        if (InputManager.Instance.IsBuildingPressed && _contact) ActiveHologram();
        else {
            ActiveBuilding();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _contact = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _contact = false;
        }
    }

    private void ActiveBuilding() {
        _building.SetActive(true);
        _hologram.SetActive(false);
    }

    private void ActiveHologram() {
        _building.SetActive(false);
        _hologram.SetActive(true);
    }
}
