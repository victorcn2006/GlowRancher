using UnityEngine;
using UnityEngine.Events;

public class UnityEvents : MonoBehaviour{
    public static UnityEvents Instance { get; private set; }
    [HideInInspector] public UnityEvent WriteData = new UnityEvent();
    [HideInInspector] public UnityEvent OnDamage = new UnityEvent();
    [HideInInspector] public UnityEvent OnDeath = new UnityEvent();
    private void Awake() {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }
}
