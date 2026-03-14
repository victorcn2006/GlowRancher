using System.Collections;
using System.Collections.Generic;
using FMOD;
using TMPro;
using UnityEngine;

public class IATextNarrator : MonoBehaviour
{
    //poner iconos de "error" --> posibles iconos: #@%

    [SerializeField] private TextMeshProUGUI _iAText;
    [SerializeField] private List<char> _errorCharacters;
    [SerializeField] private float timeBetweenCharacters;
    [SerializeField] private float errorTimeBetweenCharacters;

    [SerializeField, Tooltip("Indica entre que cantidad de letras puede aparecer un 'glitch' Ejemplo: errorProbability = 10, hay 1 posibilidad entre 10 de que 'glitchee'.")]
    private int _errorProbability;

    //private IANarratorManager _iANarratorManager;
    private IASoundNarrator _iASoundNarrator;

    void Awake()
    {
        //_iANarratorManager = GetComponent<IANarratorManager>();
        _iASoundNarrator = GetComponent<IASoundNarrator>();
    }

    public IEnumerator NarrateLine(string textToNarrate)
    {
        _iAText.text = "";
        foreach(char character in textToNarrate)
        {

            if (Random.Range(0, _errorProbability) == 0)
            {
                char errorCharacter = _errorCharacters[Random.Range(0, _errorCharacters.Count)];

                WriteLetter(errorCharacter);
                yield return new WaitForSeconds(errorTimeBetweenCharacters);
            }
            else
            {
                WriteLetter(character);
                yield return new WaitForSeconds(timeBetweenCharacters);
            } 
        }
    }
    
    private void WriteLetter(char character)
    {
        _iAText.text += character.ToString();
        _iASoundNarrator.Narrate(character);
    }

    public List<char> GetErrorCharacters()
    {
        return _errorCharacters;
    }

}
