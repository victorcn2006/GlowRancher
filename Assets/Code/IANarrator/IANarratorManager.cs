using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IANarratorManager : MonoBehaviour
{
    [SerializeField] List<Dialogue> _GeneralDialogs = new List<Dialogue>();
    private Dialogue _currentDialogue;
    private int _currentLineIndex;

    private bool _dialogDisplaying;
    private IATextNarrator _iATextNarrator;
    //private IASoundNarrator _iASoundNarrator;

    float testTimer = 7;

    void Awake()
    {
        _iATextNarrator = GetComponent<IATextNarrator>();
        //_iASoundNarrator = GetComponent<IASoundNarrator>();
    }

    private void Start()
    {
        StartNewDialog(_GeneralDialogs[0]);
    }

    private void Update()
    {
        testTimer -= Time.deltaTime;
        if (_dialogDisplaying && testTimer <= 0) // en vez de testTimer tiene que ser el input de "Enter", para que confirme el _dialogDisplaying y el input
        {
            testTimer = 20;
            _currentLineIndex++;
            if (_currentLineIndex >= _currentDialogue.lines.Count) FinishDialog();
            else StartCoroutine(_iATextNarrator.NarrateLine(_currentDialogue.lines[_currentLineIndex]));
        }
    }

    public void StartNewDialog(Dialogue newDialog)
    {
        _currentDialogue = newDialog; //asigo el dialogo actual
        _currentLineIndex = 0; //seteo el indice de la linea actual del dialogo a 0 (la primera linea)
        _dialogDisplaying = true; //marco el booleano de qu se está mostrando un texto a true

        StartCoroutine(_iATextNarrator.NarrateLine(_currentDialogue.lines[_currentLineIndex])); //llamo a la corrutina de NarrateText para empezar a mostrar el texto 
    }

    private void FinishDialog()
    {
        _dialogDisplaying = false;
    }
    /*
    public void MakeSound(char character)
    {
        _iASoundNarrator.Narrate(character);
    }

    public List<char> GetErrorCharacters()
    {
        return _iATextNarrator.GetErrorCharacters();
    }*/
    


}
