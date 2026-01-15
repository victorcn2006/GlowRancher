using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LayoutManager : MonoBehaviour
{
    public List<GameObject> panels;
    public List<GameObject> buttons;
    [SerializeField] private Transform _buttonsParent;
    public enum MENU_PANEL { GAME_OPTIONS, AUDIO, GRAPHICS, CONTROL, LENGTH };
    Dictionary<MENU_PANEL, GameObject> _panelDictionary;


    private void Start()
    {
        _panelDictionary = new Dictionary<MENU_PANEL, GameObject>();
        for (int i = 0; i < (int)MENU_PANEL.LENGTH; i++)
        {
            _panelDictionary.Add((MENU_PANEL)i, panels[i]);
        }

        for (int i = 0; i < _buttonsParent.transform.childCount; i++)
        {
            buttons.Add(_buttonsParent.transform.GetChild(i).gameObject);
        }

        UnityEvents.Instance.OnSelectionChanged.AddListener(ChangeLayout);

        //Activar panel inicial: GAME_OPTIONS
        foreach (var pair in _panelDictionary) {
            pair.Value.SetActive(false);
        }

        _panelDictionary[MENU_PANEL.GAME_OPTIONS].SetActive(true);
    }
    private void OnDisable()
    {
        UnityEvents.Instance?.OnSelectionChanged.RemoveListener(ChangeLayout);
    }
    private void ChangeLayout()
    {
        GameObject activePanel = null;
        for (int i = 0; i < buttons.Count && !activePanel; i++)
        {
            if (EventSystem.current.currentSelectedGameObject == buttons[i])
            {
                activePanel = _panelDictionary[(MENU_PANEL)i];
            }
        }
        if (activePanel != null)
        {
            foreach (KeyValuePair<MENU_PANEL, GameObject> pair in _panelDictionary)
            {
                if (pair.Value != activePanel)
                {
                    pair.Value.SetActive(false);
                }
                else
                {
                    pair.Value.SetActive(true);
                }
            }
        }
    }

    public GameObject GetPanel(MENU_PANEL panel)
    {
        return _panelDictionary[panel];
    }

}
