using UnityEngine;

public class EditBuilding : MonoBehaviour
{
    [Header("Models Hologram and Bulding")]
    [SerializeField] private GameObject _hologram;
    [SerializeField] private GameObject _building;

    [Header("Player Camera")]
    [SerializeField] private MainCameraPosition _mainCamera;
    [SerializeField] private float _placeDistance = 20f;
    [SerializeField] private LayerMask _groundLayer;

    private float _initialY;
    private bool _contact = false;
    private bool _isBuilding = false;

    private void OnEnable()
    {
        if(InputManager.Instance != null) InputManager.Instance.OnBuildPerformed.AddListener(OnBuildInput);
    }
    private void OnDisable()
    {
        if (InputManager.Instance != null) InputManager.Instance.OnBuildPerformed.RemoveListener(OnBuildInput);
    }
    private void Start()
    {
        if (_building == null || _hologram == null) return;
        _initialY = _building.transform.position.y; //Don't move y offset
        ActiveBuilding();
    }

    private void Update() {
        if (_hologram.activeSelf)
        {
            UpdateHologramPosition();
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

    private void OnBuildInput()
    {
        // Allow starting ONLY if in contact. Allow finishing REGARDLESS of contact.
        if (!_contact && !_isBuilding) return;

        _isBuilding = !_isBuilding;
        InputManager.Instance.IsBuildingPressed = _isBuilding;

        if (_isBuilding)
        {
            _initialY = _building.transform.position.y; // Update Y in case the building was moved before
            ActiveHologram();
        }
        else
        {
            _building.transform.position = _hologram.transform.position;
            ActiveBuilding();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTER");
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player entered building trigger");
            _contact = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("EXIT");
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player exited building trigger");
            _contact = false;
            
            // If we are NOT building, just make sure the building is active.
            if (!_isBuilding)
            {
                ActiveBuilding();
            }
        }
    }

    private void ActiveBuilding() {
        //Debug.Log("B");
        _building.SetActive(true);
        _hologram.SetActive(false);
    }

    private void ActiveHologram() {
        //Debug.Log("A");
        _building.SetActive(false);
        _hologram.SetActive(true);
    }
}
