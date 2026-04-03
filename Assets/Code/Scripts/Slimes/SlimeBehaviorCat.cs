using UnityEngine;

public class SlimeBehaviorCat : MonoBehaviour
{
    [SerializeField] private int _resistanceValue = 1;
    public int ResistanceValue => _resistanceValue;

    // This script now acts as a data provider for IceAreas
}
