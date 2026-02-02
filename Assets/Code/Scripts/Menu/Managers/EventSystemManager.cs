using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class EventSystemManager : MonoBehaviour {
    
    [HideInInspector] public GameObject lastSelectedObject;

    private InputAction _clickAction;
    private InputSystemUIInputModule _uIInputModule;

    private void Awake() {
        if(_uIInputModule == null) _uIInputModule = GetComponent<InputSystemUIInputModule>();
    }
    private void Start() {
        GameObject _initialFocusObject = EventSystem.current.firstSelectedGameObject;
        //If the focus is not null the EventSystem is gonna focus the first gameObject
        if (_initialFocusObject != null){
            EventSystem.current.SetSelectedGameObject(_initialFocusObject);
            lastSelectedObject = _initialFocusObject;
        }
        else{
            Debug.LogWarning("Initial focus object not assigned in the Inspector");
            return;
        }

        //uiInputModule has the UI input Click left action
        if (_uIInputModule != null){
            _clickAction = _uIInputModule.actionsAsset.FindAction("UI/Click");

            if (_clickAction != null) _clickAction.performed += OnClick;
            else Debug.LogWarning("Action 'UI/Click' not found.", this);
        }
    }

    private void Update() {
        //Here it overrides the focus everytime you move
        var current = EventSystem.current.currentSelectedGameObject;
        if (current != null && current != lastSelectedObject){
            lastSelectedObject = current;
            UnityEvents.Instance?.OnSelectionChanged.Invoke();
        }
    }
    private void OnDestroy()
    {
        if (_clickAction != null) _clickAction.performed -= OnClick;
    }
    
    private void OnClick(InputAction.CallbackContext context) {
        //This piece of code helps to not lose the focus if we click in other area.
        if (EventSystem.current.currentSelectedGameObject == null && lastSelectedObject != null && lastSelectedObject.activeInHierarchy)
            EventSystem.current.SetSelectedGameObject(lastSelectedObject);
    }
}
