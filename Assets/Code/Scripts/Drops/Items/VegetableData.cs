using UnityEngine;

public class VegetableData : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private Vegetable _vegetableData;

    [Header("Models States - Prefabs")]
    [SerializeField] private GameObject _seedPrefab;
    [SerializeField] private GameObject _growthPrefab;
    [SerializeField] private GameObject _maturePrefab;
    [SerializeField] private GameObject _recolectPrefab;

    private GameObject _currentModel;
    private float _stateTimer;


    public enum STATES{
        SEED,
        GROWTH,
        MATURE,
        RECOLECT
    }

    [SerializeField] private STATES _currentState;

    private void Start()
    {
        _stateTimer = 0f;
        SetState(STATES.SEED);
    }

    

    private void Update()
    {

        _stateTimer += Time.deltaTime;
        _vegetableData.time = _stateTimer;

        switch (_currentState)
        {
            case STATES.SEED:
                if (_stateTimer >= _vegetableData.stateDuration)
                {
                    SetState(STATES.GROWTH);
                }
                break;

            case STATES.GROWTH:
                if (_stateTimer >= _vegetableData.stateDuration)
                {
                    SetState(STATES.MATURE);
                }
                break;

            case STATES.MATURE:
                if (_stateTimer >= _vegetableData.stateDuration)
                {
                    SetState(STATES.RECOLECT);
                }
                break;

            case STATES.RECOLECT:
                // Wait for player interaction
                break;
        }
    }

    private void UpdateVisuals()
    {
        if(_currentModel != null) Destroy(_currentModel);
        GameObject prefabToSpawn = _currentState switch
        {
            STATES.SEED => _seedPrefab,
            STATES.GROWTH => _growthPrefab,
            STATES.MATURE => _maturePrefab,
            STATES.RECOLECT => _recolectPrefab,
            _ => null
        };
        if (prefabToSpawn != null)
        {
            // Instantiate as a child of this GameObject, inheriting its position
            _currentModel = Instantiate(prefabToSpawn, transform.position, transform.rotation, transform);
        }
    }

    private void SetState(STATES state) {
        _currentState = state;
        _stateTimer = 0f;
        UpdateVisuals();
    }

    public void GetState(STATES state)
    {
        _currentState = state;
    }

    public void Harvest()
    {
        if (_currentState == STATES.RECOLECT)
        {
            SetState(STATES.SEED);
        }
    }
}
