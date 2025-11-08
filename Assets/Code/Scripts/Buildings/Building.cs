using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] protected string buildingName;
    [SerializeField, TextArea] protected string description;
    private int numBuildings;
    protected Vector3 position;
    protected virtual void Awake(){
        numBuildings = 0;
        position = transform.position;
    }
    protected virtual void Start(){ }
    protected void AddBuilding(){
        numBuildings++;
    }
    // ===================== GETTERS ===================== //
    public string GetBuildingName() => buildingName;
    public string GetDescription() => description;
    public Vector3 GetPosition() => position;
    public int GetNumBuildings() => numBuildings;

    // ===================== SETTERS ===================== //
    protected void SetBuildingName(string name) => buildingName = name;
    protected void SetDescription(string desc) => description = desc;
    protected void SetPosition(Vector3 pos) => position = pos;
}
