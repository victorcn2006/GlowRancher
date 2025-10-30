using System.Collections;
using UnityEngine;

public class Farm : Building, ISavable{
    protected override void Awake() {
        StartCoroutine(_Awake());
        AddBuilding();
    }
    protected override void Start()
    {
        StartCoroutine(_Start());
    }
    private void OnEnable()
    {
        SaveManager.Instance?.RegisterSavable(this);
    }
    private void OnDisable()
    {
        SaveManager.Instance?.UnregisterSavable(this);
    }
    IEnumerator _Awake() {
        while (SaveManager.Instance == null || SaveManager.Instance.IsLoading)
            yield return null;
        base.Awake();
    }
    IEnumerator _Start() {
        while (SaveManager.Instance == null || SaveManager.Instance.IsLoading)
            yield return null;
        base.Start();
    }
    //===================== METODOS DE ISAVABLE =====================//
    public string GetSaveID()
    {
        return "Farm";
    }
    public object CaptureState()
    {
        return new FarmData(this);
    }
    public void RestoreState(object state)
    {
        /*Si el objeto que me pasas es del tipo PlayerData usa data para restaurar 
        el estado ya que tiene toda la info*/
        if (state is FarmData data)
        {
            buildingName = data.buildingName;
            description = data.description;
            position = new Vector3(data.position[0], data.position[1], data.position[2]);
            //transform.position = data.position;
        }
        else
        {
            Debug.LogWarning("RestoreState: el objeto no es de tipo FarmData para " + GetSaveID());
        }
    }
    //public Vector3 GetPosition() => transform.position;
    
}
