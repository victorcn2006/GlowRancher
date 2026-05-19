using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class IANarratorManager : MonoBehaviour
{
    private static IANarratorManager instance;
    public static IANarratorManager Instance => instance;

    [SerializeField] private GameObject _dialogueGO;
    [SerializeField] private Dialogue _OnboardingDialogue;
    [SerializeField] private float _timeAutoSkip;
    private float _timer;

    private List<Dialogue> _dialoguesList = new List<Dialogue>();
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
    private void Start()
    {
        StartNewDialog(_OnboardingDialogue);
    }

    private void Update()
    {
        if (_dialogLineFinished && _dialogDisplaying)
        {
            _timer += Time.deltaTime;
            if (_timer >= _timeAutoSkip)
            {
                NextLineDialog();
            }
        }
    }

    public void AddNewDialogueToQueue(Dialogue newDialogue)
    {
        if (newDialogue == null || newDialogue.lines == null || newDialogue.lines.Count == 0)
        {
            Debug.LogWarning("IANarratorManager: Trying to add a null or empty dialogue to the queue.");
            return;
        }

        _dialoguesList.Add(newDialogue);
        if (!_dialogDisplaying)
        {
            NextDialogue();
        }
    }

    public void RemoveOnListDialogue(Dialogue newDialogue) => _dialoguesList.Remove(newDialogue);

    public void StopCurrentDialogue()
    {
        Debug.Log("IANarratorManager: StopCurrentDialogue called. Interrupting current narration.");
        if (_narrationCoroutine != null) StopCoroutine(_narrationCoroutine);
        
        if (_currentDialogue != null)
        {
            _dialoguesList.Remove(_currentDialogue);
        }
        
        _dialogDisplaying = false;
        _currentDialogue = null;
        _currentLineIndex = 0;
        _dialogueGO.SetActive(false);
    }

    private Coroutine _narrationCoroutine;

    private void StartNewDialog(Dialogue newDialog)
    {
        if (newDialog == null || newDialog.lines == null || newDialog.lines.Count == 0)
        {
            Debug.LogWarning("IANarratorManager: Cannot start a null or empty dialogue.");
            _dialogDisplaying = false;
            _dialogueGO.SetActive(false);
            
            // If this was the first in the list, remove it and try next
            if (_dialoguesList.Count > 0 && _dialoguesList[0] == newDialog)
            {
                _dialoguesList.RemoveAt(0);
                NextDialogue();
            }
            return;
        }

        Debug.Log($"IANarratorManager: Starting new dialogue with {newDialog.lines.Count} lines.");
        _dialogueGO.SetActive(true);
        _currentDialogue = newDialog;
        _currentLineIndex = 0;
        _dialogDisplaying = true;
        _dialogLineFinished = false;
        
        if (_narrationCoroutine != null) StopCoroutine(_narrationCoroutine);
        _narrationCoroutine = StartCoroutine(_iATextNarrator.NarrateLine(_currentDialogue.lines[_currentLineIndex]));
    }

    private void NextLineDialog()
    {
        if (!_dialogDisplaying) return;
        
        if (!_dialogLineFinished)
        {
            Debug.Log("IANarratorManager: NextLineDialog called but line is not finished. Ignoring.");
            return;
        }

        Debug.Log($"IANarratorManager: Advancing to next line. Current index: {_currentLineIndex}");
        _currentLineIndex++;
        if (_currentLineIndex >= _currentDialogue.lines.Count) //si ha acabado el dialogo
        {
            Debug.Log("IANarratorManager: Dialogue finished. Closing UI.");
            _dialogueGO.SetActive(false); 
            _dialogDisplaying = false;
            RemoveOnListDialogue(_currentDialogue);
            NextDialogue();
        }
        else
        {
            _dialogLineFinished = false; 
            if (_narrationCoroutine != null) StopCoroutine(_narrationCoroutine);
            _narrationCoroutine = StartCoroutine(_iATextNarrator.NarrateLine(_currentDialogue.lines[_currentLineIndex]));
        }
    }

    public void DialogueLineFinished()
    {
        _dialogLineFinished = true;
        _timer = 0;
    } 

    private void NextDialogue()
    {
        if (_dialoguesList.Count > 0)
        {
            StartNewDialog(_dialoguesList[0]);
        }
    }

}
