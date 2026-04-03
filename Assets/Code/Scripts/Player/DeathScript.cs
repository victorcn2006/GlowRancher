using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    public static DeathScript instance;
    public bool firstMonolitoUnlocked;
    [SerializeField] private GameObject _initialPlayerSpawn;
    [SerializeField] private GameObject _monolitoSpawn;

    private void Awake() {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    public void Die() {
        if(!firstMonolitoUnlocked) this.transform.position = _initialPlayerSpawn.transform.position;
        else this.transform.position = _monolitoSpawn.transform.position;
    }


}
