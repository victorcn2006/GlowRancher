using UnityEngine;

[System.Serializable]
public class EnemyData{
    [Header("Character Info")]
    public string characterName, description;
    [Header("Stats")]
    public int health, maxHealth;
    public float moveSpeed;
    [Header("Transform Data")]
    public float[] position = new float[3];
    
    public EnemyData(Enemy enemy){
        if (enemy == null)
        {
            Debug.LogWarning("EnemyData: Enemy vacio al intentar crear datos.");
            return;
        }
        characterName = enemy.GetCharacterName();
        description = enemy.GetDescription();
        health = enemy.GetCurrentHealth();
        moveSpeed = enemy.GetMoveSpeed();
        maxHealth = enemy.GetMaxHealth();
        position[0] = enemy.transform.position.x;
        position[1] = enemy.transform.position.y;
        position[2] = enemy.transform.position.z;
        
    }
}
