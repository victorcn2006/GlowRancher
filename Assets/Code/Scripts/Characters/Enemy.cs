using UnityEngine;
using System;
using System.Collections;
public class Enemy : Character, ISavable
{
    [Tooltip("Identificador único para guardar/cargar este enemigo.")]
    [SerializeField] private string saveID;
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(saveID))
            saveID = Guid.NewGuid().ToString();
    }

    protected override void Awake()
    {
        StartCoroutine(_Awake());
        currentHealth = maxHealth;
    }

    IEnumerator _Awake()
    {
        while (SaveManager.Instance == null || SaveManager.Instance.IsLoading)
            yield return null;

        base.Awake();
    }

    private void OnEnable()
    {
        SaveManager.Instance?.RegisterSavable(this);
    }
    private void OnDisable()
    {
        SaveManager.Instance?.UnregisterSavable(this);
    }
    protected override void Attack()
    {
        if (SaveManager.Instance?.IsLoading == true)
            return;
        base.Attack();
    }
    protected override void Die()
    {
        base.Die();
    }
    //===================== METODOS DE ISAVABLE =====================//
    public string GetSaveID(){ return saveID; }
    public object CaptureState()
    {
        return new EnemyData(this);
    }
    public void RestoreState(object state)
    {
        /*Si el objeto que me pasas es del tipo EnemyData usa data para restaurar 
        el estado ya que tiene toda la info*/
        if (state is EnemyData data){
            characterName = data.characterName;
            description = data.description;
            currentHealth = data.health;
            moveSpeed = data.moveSpeed;
            maxHealth = data.maxHealth;
            position = new Vector3(data.position[0], data.position[1], data.position[2]);
        }
        else
        {
            Debug.LogWarning("RestoreState: el objeto no es de tipo EnemyData para " + GetSaveID());
        }
    }
}
