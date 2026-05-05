using UnityEngine;
using UnityEngine.Serialization;
using System.Collections.Generic;

/// <summary>
/// Manages the building editing system, allowing players to move and reposition buildings via a hologram.
/// This script should be attached to a "BuildingSystem" parent GameObject that acts as the root.
/// </summary>
public class EditBuilding : MonoBehaviour
{
    [Header("Hierarchy References")]
    [Tooltip("The actual static building model/container.")]
    [FormerlySerializedAs("_building")]
    [SerializeField] private GameObject _buildingModel;
    [Tooltip("The hologram model used for positioning.")]
    [FormerlySerializedAs("_hologram")]
    [SerializeField] private GameObject _hologramModel;

    [Header("Visual Feedback")]
    [SerializeField] private MeshRenderer _hologramRenderer;
    [FormerlySerializedAs("_invalidMaterial")]
    [SerializeField] private Material _invalidPlacementMaterial;

    [Header("Placement Settings")]
    [FormerlySerializedAs("_placeDistance")]
    [SerializeField] private float _maxPlaceDistance = 20f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _obstructionLayer;

    [Header("Selling Settings")]
    [Tooltip("The type of building to look up its price in the ShopController.")]
    [SerializeField] private BuildingType _buildingType;

    private bool _isNewBuilding;

    private Material _originalHologramMaterial;
    private float _lockedY;
    private bool _isEditing = false;
    private bool _isValidPlacement = true;
    private MainCameraPosition _mainCamera;

    // Tracks player colliders within the trigger to allow interaction
    private HashSet<Collider> _playerColliders = new HashSet<Collider>();
    private bool _isPlayerInside => _playerColliders.Count > 0;

    private void OnEnable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnBuildPerformed.AddListener(HandleBuildInput);
            InputManager.Instance.OnSellBuildingPerformed.AddListener(HandleSellInput);
        }
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnBuildPerformed.RemoveListener(HandleBuildInput);
            InputManager.Instance.OnSellBuildingPerformed.RemoveListener(HandleSellInput);
        }
        _playerColliders.Clear();
    }

    private void Start()
    {
        _mainCamera = FindObjectOfType<MainCameraPosition>();
        
        if (_buildingModel == null || _hologramModel == null)
        {
            Debug.LogError($"[EditBuilding] Missing model references on {gameObject.name}", this);
            return;
        }

        if (_hologramRenderer != null)
        {
            _originalHologramMaterial = _hologramRenderer.sharedMaterial;
        }

        _lockedY = transform.position.y;
        SetState(false); // Start in static building mode
    }

    private void Update()
    {
        if (_isEditing)
        {
            UpdateHologramPlacement();
            ValidatePlacement();
        }
    }

    private void UpdateHologramPlacement()
    {
        if (_mainCamera == null) return;

        Ray ray = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);
        Vector3 targetPosition;

        if (Physics.Raycast(ray, out var hit, _maxPlaceDistance, _groundLayer))
        {
            targetPosition = hit.point;
        }
        else
        {
            targetPosition = ray.GetPoint(_maxPlaceDistance);
        }

        // Keep the building at its original height
        targetPosition.y = _lockedY;
        _hologramModel.transform.position = targetPosition;
    }

    private void ValidatePlacement()
    {
        // Use halfExtents for OverlapBox (measures from center to edge)
        Vector3 halfExtents = _hologramModel.transform.localScale / 2f;
        
        Collider[] hitColliders = Physics.OverlapBox(
            _hologramModel.transform.position, 
            halfExtents, 
            _hologramModel.transform.rotation, 
            _obstructionLayer,
            QueryTriggerInteraction.Collide
        );

        _isValidPlacement = (hitColliders.Length == 0);

        // Update visual feedback
        if (_hologramRenderer != null)
        {
            _hologramRenderer.material = _isValidPlacement ? _originalHologramMaterial : _invalidPlacementMaterial;
        }
    }

    private void HandleBuildInput()
    {
        if (_isEditing)
        {
            TryFinalizePlacement();
        }
        else if (_isPlayerInside)
        {
            StartEditing();
        }
    }

    private void HandleSellInput()
    {
        if (!_isEditing && _isPlayerInside)
        {
            SellBuilding();
        }
    }

    private void SellBuilding()
    {
        PanelShopController shop = PanelShopController.Instance;

        // Fallback if the shop UI hasn't been opened yet (singleton not initialized)
        if (shop == null)
        {
            shop = FindFirstObjectByType<PanelShopController>(FindObjectsInactive.Include);
        }

        if (shop != null && _buildingType != BuildingType.None)
        {
            float basePrice = shop.GetBuildingPrice(_buildingType);
            float refund = basePrice * 0.5f;

            if (WalletCurrency.instance != null)
            {
                WalletCurrency.instance.Score(refund);
                Debug.Log($"[EditBuilding] Successfully sold {_buildingType} for {refund} (50% of {basePrice})");
                
                // Only destroy if refund was successful
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("[EditBuilding] Cannot sell building: WalletCurrency.instance is null!");
            }
        }
        else
        {
            if (_buildingType == BuildingType.None)
            {
                Debug.LogWarning($"[EditBuilding] {gameObject.name} cannot be sold because 'Building Type' is set to 'None' in the Inspector. Please update your building prefabs!");
            }
            else
            {
                Debug.LogError($"[EditBuilding] {gameObject.name} cannot be sold: ShopController not found in scene!");
            }
        }
    }

    public void StartEditing(bool isNew = false)
    {
         _isEditing = true;
         _isNewBuilding = isNew;
    
        // If it's an existing building, lock to its current Y
        // If it's new, we'll set it in the first UpdateHologramPlacement
        _lockedY = transform.position.y;
   
       InputManager.Instance.IsBuildingPressed = true;
       SetState(true);
    }

private void TryFinalizePlacement()
    {
        if (!_isValidPlacement)
        {
            Debug.Log("<color=red>Invalid placement: Building obstructed!</color>");
            CancelPlacement();
            return;
        }

        // Apply new position to the root object
        transform.position = _hologramModel.transform.position;
        
        // Reset building model local position
        _buildingModel.transform.localPosition = Vector3.zero;

        _isEditing = false;
        InputManager.Instance.IsBuildingPressed = false;
        SetState(false);
    }

    private void CancelPlacement(){
        if (_isNewBuilding)
        {
            Destroy(gameObject);
        }
        else
        {
             // Revert to original state
            _isEditing = false;
             InputManager.Instance.IsBuildingPressed = false;
             SetState(true);
        }
    }

    private void SetState(bool editing)
    {
        _buildingModel.SetActive(!editing);
        _hologramModel.SetActive(editing);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerColliders.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerColliders.Remove(other);
            
            // If the player leaves while NOT editing, ensure we stay in static mode
            if (!_isEditing)
            {
                SetState(false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!_isEditing || _hologramModel == null) return;

        Gizmos.color = _isValidPlacement ? new Color(0, 1, 0, 0.3f) : new Color(1, 0, 0, 0.3f);
        Gizmos.matrix = Matrix4x4.TRS(_hologramModel.transform.position, _hologramModel.transform.rotation, _hologramModel.transform.lossyScale);
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
