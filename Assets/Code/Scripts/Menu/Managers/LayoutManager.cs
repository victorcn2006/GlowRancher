using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LayoutManager : MonoBehaviour
{
    public List<GameObject> panels;
    public List<GameObject> buttons;
    [SerializeField] Transform buttonsParent;
    public enum MENU_PANEL { GAME_OPTIONS, AUDIO, GRAPHICS, CONTROL, LENGTH };
    Dictionary<MENU_PANEL, GameObject> panelDictionary;


    private void Start()
    {
        panelDictionary = new Dictionary<MENU_PANEL, GameObject>();
        for (int i = 0; i < (int)MENU_PANEL.LENGTH; i++)
        {
            panelDictionary.Add((MENU_PANEL)i, panels[i]);
        }

        for (int i = 0; i < buttonsParent.transform.childCount; i++)
        {
            buttons.Add(buttonsParent.transform.GetChild(i).gameObject);
        }

        UnityEvents.Instance.OnSelectionChanged.AddListener(ChangeLayout);
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
                activePanel = panelDictionary[(MENU_PANEL)i];
            }
        }
        if (activePanel != null)
        {
            foreach (KeyValuePair<MENU_PANEL, GameObject> pair in panelDictionary)
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
        return panelDictionary[panel];
    }

}
