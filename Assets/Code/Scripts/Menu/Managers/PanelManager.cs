using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField] GameObject _panel;
    [SerializeField] GameObject _panel2;

    public void ActivePanel1() {
        _panel2.SetActive(false);
        _panel.SetActive(true);
    }

    public void ActivePanel2() {
        _panel.SetActive(false);
        _panel2.SetActive(true);
    }  
}
