using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IANarratorManager : MonoBehaviour
{

    private static IANarratorManager instance;

    [SerializeField] private List<Dialogue> _generalDialogs = new List<Dialogue>();
    [SerializeField] private GameObject _dialogueGO;

    private Dialogue _currentDialogue;
    private int _currentLineIndex;

    private bool _dialogDisplaying;
    private bool _dialogLineFinished;

    private IATextNarrator _iATextNarrator;


    private void OnEnable()
    {
        InputManager.Instance.OnEnterPerformed.AddListener(NextLineDialog);
    }

    private void OnDisable()
    {
        InputManager.Instance.OnEnterPerformed.RemoveListener(NextLineDialog);
    }

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        _iATextNarrator = GetComponent<IATextNarrator>();
    }

    private void Start()
    {
        _dialogueGO.SetActive(false);
        StartCoroutine(A());
    }

    private IEnumerator A()
    {
        yield return new WaitForSeconds(3f);
        StartNewDialog(_generalDialogs[0]);
    }

    public void StartNewDialog(Dialogue newDialog)
    {

        _dialogueGO.SetActive(true);
        _dialogLineFinished = false;
        _currentDialogue = newDialog; //asigo el dialogo actual
        _currentLineIndex = 0; //seteo el indice de la linea actual del dialogo a 0 (la primera linea)
        _dialogDisplaying = true; //marco el booleano de qu se está mostrando un texto a true

        StartCoroutine(_iATextNarrator.NarrateLine(_currentDialogue.lines[_currentLineIndex])); //llamo a la corrutina de NarrateText para empezar a mostrar el texto 
    }

    private void NextLineDialog()
    {
        if (_dialogDisplaying && _dialogLineFinished) // en vez de testTimer tiene que ser el input de "Enter", para que confirme el _dialogDisplaying y el input
        {
            _dialogLineFinished = false;
            _currentLineIndex++;
            if (_currentLineIndex >= _currentDialogue.lines.Count) FinishDialog();
            else StartCoroutine(_iATextNarrator.NarrateLine(_currentDialogue.lines[_currentLineIndex]));
        }
    }

    private void FinishDialog()
    {
        _dialogDisplaying = false;
        _dialogueGO.SetActive(false);
    }

    public void DialogueLineFinished() => _dialogLineFinished = true;

}
