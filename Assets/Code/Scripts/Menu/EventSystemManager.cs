using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class EventSystemManager : MonoBehaviour {
    //GameObject to focus at start
    [SerializeField] private GameObject initialFocusObject;
    //Here I saved the click action from Controls
    private InputAction clickAction;
    //Here I'm going to override the focus from the EventSystem
    [HideInInspector] public GameObject lastSelectedObject;

    private InputSystemUIInputModule uIInputModule;

    private void Awake() {
        uIInputModule = GetComponent<InputSystemUIInputModule>();
    }
    private void Start() {
        //If the focus is not null the EventSystem is gonna focus the first gameObject
        if (initialFocusObject != null)
        {
            EventSystem.current.SetSelectedGameObject(initialFocusObject);
            lastSelectedObject = initialFocusObject;
        }
        else
        {
            Debug.Log("Initial focus object not assigned in the Inspector");
            return;
        }
        //If we can access to uiInputModule that has the inputs of the UI the code is going to execute
        if (uIInputModule != null)
        {
            clickAction = uIInputModule.actionsAsset.FindAction("UI/Click");
            //If the action is not null and exists I subscribe the action when you click
            if (clickAction != null)
            {
                clickAction.performed += OnClick;
            }
            else
            {
                Debug.LogWarning("Action 'UI/Click' not found.", this);
            }
        }
    }

    private void Update() {
        //Here it overrides the focus everytime you move
        if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject != lastSelectedObject)
        {
            lastSelectedObject = EventSystem.current.currentSelectedGameObject;
        }
    }

    private void OnClick(InputAction.CallbackContext context) {
        //This piece of code helps to not lose the focus if we click in other area.
        if (EventSystem.current.currentSelectedGameObject == null && lastSelectedObject != null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelectedObject);
        }
    }
    //It unsubscribe the action when we finished
    private void OnDestroy() {
        if (clickAction != null)
        {
            clickAction.performed -= OnClick;
        }
    }
}
