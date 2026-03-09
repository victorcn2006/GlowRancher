using System.Collections;
using UnityEngine;

public class Player : Character, ISavable
{
    [Header("Configuración de Bioma y Economía")]
    public string currentBiome = "Pradera";
    public int money = 0;

    [Header("Sistema de Energía (Stamina)")]
    [SerializeField] private float _maxEnergy = 100f;
    [SerializeField] private float _currentEnergy = 100f;
    [SerializeField] private float _energyBurnRate = 25f;
    [SerializeField] private float _energyRegenRate = 45f;
    [SerializeField] private float _walkRegenMultiplier = 0.5f;
    [SerializeField] private float _regenDelay = 1f;

    private Coroutine _regenCoroutine;
    private SiloInventory _inventory;

    protected override void Awake()
    {
        // Importante: Si la clase base (Character) tiene un Awake, 
        // a veces es mejor llamarlo después de nuestras referencias.
        _currentEnergy = _maxEnergy;
        currentHealth = maxHealth;
        _inventory = new SiloInventory();

        StartCoroutine(_AwakeRoutine());
    }

    // Cambié el nombre para evitar confusiones con Awake()
    private IEnumerator _AwakeRoutine()
    {
        while (SaveManager.Instance == null || SaveManager.Instance.IsLoading)
        {
            yield return null;
        }
        // Si tu clase Character tiene lógica en Awake, asegúrate de que sea accesible o virtual
    }

    private void Update()
    {
        HandleEnergyLogic();
    }

    private void HandleEnergyLogic()
    {
        bool isMoving = InputManager.Instance.MoveInput.magnitude > 0.1f;
        // Solo corre si pulsa el botón, se mueve y tiene energía
        bool isRunning = InputManager.Instance.IsRunning && isMoving && _currentEnergy > 0;

        if (isRunning)
        {
            if (_regenCoroutine != null)
            {
                StopCoroutine(_regenCoroutine);
                _regenCoroutine = null;
            }
            _currentEnergy -= _energyBurnRate * Time.deltaTime;
        }
        else
        {
            if (_currentEnergy < _maxEnergy && _regenCoroutine == null)
            {
                _regenCoroutine = StartCoroutine(RegenEnergyRoutine());
            }
        }

        _currentEnergy = Mathf.Clamp(_currentEnergy, 0, _maxEnergy);
    }

    private IEnumerator RegenEnergyRoutine()
    {
        yield return new WaitForSeconds(_regenDelay);

        while (_currentEnergy < _maxEnergy)
        {
            bool isMoving = InputManager.Instance.MoveInput.magnitude > 0.1f;

            // Si el jugador intenta correr de nuevo, paramos la regeneración
            if (InputManager.Instance.IsRunning && isMoving && _currentEnergy > 0)
                break;

            float currentRegenSpeed = isMoving ? (_energyRegenRate * _walkRegenMultiplier) : _energyRegenRate;

            _currentEnergy += currentRegenSpeed * Time.deltaTime;
            _currentEnergy = Mathf.Clamp(_currentEnergy, 0, _maxEnergy);

            yield return null;
        }

        _regenCoroutine = null;
    }

    public float GetCurrentEnergy() => _currentEnergy;
    public float GetMaxEnergy() => _maxEnergy;

    // Propiedad que lee PlayerMovement
    public bool CanRun => InputManager.Instance.IsRunning && _currentEnergy > 0 && InputManager.Instance.MoveInput.magnitude > 0.1f;

    public void Heal(int amount) => currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

    public override void TakeDamage(int damage) => base.TakeDamage(damage);

    // --- IMPLEMENTATION ---
    private void OnEnable() => SaveManager.Instance?.RegisterSavable(this);
    private void OnDisable() => SaveManager.Instance?.UnregisterSavable(this);
    public string GetSaveID() => "Player";
    public object CaptureState() => new PlayerData(this);
    public void RestoreState(object state)
    {
        if (state is PlayerData data)
        {
            currentBiome = data.currentBiome;
            money = data.money;
            currentHealth = data.health;
            transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        }
    }
}
