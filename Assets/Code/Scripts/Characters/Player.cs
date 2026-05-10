using System.Collections;
using UnityEngine;

public class Player : Character
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
    [SerializeField] private const float REGENDELAY = 1f;

    private Coroutine _regenCoroutine;
    private SiloInventory _inventory;

    //tiempo que tiene que pasar sin recibr daño para que empieze a curar
    private const float TIMER_TO_HEAL = 10f;
    private float _healTimer;

    //tiempo que tiene que pasar entre cada heal una vez puede empezar a curarse
    private const float HEALING_TIC_TIMER = 0.5f;
    private float _healingTimer;

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

        _healTimer += Time.deltaTime;
        if(_healTimer >= TIMER_TO_HEAL)
        {
            _healingTimer += Time.deltaTime;
            if (_healingTimer >= HEALING_TIC_TIMER)
            {
                Heal(2);
                _healingTimer = 0;
            }
        }

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
        yield return new WaitForSeconds(REGENDELAY);

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
    public void AddMaxEnergy() { _maxEnergy += 10; } 

    // Propiedad que lee PlayerMovement
    public bool CanRun => InputManager.Instance.IsRunning && _currentEnergy > 0 && InputManager.Instance.MoveInput.magnitude > 0.1f;

    //public void Heal(int amount) => currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    public void Heal(int amount) {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

    } 

    //public override void TakeDamage(int damage) => base.TakeDamage(damage);
    public override void TakeDamage(int damage)
    {
        Debug.Log("Dañado");
        base.TakeDamage(damage);
        _healTimer = 0;
        _healingTimer = 0;
    } 

    // --- IMPLEMENTATION ---
    public string GetSaveID() => "Player";

    protected override void Die()
    {
        // --- Start: Update deathCounter via GameManager ---
        // Call GameManager to handle death count increment and save.
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddDeathPlayer();
        }
        // --- End: Update deathCounter via GameManager ---

        DeathScript.instance.Die();
        currentHealth = maxHealth;
    }
}
