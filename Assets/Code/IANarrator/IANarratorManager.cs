using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IANarratorManager : MonoBehaviour
{
    private static IANarratorManager instance;
    public static IANarratorManager Instance => instance;

    [SerializeField] private GameObject _dialogueGO;
    private Dialogue _currentDialogue;
    private int _currentLineIndex;
    private bool _dialogDisplaying;
    private bool _dialogLineFinished;
    private IATextNarrator _iATextNarrator;

    private void OnEnable() { if (InputManager.Instance != null) InputManager.Instance.OnEnterPerformed.AddListener(NextLineDialog); }
    private void OnDisable() { if (InputManager.Instance != null) InputManager.Instance.OnEnterPerformed.RemoveListener(NextLineDialog); }

    void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }
        _iATextNarrator = GetComponent<IATextNarrator>();
    }

    public void StartNewDialog(Dialogue newDialog)
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
            if (_currentLineIndex >= _currentDialogue.lines.Count) { _dialogueGO.SetActive(false); _dialogDisplaying = false; }
            else { _dialogLineFinished = false; StartCoroutine(_iATextNarrator.NarrateLine(_currentDialogue.lines[_currentLineIndex])); }
        }
    }

    public void DialogueLineFinished() => _dialogLineFinished = true;
}
