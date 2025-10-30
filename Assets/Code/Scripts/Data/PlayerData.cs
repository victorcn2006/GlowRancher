using UnityEngine;
[System.Serializable]
public class PlayerData{
    [Header("Character Info")]
    public string characterName, description, currentBiome;
    [Header("Character Stats")]
    public int health, money, stamina, maxHealth;
    [Header("Transform Data")]
    public float moveSpeed;
    public float[] position = new float[3];
    
    public PlayerData(Player player) {
        characterName = player.GetCharacterName();
        description = player.GetDescription();
        currentBiome = player.currentBiome;
        health = player.GetCurrentHealth();
        money = player.money;
        stamina = player.stamina;
        maxHealth = player.GetMaxHealth();
        moveSpeed = player.GetMoveSpeed();
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
