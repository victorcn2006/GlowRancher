using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] protected string buildingName;
    [SerializeField, TextArea] protected string description;

    protected Vector3 position;
    protected virtual void Awake(){
        position = transform.position;
    }
    protected virtual void Start() { }
    // ===================== GETTERS ===================== //
    public string GetBuildingName() => buildingName;
    public string GetDescription() => description;
    public Vector3 GetPosition() => position;

    // ===================== SETTERS ===================== //
    protected void SetBuildingName(string name) => buildingName = name;
    protected void SetDescription(string desc) => description = desc;
    protected void SetPosition(Vector3 pos) => position = pos;
}
