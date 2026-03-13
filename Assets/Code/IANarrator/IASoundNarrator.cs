using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class IASoundNarrator : MonoBehaviour
{

    //private IANarratorManager _iANarratorManager;
    private IATextNarrator _iATextNarrator;

    [SerializeField] private List<AudioClip> _iaSounds = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _iaErrorSounds = new List<AudioClip>();

    [SerializeField] private AudioSource _audioSource;
    void Awake()
    {
        //_iANarratorManager = GetComponent<IANarratorManager>();
        _iATextNarrator = GetComponent<IATextNarrator>();
    }



    //hacer que si llegan letras de "error" suene diferente


    public void Narrate(char character)
    {
        if (!_iATextNarrator.GetErrorCharacters().Contains(character)) MakeSound(character); // si no es un character de error emite un sonido normal
        else MakeErrorSound(); // si es un sonido de error emite un sonido de error
    }

    private void MakeSound(char character)
    {
        if (_iaSounds.Count <= 0) return;

        if (character == ' ' || character == '.') return; // si es un espacio o un punto no genera sonido
        else _audioSource.PlayOneShot(_iaSounds[Random.Range(0, _iaSounds.Count)]); // si no es un espacio o punto emite un sonido

    }

    private void MakeErrorSound()
    {
        if (_iaErrorSounds.Count <= 0) return;

        else _audioSource.PlayOneShot(_iaErrorSounds[Random.Range(0, _iaErrorSounds.Count)]);

    }

}
