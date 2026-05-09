using UnityEngine;
using MongoDB.Driver;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

public class MongoDBReader : MonoBehaviour
{
    public static MongoDBReader Instance { get; private set; }
    private IMongoCollection<PlayerData> _collection;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        // Initialize Connection
        var client = new MongoClient("mongodb+srv://victor:victor2006@cluster0.ndzrtvd.mongodb.net/?appName=Cluster0");
        var database = client.GetDatabase("GlowRancher");
        _collection = database.GetCollection<PlayerData>("UserStats");
    }

    public async Task<PlayerData> LoadStats(string userId)
    {
        return await _collection.Find(x => x.userId == userId).FirstOrDefaultAsync();
    }

    public async Task SaveStats(PlayerData data)
    {
        var filter = Builders<PlayerData>.Filter.Eq(x => x.userId, data.userId);
        await _collection.ReplaceOneAsync(filter, data, new ReplaceOptions { IsUpsert = true });
    }
}

[System.Serializable]
[BsonIgnoreExtraElements]
public class PlayerData
{
    public string userId;
    public float timePlayed;
    public int deathCounter;
    public int amountBuildings;
    public bool tutorialUnlocked;
    public int amountPlayersGamePassed;
    public float moneyAmount;
    public int buildingSystemUsed;
    public int amountShopUsed;
    public int jumpAmount;
    public int aspirateAmountObjects;
    public int shopAmountObjects;
    public int siloAmountOpened;
    public float puzzleTime1;
    public float puzzleTime2;
    public float puzzleTime3;
    public float puzzleTime4;
    public float puzzleTime5;
    public int sellShopAmount;
}
