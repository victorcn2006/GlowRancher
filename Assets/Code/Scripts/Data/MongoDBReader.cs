using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;

public class MongoDBReader : MonoBehaviour
{
    public static MongoDBReader Instance { get; private set; }

    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> usersCollection;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    async void Start() {
        string connectionString = "mongodb+srv://victor:victor2006@cluster0.ndzrtvd.mongodb.net/?appName=Cluster0";

        try {
            client = new MongoClient(connectionString);
            Debug.Log("MongoDB client initialized. Connection will be established on first operation.");
        } catch (System.Exception e) {
            Debug.LogError($"MongoDB Initialization Error: {e.Message}");
        }
    }
}
