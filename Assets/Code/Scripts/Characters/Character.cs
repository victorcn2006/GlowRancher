using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected string characterName;
    [SerializeField, TextArea] protected string description;

    [SerializeField] protected int maxHealth = 3;
    [SerializeField] protected int maxEnergy = 100;

    protected Vector3 position;

    public int currentHealth;
    public int currentEnergy;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
        position = transform.position;
    }

    // Getters
    public string GetCharacterName() => characterName;
    public string GetDescription() => description;
    public int GetMaxEnergy() => maxEnergy;
    public int GetCurrentEnergy() => currentEnergy;
    public int GetMaxHealth() => maxHealth;
    public int GetCurrentHealth() => currentHealth;
    public Vector3 GetPosition() => position;

    // Setters y Lógica
    protected void SetCharacterName(string name) => characterName = name;

    public virtual void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    protected virtual void Attack()
    {
        Debug.Log(characterName + " ataca.");
    }

    public virtual void TakeDamage(int damage)
    {
        damage = Mathf.Max(0, damage);
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        if (currentHealth <= 0) Die();
    }

    protected virtual void Die()
    {
        if (gameObject.activeSelf) gameObject.SetActive(false);
    }

    public void UpdatePosition() => position = transform.position;
}
