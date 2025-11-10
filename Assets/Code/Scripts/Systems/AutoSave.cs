using System.Collections;
using UnityEngine;

public class AutoSave : MonoBehaviour{
    public static AutoSave Instance {get; private set; }
    [Header("Add the autosave icon")]
    [SerializeField] private GameObject iconImage;
    private float currentTime;
    private const float TIME_SAVE = 1f;
    private float minutes;
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
        if (minutes >= TIME_SAVE)
        {
            UnityEvents.Instance.WriteData.Invoke();
            Debug.Log("Guardado");
            currentTime = 0f;
            StartCoroutine(SaveCoroutine());
        }
    }
    private void UpdateTime(float time){
        currentTime += Time.deltaTime;
        minutes = currentTime / 60f;
    }
    public float GetCurrentTime() {  
        return currentTime; 
    }
    public float GetCurrentMinutes() {
        return minutes;
    }
    private IEnumerator SaveCoroutine() {
        iconImage.SetActive(true);
        yield return new WaitForSeconds(10f);
        iconImage.SetActive(false);
    }
}
