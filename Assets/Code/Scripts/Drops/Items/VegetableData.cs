using UnityEngine;

public class VegetableData : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private Vegetable _vegetableData;

    [Header("Models States")]
    [SerializeField] private GameObject _seed;
    [SerializeField] private GameObject _growth;
    [SerializeField] private GameObject _mature;
    [SerializeField] private GameObject _recolect;

    private float _stateTimer;


    public enum STATES{
        SEED,
        GROWTH,
        MATURE,
        RECOLECT
    }

    private void Start()
    {
        _stateTimer = 0f;
        SetState(STATES.SEED);
    }

    [SerializeField] private STATES _currentState;

    private void Update()
    {

        _stateTimer += Time.deltaTime;

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
        _vegetableData.time = _stateTimer;
    }

    private void UpdateVisuals()
    {
        _seed.SetActive(false);
        _growth.SetActive(false);
        _mature.SetActive(false);
        _recolect.SetActive(false);

        switch (_currentState)
        {
            case STATES.SEED:
                _seed.SetActive(true);
                break;

            case STATES.GROWTH:
                _growth.SetActive(true);
                break;

            case STATES.MATURE:
                _mature.SetActive(true);
                break;

            case STATES.RECOLECT:
                _recolect.SetActive(true);
                break;
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
