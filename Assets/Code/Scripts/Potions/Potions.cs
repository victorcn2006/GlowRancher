using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPotion", menuName = "Items/Drops/Potion")]
public class Potions : ScriptableObject
{
    public string potionName;
    public string description;
    public int cure;
    public string id;

}
