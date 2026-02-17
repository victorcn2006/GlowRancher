using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character, ISavable{

    [Header("Estadísticas del Jugador")]
    [SerializeField] public int _money = 0;
    [SerializeField] public int _stamina = 100;
    [SerializeField] public string _currentBiome = "Pradera";

    public int Money => _money;
    public int Stamina => _stamina;
    public string CurrentBiome => _currentBiome;

    protected override void Awake()
    {
        base.Awake();

        //Inicializqación local
        currentHealth = maxHealth;
    }

   private void Start()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.RegisterSavable(this); 
        }
    }

    private void OnDestroy()
    {
        if(SaveManager.Instance != null)
        {
            SaveManager.Instance.UnregisterSavable(this);
        }
    }

    //Override al attack cambiar por la logica nueva de la aspiradora
    protected override void Attack() {


        if (SaveManager.Instance != null && SaveManager.Instance.IsLoading)
            return;

        base.Attack();
    }

    public override void TakeDamage (int damage)
    {
        base.TakeDamage(damage);
        Debug.Log("Jugador a recibido daño");
    } 
    public void AddMoney(int amount)
    {
        _money += amount;
        // Notifica el SaveManager del canvi
    }

    // Metodo para gastar dinero
    public void SpendMoney(int amount)
    {
        _money = Mathf.Max(0, _money - amount);
    }

    // Quan el jugador puja de nivell
    public void ChangeBiome(string biome)
    {
        if (!string.IsNullOrEmpty(biome))
        {
            _currentBiome = biome;
        }
           
    }

    //===================== METODOS DE ISAVABLE =====================//
    public string GetSaveID()
    {
        return "Player_Main_Data";
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
            currentHealth = data.health;
            maxHealth = data.maxHealth;

            _currentBiome = data.currentBiome;
            _money = data.money;
            _stamina = data.stamina;

            transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        }
        else
        {
            Debug.LogWarning("RestoreState: el objeto no es de tipo PlayerData para " + GetSaveID());
        }
    }
}
