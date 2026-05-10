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
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
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

        if (MongoDBReader.Instance != null)
            _ = MongoDBReader.Instance.SaveStats(data);
    }

    public void AddDeathPlayer() { data.deathCounter++; SaveStats(); }
    public float GetCurrentTimePlayed() => data.timePlayed;
    public int GetDeathCounter() => data.deathCounter;
    private void OnApplicationQuit() => SaveStats();
}
