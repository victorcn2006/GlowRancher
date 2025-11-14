using UnityEngine;

public class UIAudioManager : MonoBehaviour{
    public static UIAudioManager Instance{ get; private set; }
    [Header("Clips de UI")]
    [SerializeField] private AudioClip clickSound;
    
    private AudioSource audioSource;

    private void Awake() {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
    public void PlayClick() => PlaySound(clickSound);
    private void PlaySound(AudioClip clip) {
        if (clip == null) return;
        audioSource.PlayOneShot(clip);
    }
}
