using System.Collections;
using UnityEngine;

public class Player : Character, ISavable{
    //Variables opcionales con Getter publico y setter privado
    [RequiredField, SerializeField] GameObject gm;
    public string currentBiome { get; private set; } = "Pradera";
    public int money { get; private set; } = 0;
    public int stamina { get; private set; } = 100;


    //Override al Awake del Character para asignar valores a las variables
    protected override void Awake() {
        StartCoroutine(_Awake());
        currentHealth = maxHealth;
    }
    IEnumerator _Awake() {
        while (SaveManager.Instance == null || SaveManager.Instance.IsLoading)
            yield return null;
        base.Awake();
    }

    private void OnEnable()
    {
        SaveManager.Instance?.RegisterSavable(this);
    }
    private void OnDisable()
    {
        SaveManager.Instance?.UnregisterSavable(this);
    }
    //Override al attack cambiar por la logica nueva de la aspiradora
    protected override void Attack() {
        if (SaveManager.Instance != null && SaveManager.Instance.IsLoading)
            return;
        base.Attack();
    }
    public void AddMoney(int amount) {
        money += amount;
        // Notifica el SaveManager del canvi
    }

    // Metodo para gastar dinero
    public void SpendMoney(int amount) {
        money = Mathf.Max(0, money - amount);
    }

    // Quan el jugador puja de nivell
    public void ChangeBiome(string biome) {
        if(!string.IsNullOrEmpty(biome))
            currentBiome = biome;
    }

    // Quan el jugador rep dany
    protected override void TakeDamage(int damage) {
        base.TakeDamage(damage);
    } 

    //===================== METODOS DE ISAVABLE =====================//
    public string GetSaveID()
    {
        return "Player";
    }
    public object CaptureState()
    {
        return new PlayerData(this);
    }
    public void RestoreState(object state)
    {
        /*Si el objeto que me pasas es del tipo PlayerData usa data para restaurar 
        el estado ya que tiene toda la info*/
        if (state is PlayerData data)
        {
            characterName = data.characterName;
            description = data.description;
            currentBiome = data.currentBiome;
            money = data.money;
            currentHealth = data.health;
            maxHealth= data.maxHealth;
            moveSpeed = data.moveSpeed;
            stamina = data.stamina;
            transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        }
        else
        {
            Debug.LogWarning("RestoreState: el objeto no es de tipo PlayerData para " + GetSaveID());
        }
    }
}