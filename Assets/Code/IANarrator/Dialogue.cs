using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Narrator/Dialogue")]
public class Dialogue : ScriptableObject
{
    [TextArea(3, 10)]
    public List<string> lines = new List<string>();
}
