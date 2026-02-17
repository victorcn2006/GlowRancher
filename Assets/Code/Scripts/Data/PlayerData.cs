using UnityEngine;
[System.Serializable]
public class PlayerData{
    [Header("Character Info")]
    public string characterName, description, currentBiome;
    [Header("Character Stats")]
    public int health, money, stamina, maxHealth;
    [Header("Transform Data")]
    public float[] position = new float[3];
    
    public PlayerData(Player player) {
        characterName = player.GetCharacterName();
        description = player.GetDescription();
        currentBiome = player._currentBiome;
        health = player.GetCurrentHealth();
        money = player._money;
        stamina = player._stamina;
        maxHealth = player.GetMaxHealth();
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
