using UnityEngine;
[System.Serializable]
public class FarmData
{
    [Header("Building Info")]
    public string buildingName, description;
    public float[] position = new float[3];
    public FarmData(Farm farm){ 
        buildingName = farm.GetBuildingName();
        description = farm.GetDescription();
        position[0] = farm.transform.position.x;
        position[1] = farm.transform.position.y;
        position[2] = farm.transform.position.z;
    }
}
