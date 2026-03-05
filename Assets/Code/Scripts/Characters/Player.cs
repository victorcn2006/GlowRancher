using System.Collections;
using UnityEngine;

public class Player : Character, ISavable
{
    public string currentBiome { get; private set; } = "Pradera";
    public int money { get; private set; } = 0;
    public int stamina { get; private set; } = 100;
    private SiloInventory _inventory;

    protected override void Awake()
    {
        StartCoroutine(_Awake());
        // Inicializamos la vida si no viene de un guardado previo
        currentHealth = maxHealth;
        _inventory = new SiloInventory();
    }

    IEnumerator _Awake()
    {
        while (SaveManager.Instance == null || SaveManager.Instance.IsLoading)
            yield return null;
        base.Awake();
    }

    // --- LÓGICA DE SALUD ---

    public void Heal(int amount)
    {
        currentHealth += amount;

        // Limitar la curación para que no sobrepase el máximo
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        // Aquí podrías añadir lógica de muerte si currentHealth <= 0
    }

    // --- SISTEMA DE GUARDADO (ISAVABLE) ---

    private void OnEnable() => SaveManager.Instance?.RegisterSavable(this);
    private void OnDisable() => SaveManager.Instance?.UnregisterSavable(this);

    public string GetSaveID() => "Player";
    public object CaptureState() => new PlayerData(this);

    public void RestoreState(object state)
    {
        if (state is PlayerData data)
        {
            characterName = data.characterName;
            currentBiome = data.currentBiome;
            money = data.money;
            currentHealth = data.health;
            maxHealth = data.maxHealth;
            stamina = data.stamina;
            transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        }
    }
}
