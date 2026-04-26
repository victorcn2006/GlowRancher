using DG.Tweening.Core.Easing;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;
    public static GameManager Instance { get { return RequestGameManager(); } }

    [Header("STATS KEYS")]
    private const string TIME_PLAYED = "TIME_PLAYED";
    private const string DEATH_COUNTER = "DEATH_COUNTER";

    [SerializeField] private int _deathCounter;
    [SerializeField] private float _timePlayed;

    private void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(gameObject);
            return;
        }

        gameManager = this;
        DontDestroyOnLoad(gameObject);

        _timePlayed = PlayerPrefs.GetFloat(TIME_PLAYED, 0f);
        _deathCounter = PlayerPrefs.GetInt(DEATH_COUNTER, 0);
    }

    private void Update()
    {
        _timePlayed += Time.deltaTime;
    }


    static GameManager RequestGameManager()
    {
        if (gameManager == null)
        {
            GameObject gameManagerObj = new GameObject("GameManager");
            gameManager = gameManagerObj.AddComponent<GameManager>();

        }
        return gameManager;

    }

    public void SaveStats()
    {
        PlayerPrefs.SetFloat(TIME_PLAYED, _timePlayed);
        PlayerPrefs.SetInt(DEATH_COUNTER, _deathCounter);
        PlayerPrefs.Save();
    }

    public void AddDeathPlayer() {
        _deathCounter++;
        SaveStats();
    }

    public int GetDeathCounter()
    {
        return _deathCounter;
    }

    public float GetCurrentTimePlayed() {
        return _timePlayed;
    }

    private void OnApplicationQuit()
    {
        SaveStats();
    }
}
