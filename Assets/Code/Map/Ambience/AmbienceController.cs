using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AmbienceController : MonoBehaviour
{
    public static AmbienceController Instance;

    public enum AmbienceStates
    {
        CORRUPTED,
        ALIVE
    }

    [Header("GAME OBJECTS NEEDED")]
    [SerializeField, RequiredField] private GameObject _directionalLightGO;
    [SerializeField, RequiredField] private GameObject _skySphereGO;

    [Header("VALUES NEEDED")]
    [SerializeField, RequiredField] private float _transitionBetweenAmbienceTime;
    [SerializeField, RequiredField] private Ease _easeTransitionType;

    private Light _directionalLight;

    //=====================CORRUPTED SETTINGS=====================\\
    [Space(7)]
    [Header("CORRUPTED SETTINGS")]
    [Header("Directional Light")]  //DONE
    [SerializeField] private Color _directionalLightCorruptedColor;
    [SerializeField] private float _directionalLightCorruptedIntensity;

    [Space(5)]
    [Header("SkyBox")]
    [SerializeField] private float _skyBoxCorruptedAtmosphereThickness;
    [SerializeField] private float _skyBoxCorruptedExposure;
    [SerializeField] private Color _skyBoxCorruptedTint;

    [Space(5)]
    [Header("Fog")]
    [SerializeField] private float _fogCorruptedDensity;
    [SerializeField] private Color _fogColor;


    //=====================ALIVE SETTINGS=====================\\
    [Space(10)]
    [Header("ALIVE SETTINGS")]
    [Header("Directional Light")]
    [SerializeField] private Color _directionalLightAliveColor;
    [SerializeField] private float _directionalLightAliveIntensity;
    // ShadowType "No Shadows";

    [Space(5)]
    [Header("SkyBox")]
    [SerializeField] private float _skyBoxAliveAtmosphereThickness;
    [SerializeField] private float _skyBoxAliveExposure;
    [SerializeField] private Color _skyBoxAliveTint;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        _directionalLight = _directionalLightGO.GetComponent<Light>();
        RenderSettings.fogColor = _fogColor;
        SetAmbience(AmbienceStates.ALIVE);
    }

    
    public void SetAmbience(AmbienceStates ambienceState)
    {

        Debug.Log("SetAmbience");

        //Target Variables
        Color targetLightColor;
        float targetLightIntensity;
        float targetShadowStrength;

        float targetSkyBoxAtmosphereThickness;
        float targetSkyBoxExposure;
        Color targetSkyBoxColor;

        float targetFogDensity;


        if (ambienceState == AmbienceStates.CORRUPTED)
        {

            targetLightColor = _directionalLightCorruptedColor;
            targetLightIntensity = _directionalLightCorruptedIntensity;
            targetShadowStrength = 0f;

            targetSkyBoxAtmosphereThickness = _skyBoxCorruptedAtmosphereThickness;
            targetSkyBoxExposure = _skyBoxCorruptedExposure;
            targetSkyBoxColor = _skyBoxCorruptedTint;
            targetFogDensity = _fogCorruptedDensity;
        }
        else
        {
            targetLightColor = _directionalLightAliveColor;
            targetLightIntensity = _directionalLightAliveIntensity;
            targetShadowStrength = 1f;

            targetSkyBoxAtmosphereThickness = _skyBoxAliveAtmosphereThickness;
            targetSkyBoxExposure = _skyBoxAliveExposure;
            targetSkyBoxColor = _skyBoxAliveTint;
            targetFogDensity = 0f;
        }

        //============= LIGHT =============\\
        _directionalLight.DOColor(targetLightColor, _transitionBetweenAmbienceTime).SetEase(_easeTransitionType);
        _directionalLight.DOIntensity(targetLightIntensity, _transitionBetweenAmbienceTime).SetEase(_easeTransitionType);

        _directionalLight.shadows = LightShadows.Hard;

        DOTween.To(() => _directionalLight.shadowStrength, x => _directionalLight.shadowStrength = x, targetShadowStrength, _transitionBetweenAmbienceTime).SetEase(Ease.OutQuad);

        StartCoroutine(CheckShadowAfterTransition());


        //============= SKYBOX =============\\
        DOTween.To(() => RenderSettings.skybox.GetFloat("_AtmosphereThickness"), x => RenderSettings.skybox.SetFloat("_AtmosphereThickness", x), targetSkyBoxAtmosphereThickness, _transitionBetweenAmbienceTime).SetEase(Ease.OutQuad);
        DOTween.To(() => RenderSettings.skybox.GetFloat("_Exposure"), x => RenderSettings.skybox.SetFloat("_Exposure", x), targetSkyBoxExposure, _transitionBetweenAmbienceTime).SetEase(Ease.OutQuad);
        RenderSettings.skybox.DOColor(targetSkyBoxColor, "_SkyTint" , _transitionBetweenAmbienceTime).SetEase(_easeTransitionType);


        //============= FOG =============\\
        RenderSettings.fog = true;

        DOTween.To(() => RenderSettings.fogDensity, x => RenderSettings.fogDensity = x, targetFogDensity, _transitionBetweenAmbienceTime).SetEase(Ease.OutQuad);

        StartCoroutine(CheckFogAfterTransition());


    }

    private IEnumerator CheckShadowAfterTransition()
    {
        // Espera el tiempo de transición más un pequeño buffer
        yield return new WaitForSeconds(_transitionBetweenAmbienceTime + 1f);

        // Si el target era 0, desactiva las sombras
        if (_directionalLight.shadowStrength == 0f)
        {
            _directionalLight.shadows = LightShadows.None;
        }
    }


    private IEnumerator CheckFogAfterTransition()
    {
        // Espera el tiempo de transición más un pequeño buffer
        yield return new WaitForSeconds(_transitionBetweenAmbienceTime + 1f);

        // Si el target era 0, desactiva las sombras
        if (RenderSettings.fogDensity == 0f)
        {
            RenderSettings.fog = false;
        }
    }

}
