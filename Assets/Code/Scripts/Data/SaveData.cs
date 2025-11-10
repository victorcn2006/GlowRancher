[System.Serializable]
public class SaveData {
    public PlayerData playerData;
    public EnemyData[] enemiesData;
    public FarmData farmData;
    public SaveData(Player player, Enemy[] enemies, Farm farm) {
        playerData = new PlayerData(player);
        enemiesData = new EnemyData[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            enemiesData[i] = new EnemyData(enemies[i]);
        }
        farmData = new FarmData(farm);
    }
}