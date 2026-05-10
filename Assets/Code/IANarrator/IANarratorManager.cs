using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IANarratorManager : MonoBehaviour
{
    private static IANarratorManager instance;
    public static IANarratorManager Instance => instance;

    [SerializeField] private GameObject _dialogueGO;
    [SerializeField] private Dialogue _OnboardingDialogue;

    private List<Dialogue> _dialoguesList = new List<Dialogue>();
    private Dialogue _currentDialogue;
    private int _currentLineIndex;
    private bool _dialogDisplaying;
    private bool _dialogLineFinished;
    private IATextNarrator _iATextNarrator;

    private void OnEnable() { if (InputManager.Instance != null) InputManager.Instance.OnEnterPerformed.AddListener(NextLineDialog); }
    private void OnDisable() { if (InputManager.Instance != null) InputManager.Instance.OnEnterPerformed.RemoveListener(NextLineDialog); }

    private void Start()
    {
        StartNewDialog(_OnboardingDialogue);
    }
    void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }
        _iATextNarrator = GetComponent<IATextNarrator>();
    }

    public void AddNewDialogueToQueue(Dialogue newDialogue)
    {
        _dialoguesList.Add(newDialogue);
        if (!_dialogDisplaying)
        {
            NextDialogue();

        }
    }
    public void RemoveOnListDialogue(Dialogue newDialogue) => _dialoguesList.Remove(newDialogue);
    private void StartNewDialog(Dialogue newDialog)
    {
        _dialogueGO.SetActive(true);
        _currentDialogue = newDialog;
        _currentLineIndex = 0;
        _dialogDisplaying = true;
        _dialogLineFinished = false;
        StopAllCoroutines();
        StartCoroutine(_iATextNarrator.NarrateLine(_currentDialogue.lines[_currentLineIndex]));
    }

    private void NextLineDialog()
    {
        if (_dialogDisplaying && _dialogLineFinished)
        {
            _currentLineIndex++;
            if (_currentLineIndex >= _currentDialogue.lines.Count) //si ha acabado el dialogo
            {
                _dialogueGO.SetActive(false); _dialogDisplaying = false;
                RemoveOnListDialogue(_currentDialogue);
                NextDialogue();
            }
            else
            {
                _dialogLineFinished = false; StartCoroutine(_iATextNarrator.NarrateLine(_currentDialogue.lines[_currentLineIndex]));
            }
            
        }
    }

    public void DialogueLineFinished()
    {
        _dialogLineFinished = true;

    } 

    private void NextDialogue()
    {
        if (_dialoguesList.Count > 0)
        {
            StartNewDialog(_dialoguesList[0]);
        }
    }

}
