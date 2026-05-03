using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BrightnessManager : MonoBehaviour
{
    public static BrightnessManager instance;

    private const string BRIGHTNESS_KEY = "brightness";
    private const float DEFAULT_BRIGHTNESS = 0.5f;

    private Volume _globalVolume;
    private ColorAdjustments _colorAdjustments;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
            // Ensure it's a root object for DontDestroyOnLoad to work correctly
            if (transform.parent != null)
            {
                transform.SetParent(null);
            }
            
            DontDestroyOnLoad(gameObject);
            InitializePostProcessing();
            LoadAndApplyBrightness();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializePostProcessing()
    {
        // Look for an existing global volume
        Volume[] volumes = FindObjectsByType<Volume>(FindObjectsSortMode.None);
        foreach (var v in volumes)
        {
            if (v.isGlobal)
            {
                _globalVolume = v;
                Debug.Log($"[BrightnessManager] Found existing global volume: {v.name}");
                break;
            }
        }

        // Create a dedicated global volume if none found
        if (_globalVolume == null)
        {
            GameObject volumeObject = new GameObject("GlobalBrightnessVolume");
            volumeObject.transform.SetParent(transform);
            _globalVolume = volumeObject.AddComponent<Volume>();
            _globalVolume.isGlobal = true;
            _globalVolume.priority = 100;
            Debug.Log("[BrightnessManager] Created new dedicated global volume");
        }

        // Ensure we have a profile
        if (_globalVolume.sharedProfile == null)
        {
            _globalVolume.sharedProfile = ScriptableObject.CreateInstance<VolumeProfile>();
            Debug.Log("[BrightnessManager] Created new VolumeProfile");
        }

        // Ensure we have ColorAdjustments override
        if (!_globalVolume.sharedProfile.TryGet(out _colorAdjustments))
        {
            _colorAdjustments = _globalVolume.sharedProfile.Add<ColorAdjustments>(true);
            Debug.Log("[BrightnessManager] Added ColorAdjustments to profile");
        }

        _colorAdjustments.postExposure.overrideState = true;
        
        // Debug: Check if camera can see this volume's layer
        int volumeLayer = _globalVolume.gameObject.layer;
        Debug.Log($"[BrightnessManager] Volume is on layer: {LayerMask.LayerToName(volumeLayer)} (Index: {volumeLayer})");
    }

    private void LoadAndApplyBrightness()
    {
        float savedBrightness = PlayerPrefs.GetFloat(BRIGHTNESS_KEY, DEFAULT_BRIGHTNESS);
        UpdateBrightness(savedBrightness);
    }

    public void SetBrightness(float value)
    {
        PlayerPrefs.SetFloat(BRIGHTNESS_KEY, value);
        PlayerPrefs.Save();
        UpdateBrightness(value);
    }

    public float GetBrightness()
    {
        return PlayerPrefs.GetFloat(BRIGHTNESS_KEY, DEFAULT_BRIGHTNESS);
    }

    private void UpdateBrightness(float brightness)
    {
        // Re-find volume if it's missing (e.g. after scene load)
        if (_globalVolume == null)
        {
            Volume[] volumes = FindObjectsByType<Volume>(FindObjectsSortMode.None);
            foreach (var v in volumes)
            {
                if (v.isGlobal)
                {
                    _globalVolume = v;
                    break;
                }
            }
        }

        if (_globalVolume == null) return;

        // Ensure we have the effect reference
        if (_colorAdjustments == null && _globalVolume.sharedProfile != null)
        {
            _globalVolume.sharedProfile.TryGet(out _colorAdjustments);
        }

        if (_colorAdjustments != null)
        {
            // Range: -5 (Very Dark) to +2 (Bright)
            // 0.5 is the middle (Standard)
            float postExposureValue = Mathf.Lerp(-5f, 2f, brightness);
            _colorAdjustments.postExposure.value = postExposureValue;
            
            // Safety: Ensure it's active
            _colorAdjustments.active = true;
            _colorAdjustments.postExposure.overrideState = true;

            Debug.Log($"[BrightnessManager] APPLIED -> Slider: {brightness:F2} | PostExposure: {postExposureValue:F2}");
        }
        else
        {
            // Fallback: If for some reason the effect isn't there, we use Weight
            // Slider 1.0 -> Weight 0 (Normal)
            // Slider 0.0 -> Weight 1 (Full Effect/Dark)
            // This assumes you have a profile that makes things dark.
            _globalVolume.weight = 1f - brightness;
            Debug.Log($"[BrightnessManager] FALLBACK -> Volume Weight: {_globalVolume.weight:F2}");
        }
    }
}
