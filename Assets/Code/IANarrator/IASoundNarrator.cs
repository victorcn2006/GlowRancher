using UnityEngine;
using FMODUnity; // Necesario para EventReference y RuntimeManager
using FMOD.Studio;

public class IASoundNarrator : MonoBehaviour
{
    private IATextNarrator _iATextNarrator;

    [Header("Referencias desde los Banks")]
    [SerializeField] private EventReference _fmodNormalSound;
    [SerializeField] private EventReference _fmodErrorSound;

    [Header("Ajustes de Audio")]
    [Range(0f, 0.5f)]
    [SerializeField] private float _pitchVariation = 0.12f;

    void Awake()
    {
        _iATextNarrator = GetComponent<IATextNarrator>();
    }

    public void Narrate(char character)
    {
        // Ignorar espacios y puntos
        if (character == ' ' || character == '.') return;

        // Seleccionar la referencia según el tipo de carácter
        EventReference selectedEvent = _iATextNarrator.GetErrorCharacters().Contains(character)
            ? _fmodErrorSound : _fmodNormalSound;

        // Verificar si la referencia es válida (si no está vacía)
        if (selectedEvent.IsNull) return;

        PlayFMODEvent(selectedEvent);
    }

    private void PlayFMODEvent(EventReference eventRef)
    {
        // Crear instancia desde la referencia detectada en los Banks
        EventInstance instance = RuntimeManager.CreateInstance(eventRef);

        // Pitch aleatorio para que no suene robótico
        instance.setPitch(Random.Range(1f - _pitchVariation, 1f + _pitchVariation));

        instance.start();
        instance.release();
    }
}
