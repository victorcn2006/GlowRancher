using UnityEngine;

[CreateAssetMenu(fileName = "NewVegetable", menuName = "Items/Drops/Vegetables")]
public class Vegetable : ScriptableObject
{
    public string vegetableName;
    public string description;
    public string id;
    public float time = 0f;

}
