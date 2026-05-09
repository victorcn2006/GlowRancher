using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private PlayerData data = new PlayerData { userId = "DefaultPlayer" };
    public event Action OnStatsLoaded;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        // Load local fallback
        data.timePlayed = PlayerPrefs.GetFloat("TIME_PLAYED", 0f);
        data.deathCounter = PlayerPrefs.GetInt("DEATH_COUNTER", 0);
    }

    private async void Start()
    {
        // Wait for MongoDBReader to be ready
        while (MongoDBReader.Instance == null) await Task.Yield();

        PlayerData cloudData = await MongoDBReader.Instance.LoadStats(data.userId);
        if (cloudData != null)
        {
            data = cloudData;
            OnStatsLoaded?.Invoke();
            Debug.Log("Stats synced from MongoDB.");
        }
    }

    private void Update() => data.timePlayed += Time.deltaTime;

    public void SaveStats()
    {
        PlayerPrefs.SetFloat("TIME_PLAYED", data.timePlayed);
        PlayerPrefs.SetInt("DEATH_COUNTER", data.deathCounter);
        PlayerPrefs.Save();

        // --- Start: Synchronize Player.money with GameManager.data.moneyAmount ---
        // Find the Player instance to get its current money
        Player player = FindObjectOfType<Player>(); // Assumes Player script is on a GameObject in the scene
        if (player != null)
        {
            data.moneyAmount = player.money;
        }
        // --- End: Synchronize Player.money with GameManager.data.moneyAmount ---

        if (MongoDBReader.Instance != null)
            _ = MongoDBReader.Instance.SaveStats(data);
    }

    public void AddDeathPlayer() { data.deathCounter++; SaveStats(); }
    public float GetCurrentTimePlayed() => data.timePlayed;
    public int GetDeathCounter() => data.deathCounter;

    public void SetBuildingAmount() {
        data.buildingSystemUsed++; SaveStats();
        
    }
    public int GetBuildingAmountEdit() => data.amountBuildings;

    public void AddPlayerGamePassed() => data.amountPlayersGamePassed++;
    public int GetPlayersGamePassed() => data.amountPlayersGamePassed;

    public void AddShopUsedAmount() => data.amountShopUsed++;
    public int GetAmountShopUsed() => data.amountShopUsed;

    public void AddJumpAmount() => data.jumpAmount++;
    public int GetJumpAmount() => data.jumpAmount;

    public void AddSiloOpened() => data.siloAmountOpened++;
    public int GetSiloOpenedAmount() => data.siloAmountOpened;

    public void AddMoneyAmount(float money) => data.moneyAmount = money;
    public float GetMoneyAmount() => data.moneyAmount;

    public void TutorialUnlocked() => data.tutorialUnlocked = true;
    public bool IsTutorialUnlocked() => data.tutorialUnlocked;

    public void AddSellAmount() => data.sellShopAmount++;
    public int GetSellAmount() => data.sellShopAmount;

    public void SetAspirableAmountItems() => data.aspirateAmountObjects++;
    public int GetAspirableAmountItems() => data.aspirateAmountObjects;

    public void SetBuildingsAmount() => data.buildingSystemUsed++;
    public int GetBuildingsAmount() => data.buildingSystemUsed;
    private void OnApplicationQuit() => SaveStats();
}
