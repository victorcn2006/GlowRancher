using UnityEngine;
using FMODUnity;

public class FMODVolumeLoader : MonoBehaviour
{
    [Header("VCA Paths to Load")]
    public string[] vcaPaths; // Lista de rutas (ej: vca:/Music, vca:/SFX)

    void Start()
    {
        foreach (string path in vcaPaths)
        {
            LoadAndApplyVolume(path);
        }
    }

    private void LoadAndApplyVolume(string path)
    {
        // 1. Obtener el VCA de FMOD
        FMOD.Studio.VCA vca = RuntimeManager.GetVCA(path);

        // 2. Crear la misma clave que usamos en el Slider
        string key = "Volume_" + path;

        // 3. Si existe el guardado, lo aplicamos. Si no, 1f (máximo).
        if (PlayerPrefs.HasKey(key))
        {
            float savedVol = PlayerPrefs.GetFloat(key);
            vca.setVolume(savedVol);
            Debug.Log($"[FMOD] Cargado volumen {savedVol} para {path}");
        }
        else
        {
            vca.setVolume(1f);
        }
    }
}
