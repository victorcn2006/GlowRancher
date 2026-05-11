using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimacionesAspiradora : MonoBehaviour
{
    [SerializeField] private GameObject _energy;
    [SerializeField] private GameObject _energyPivot;
    [SerializeField] private float _energyRotateBaseVelocity;
    [SerializeField] private float _energyRotateAspirateVelocity;
    [SerializeField] private float _accelerationDuration;
    [SerializeField] private float _decelerationDuration;

    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private ParticleSystemForceField _particleForceField;

    private float _currentVelocity;
    private Tween _velocityTween;
    private Tween _rotationTween;

    private void OnEnable()
    {
        _currentVelocity = _energyRotateBaseVelocity;
        StartConstantRotation();
    }

    private void StartConstantRotation()
    {
        _rotationTween?.Kill();
        UpdateRotationTween();
    }

    private void UpdateRotationTween()
    {
        if (_energyPivot == null) return;
        _rotationTween?.Kill();
        float duration = 360f / _currentVelocity;
        _rotationTween = _energyPivot.transform
            .DOLocalRotate(new Vector3(360f, 0f, 0f), duration, RotateMode.LocalAxisAdd)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
    }
    public void StartAspirating()
    {
        if (_particleSystem != null && !_particleSystem.isPlaying)
            _particleSystem.Play();

        _particleForceField.gameObject.SetActive(true);

        _velocityTween?.Kill();
        _velocityTween = DOTween.To(
            () => _currentVelocity,
            velocity =>
            {
                _currentVelocity = velocity;
                UpdateRotationTween();
            },
            _energyRotateAspirateVelocity,
            _accelerationDuration
        ).SetEase(Ease.InOutQuad);
    }

    public void StopAspirating()
    {
        if (_particleSystem != null && _particleSystem.isPlaying)
            _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);

        _particleForceField.gameObject.SetActive(false);

        _velocityTween?.Kill();
        _velocityTween = DOTween.To(
            () => _currentVelocity,
            velocity =>
            {
                _currentVelocity = velocity;
                UpdateRotationTween();
            },
            _energyRotateBaseVelocity,
            _decelerationDuration
        ).SetEase(Ease.InOutQuad);
    }

    private void OnDestroy()
    {
        _velocityTween?.Kill();
        _rotationTween?.Kill();
    }
}
