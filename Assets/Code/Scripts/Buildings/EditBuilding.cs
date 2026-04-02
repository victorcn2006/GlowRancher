using UnityEngine;
using System.Collections.Generic;

public class EditBuilding : MonoBehaviour
{
    [Header("Models Hologram and Bulding")]
    [SerializeField] private GameObject _hologram;
    [SerializeField] private GameObject _building;

    [Header("Hologram Visuals")]
    [SerializeField] private MeshRenderer _hologramRenderer;
    [SerializeField] private Material _invalidMaterial;

    [Header("Player Camera")]
    [SerializeField] private MainCameraPosition _mainCamera;
    [SerializeField] private float _placeDistance = 20f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _obstructionLayer;

    private Material _originalMaterial;
    private float _initialY;
    private bool _isBuilding = false;
    private bool _isValidPlacement = true;

    // Use a HashSet to track unique colliders to handle multiple colliders on the player
    private HashSet<Collider> _playerColliders = new HashSet<Collider>();
    private bool _contact => _playerColliders.Count > 0;

    private void OnEnable()
    {
        if(InputManager.Instance != null) InputManager.Instance.OnBuildPerformed.AddListener(OnBuildInput);
    }
    private void OnDisable()
    {
        if (InputManager.Instance != null) InputManager.Instance.OnBuildPerformed.RemoveListener(OnBuildInput);
        _playerColliders.Clear();
    }
    private void Start()
    {
        if (_building == null || _hologram == null) return;
        
        if (_hologramRenderer != null)
        {
            _originalMaterial = _hologramRenderer.sharedMaterial;
        }

        _initialY = transform.position.y; //Don't move y offset
        ActiveBuilding();
    }

    private void Update() {
        if (_hologram.activeSelf)
        {
            UpdateHologramPosition();
            CheckPlacementValidity();
        }
    }

    private void UpdateHologramPosition()
    {
        if (_mainCamera == null) return;

        Ray ray = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);
        RaycastHit hit;
        Vector3 targetPosition;

        if (Physics.Raycast(ray, out hit, _placeDistance, _groundLayer))
        {
            targetPosition = hit.point;
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green);
        }
        else
        {
            targetPosition = ray.GetPoint(_placeDistance);
            Debug.DrawRay(ray.origin, ray.direction * _placeDistance, Color.red);
        }

        // Lock the Y position to the initial height
        targetPosition.y = _initialY;
        _hologram.transform.position = targetPosition;
    }

    private void CheckPlacementValidity()
    {
        // We use halfExtents because OverlapBox measures from center to edge.
        // If your model scale is (1,1,1) but it's physically bigger, you might need to adjust this value.
        Vector3 halfExtents = _hologram.transform.localScale / 2f;
        
        Collider[] hitColliders = Physics.OverlapBox(
            _hologram.transform.position, 
            halfExtents, 
            _hologram.transform.rotation, 
            _obstructionLayer,
            QueryTriggerInteraction.Collide
        );

        _isValidPlacement = (hitColliders.Length == 0);

        // Update Material based on validity
        if (_hologramRenderer != null)
        {
            _hologramRenderer.material = _isValidPlacement ? _originalMaterial : _invalidMaterial;
        }
    }

    // Visualizes the detection box in the Unity Editor Scene View
    private void OnDrawGizmos()
    {
        if (_hologram == null || !_hologram.activeSelf) return;

        Gizmos.color = _isValidPlacement ? new Color(0, 1, 0, 0.5f) : new Color(1, 0, 0, 0.5f);
        Gizmos.matrix = Matrix4x4.TRS(_hologram.transform.position, _hologram.transform.rotation, _hologram.transform.lossyScale);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.DrawCube(Vector3.zero, Vector3.one); // semi-transparent solid box
    }

    private void OnBuildInput()
    {
        // BLOCK: If trying to FINISH building but placement is INVALID, do nothing
        if (_isBuilding && !_isValidPlacement) 
        {
            Debug.Log("Invalid placement: Building obstructed!");
            return; 
        }

        // Allow starting ONLY if in contact. Allow finishing REGARDLESS of contact.
        if (!_contact && !_isBuilding) return;

        _isBuilding = !_isBuilding;
        InputManager.Instance.IsBuildingPressed = _isBuilding;

        if (_isBuilding)
        {
            _initialY = transform.position.y; // Update Y based on the parent's position
            ActiveHologram();
        }
        else
        {
            // Move the parent to the hologram's position so the trigger moves with it
            transform.position = _hologram.transform.position;
            
            // Ensure the building model is centered relative to the parent
            _building.transform.localPosition = Vector3.zero;
            
            ActiveBuilding();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_playerColliders.Add(other))
            {
                Debug.Log($"ENTER: {other.name}. Total: {_playerColliders.Count}");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_playerColliders.Contains(other))
            {
                _playerColliders.Add(other);
                Debug.Log($"STAY (Recovered): {other.name}. Total: {_playerColliders.Count}");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_playerColliders.Remove(other))
            {
                Debug.Log($"EXIT: {other.name}. Total: {_playerColliders.Count}");
            }
            
            // If we are NOT building, just make sure the building is active.
            if (!_contact && !_isBuilding)
            {
                ActiveBuilding();
            }
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
