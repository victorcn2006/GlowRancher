using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGem", menuName = "Items/Drops/Gem")]
public class GemData : ScriptableObject
{
    public string gemName;
    public string description;
    public int value;
    public string id;

    private void OnEnable()
    {
        GenerateUniqueID();
    }

    //funcio de la id
    public void GenerateUniqueID()
    {
        id = Guid.NewGuid().ToString();  //Genera una id i la agrega
    }
}
