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
        data.amountBuildings = PlayerPrefs.GetInt("AMOUNT_BUILDINGS", 0);
        data.tutorialUnlocked = PlayerPrefs.GetInt("TUTORIAL_UNLOCKED", 0) == 1;
        data.amountPlayersGamePassed = PlayerPrefs.GetInt("GAME_PASSED_COUNT", 0);
        data.moneyAmount = PlayerPrefs.GetFloat("MONEY_AMOUNT", 0f);
        data.buildingSystemUsed = PlayerPrefs.GetInt("BUILDING_SYSTEM_USED", 0);
        data.amountShopUsed = PlayerPrefs.GetInt("SHOP_USED_COUNT", 0);
        data.jumpAmount = PlayerPrefs.GetInt("JUMP_AMOUNT", 0);
        data.aspirateAmountObjects = PlayerPrefs.GetInt("ASPIRATE_AMOUNT", 0);
        data.shopAmountObjects = PlayerPrefs.GetInt("SHOP_OBJECTS_COUNT", 0);
        data.siloAmountOpened = PlayerPrefs.GetInt("SILO_OPENED_COUNT", 0);
        data.puzzleTime1 = PlayerPrefs.GetFloat("PUZZLE_TIME_1", 0f);
        data.puzzleTime2 = PlayerPrefs.GetFloat("PUZZLE_TIME_2", 0f);
        data.puzzleTime3 = PlayerPrefs.GetFloat("PUZZLE_TIME_3", 0f);
        data.puzzleTime4 = PlayerPrefs.GetFloat("PUZZLE_TIME_4", 0f);
        data.puzzleTime5 = PlayerPrefs.GetFloat("PUZZLE_TIME_5", 0f);
        data.sellShopAmount = PlayerPrefs.GetInt("SELL_SHOP_AMOUNT", 0);
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
        PlayerPrefs.SetInt("AMOUNT_BUILDINGS", data.amountBuildings);
        PlayerPrefs.SetInt("TUTORIAL_UNLOCKED", data.tutorialUnlocked ? 1 : 0);
        PlayerPrefs.SetInt("GAME_PASSED_COUNT", data.amountPlayersGamePassed);
        PlayerPrefs.SetFloat("MONEY_AMOUNT", data.moneyAmount);
        PlayerPrefs.SetInt("BUILDING_SYSTEM_USED", data.buildingSystemUsed);
        PlayerPrefs.SetInt("SHOP_USED_COUNT", data.amountShopUsed);
        PlayerPrefs.SetInt("JUMP_AMOUNT", data.jumpAmount);
        PlayerPrefs.SetInt("ASPIRATE_AMOUNT", data.aspirateAmountObjects);
        PlayerPrefs.SetInt("SHOP_OBJECTS_COUNT", data.shopAmountObjects);
        PlayerPrefs.SetInt("SILO_OPENED_COUNT", data.siloAmountOpened);
        PlayerPrefs.SetFloat("PUZZLE_TIME_1", data.puzzleTime1);
        PlayerPrefs.SetFloat("PUZZLE_TIME_2", data.puzzleTime2);
        PlayerPrefs.SetFloat("PUZZLE_TIME_3", data.puzzleTime3);
        PlayerPrefs.SetFloat("PUZZLE_TIME_4", data.puzzleTime4);
        PlayerPrefs.SetFloat("PUZZLE_TIME_5", data.puzzleTime5);
        PlayerPrefs.SetInt("SELL_SHOP_AMOUNT", data.sellShopAmount);
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

        Debug.Log($"[GameManager] Stats saved successfully:\n" +
                  $"- Time: {data.timePlayed:F2}s\n" +
                  $"- Money: {data.moneyAmount}\n" +
                  $"- Deaths: {data.deathCounter}\n" +
                  $"- Jumps: {data.jumpAmount}\n" +
                  $"- Buildings: {data.buildingSystemUsed}\n" +
                  $"- Shop Used: {data.amountShopUsed}\n" +
                  $"- Silos Opened: {data.siloAmountOpened}\n" +
                  $"- Tutorial: {(data.tutorialUnlocked ? "Unlocked" : "Locked")}\n" +
                  $"- Puzzles Best Times: P1:{data.puzzleTime1:F2}s, P2:{data.puzzleTime2:F2}s, P3:{data.puzzleTime3:F2}s, P4:{data.puzzleTime4:F2}s, P5:{data.puzzleTime5:F2}s");
    }

    public void AddDeathPlayer() { data.deathCounter++; SaveStats(); }
    public float GetCurrentTimePlayed() => data.timePlayed;
    public int GetDeathCounter() => data.deathCounter;

    public void SetBuildingAmount() {
        data.buildingSystemUsed++; SaveStats();
        
    }
    public int GetBuildingAmountEdit() => data.amountBuildings;

    public void AddPlayerGamePassed() { data.amountPlayersGamePassed++; SaveStats(); }
    public int GetPlayersGamePassed() => data.amountPlayersGamePassed;

    public void AddShopUsedAmount() { data.amountShopUsed++; SaveStats(); }
    public int GetAmountShopUsed() => data.amountShopUsed;

    public void AddJumpAmount() { data.jumpAmount++; SaveStats(); }
    public int GetJumpAmount() => data.jumpAmount;

    public void AddSiloOpened() { data.siloAmountOpened++; SaveStats(); }
    public int GetSiloOpenedAmount() => data.siloAmountOpened;

    public void AddMoneyAmount(float money) { data.moneyAmount = money; SaveStats(); }
    public float GetMoneyAmount() => data.moneyAmount;

    public void TutorialUnlocked() { data.tutorialUnlocked = true; SaveStats(); }
    public bool IsTutorialUnlocked() => data.tutorialUnlocked;

    public void AddSellAmount() { data.sellShopAmount++; SaveStats(); }
    public int GetSellAmount() => data.sellShopAmount;

    public void SetAspirableAmountItems() { data.aspirateAmountObjects++; SaveStats(); }
    public int GetAspirableAmountItems() => data.aspirateAmountObjects;

    public void SetBuildingsAmount() { data.buildingSystemUsed++; SaveStats(); }
    public int GetBuildingsAmount() => data.buildingSystemUsed;

    public void SetTimePuzzle(float time, int monolitoNumber) {
        switch (monolitoNumber) {
            case 0:
                if (data.puzzleTime1 == 0 || time < data.puzzleTime1) data.puzzleTime1 = time;
                break;
            case 1:
                if (data.puzzleTime2 == 0 || time < data.puzzleTime2) data.puzzleTime2 = time;
                break;
            case 2:
                if (data.puzzleTime3 == 0 || time < data.puzzleTime3) data.puzzleTime3 = time;
                break;
            case 3:
                if (data.puzzleTime4 == 0 || time < data.puzzleTime4) data.puzzleTime4 = time;
                break;
            case 4:
                if (data.puzzleTime5 == 0 || time < data.puzzleTime5) data.puzzleTime5 = time;
                break;
            default:
                return;
        }
        SaveStats();
    }
    public float GetTimePuzzle(int numPuzzle) {
        switch (numPuzzle)
        {
            case 0:
                return data.puzzleTime1;
            case 1:
                return data.puzzleTime2;
            case 2:
                return data.puzzleTime3;
            case 3:
                return data.puzzleTime4;
            case 4:
                return data.puzzleTime5;
            default:
                return 0;
        }
    }
    private void OnApplicationQuit() => SaveStats();
}
