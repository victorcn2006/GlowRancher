using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IATextNarrator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _iAText;
    [SerializeField] private float _timeBetweenCharacters = 0.05f;
    [SerializeField] private List<char> _errorCharacters = new List<char> { '#', '@', '%' };
    [SerializeField] private float _errorTimeBetweenCharacters = 0.02f;
    [SerializeField, Range(0, 100)] private int _errorChance = 10;

    private IANarratorManager _iANarratorManager;
    private IASoundNarrator _iASoundNarrator;

    void Awake()
    {
        _iANarratorManager = GetComponent<IANarratorManager>();
        _iASoundNarrator = GetComponent<IASoundNarrator>();
    }

    public IEnumerator NarrateLine(string textToNarrate)
    {
        _iAText.text = "";
        foreach(char character in textToNarrate)
        {
            if (_errorChance > 0 && Random.Range(0, _errorChance) == 0 && character != ' ')
            {
                char errorCharacter = _errorCharacters[Random.Range(0, _errorCharacters.Count)];
                WriteLetter(errorCharacter);
                yield return new WaitForSeconds(_errorTimeBetweenCharacters);
            }
            else
            {
                WriteLetter(character);
                yield return new WaitForSeconds(_timeBetweenCharacters);
            }
        }
        _iANarratorManager.DialogueLineFinished();
    }

    private void WriteLetter(char character)
    {
        _iAText.text += character.ToString();
        if (_iASoundNarrator != null) _iASoundNarrator.Narrate(character);
    }

    public List<char> GetErrorCharacters() => _errorCharacters;
}
