using UnityEngine;

public class Character : MonoBehaviour {
    [SerializeField] protected string characterName;
    [SerializeField, TextArea] protected string description;
    /*maxHealth es protected porque las hijas podran acceder a la variable debido a su proteccion, 
     * tambien es virtual ya que los hijos lo podran sobreescribir ya que cada character tiene vida diferente
     * se asigna 3 por defecto
     */
    [SerializeField] protected int maxHealth;
    protected Vector3 position;
    //Se va actualizando y es protected accesible para los hijos
    protected int currentHealth;
    //Velocidad de la entidad, override para sobreescribir, por eso es virtual
    protected virtual void Awake(){
        currentHealth = maxHealth;
        position = transform.position;
    }
    //==========================GETTERS======================
    public string GetCharacterName() { return characterName; }
    public string GetDescription() { return description; }
    public int GetMaxHealth() {  return maxHealth; }
    public int GetCurrentHealth() { return currentHealth; }
    public Vector3 GetPosition() { return position; }

    //==========================SETTERS======================
    protected void SetCharacterName(string name) {
        characterName = name;
    }
    protected void SetDescription(string description) { 
        this.description = description; 
    }
    protected void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log(characterName + " ha sido curado. Vida actual: " + currentHealth);
    }

    //======================METODOS OVERRIDE===================
    protected virtual void Attack()
    {
        Debug.Log(characterName + " ataca.");
    }

    public virtual void TakeDamage(int damage)
    {
        //Evita valores negativos
        damage = Mathf.Max(0, damage);
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        UnityEvents.Instance?.OnDamage.Invoke();
        Debug.Log(characterName + " recibe " + damage + " de daño. Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
            UnityEvents.Instance?.OnDeath.Invoke();
        }
    }

    protected virtual void Die()
    {
        Debug.Log(characterName + " ha muerto.");
        if(gameObject.activeSelf)
            gameObject.SetActive(false);
    }
    public void UpdatePosition() => position = transform.position;
}
