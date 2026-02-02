using System.Collections;
using UnityEngine;

public class AutoSave : MonoBehaviour{
    public static AutoSave Instance {get; private set; }
    [Header("Add the autosave icon")]
    [SerializeField] private GameObject _iconImage;
    private float _currentTime;
    private const float TIME_SAVE = 1f;
    private float _minutes;
    private void Awake() {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }
    private void Update() {
        UpdateTime(Time.deltaTime);
        AutoSavingData();

    }
    private void AutoSavingData() {
        if (_minutes >= TIME_SAVE)
        {
            UnityEvents.Instance.WriteData.Invoke();
            Debug.Log("Guardado");
            _currentTime = 0f;
            StartCoroutine(SaveCoroutine());
        }
    }
    private void UpdateTime(float time){
        _currentTime += Time.deltaTime;
        _minutes = _currentTime / 60f;
    }
    public float Get_currentTime() {  
        return _currentTime; 
    }
    public float GetCurrent_minutes() {
        return _minutes;
    }
    private IEnumerator SaveCoroutine() {
        _iconImage.SetActive(true);
        yield return new WaitForSeconds(10f);
        _iconImage.SetActive(false);
    }
}
