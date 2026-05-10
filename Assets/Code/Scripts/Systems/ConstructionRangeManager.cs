using UnityEngine;

/// <summary>
/// Manages the allowable construction radius centered on the House.
/// </summary>
public class ConstructionRangeManager : MonoBehaviour
{
    public static ConstructionRangeManager Instance { get; private set; }

    [Header("Radius Settings")]
    [SerializeField] private float _baseRadius = 15f;
    [SerializeField] private float _radiusStep = 10f;
    [SerializeField] private float _maxRadius = 50f;

    [Header("Upgrade Settings")]
    [SerializeField] private float _upgradeCost = 0f;

    private float _currentRadius;
    private int _upgradeLevel = 0;
    private Vector3 _centerPoint = Vector3.zero;
    private bool _houseFound = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _currentRadius = _baseRadius;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        FindHouse();
    }

    private void FindHouse()
    {
        House house = FindFirstObjectByType<House>();
        if (house != null)
        {
            _centerPoint = house.transform.position;
            _houseFound = true;
            Debug.Log($"[ConstructionRangeManager] House found at {_centerPoint}. Initial radius: {_currentRadius}");
        }
        else
        {
            Debug.LogWarning("[ConstructionRangeManager] House not found! Defaulting center to Vector3.zero.");
            _centerPoint = Vector3.zero;
            _houseFound = false;
        }
    }

    public bool IsPositionWithinRange(Vector3 targetPos)
    {
        // Ignore Y for a 2D circle check on the ground plane
        Vector3 centerFlat = new Vector3(_centerPoint.x, 0, _centerPoint.z);
        Vector3 targetFlat = new Vector3(targetPos.x, 0, targetPos.z);
        
        return Vector3.Distance(centerFlat, targetFlat) <= _currentRadius;
    }

    public void UpgradeRange()
    {
        if (_currentRadius >= _maxRadius)
        {
            Debug.Log("<color=cyan>[ConstructionRangeManager] Maximum range reached!</color>");
            return;
        }

        if (WalletCurrency.instance != null)
        {
            if (WalletCurrency.instance.bank >= _upgradeCost)
            {
                WalletCurrency.instance.Score(-_upgradeCost);
                _upgradeLevel++;
                
                float nextRadius = _baseRadius + (_upgradeLevel * _radiusStep);
                _currentRadius = Mathf.Min(nextRadius, _maxRadius);
                
                Debug.Log($"[ConstructionRangeManager] Range upgraded to Level {_upgradeLevel}. New Radius: {_currentRadius}");
            }
            else
            {
                Debug.Log("<color=yellow>Not enough currency to upgrade range!</color>");
            }
        }
        else
        {
            Debug.LogError("[ConstructionRangeManager] WalletCurrency.instance is null!");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Vector3 center = _houseFound ? _centerPoint : Vector3.zero;
        
        // Draw the range circle
        Gizmos.DrawWireSphere(center, _currentRadius);
        
        // Draw a semi-transparent disc for better visibility
        Gizmos.color = new Color(0, 1, 1, 0.1f);
        Gizmos.DrawSphere(center, _currentRadius);
    }
}
