using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private GameObject _buildingPrefab;

    private void OnEnable() {
        if (InputManager.Instance != null)
            InputManager.Instance.OnBuildPerformed.AddListener(HandleBuildInput);
    }
    private void OnDisable()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnBuildPerformed.RemoveListener(HandleBuildInput);
    }
     private void HandleBuildInput(){
        // Only spawn if we aren't already building/editing something
        if (!InputManager.Instance.IsBuildingPressed)
        {
            SpawnBuilding();
        }
     }
     private void SpawnBuilding(){
        // Instantiate the building at a temporary position
        GameObject newBuilding = Instantiate(_buildingPrefab, Vector3.zero, Quaternion.identity);
        // Get the EditBuilding component and start placement mode immediately
        EditBuilding editComp = newBuilding.GetComponent<EditBuilding>();
        if (editComp != null)
        {
            editComp.StartEditing(true); // Pass true to indicate it's a new building
        }
    }
}

