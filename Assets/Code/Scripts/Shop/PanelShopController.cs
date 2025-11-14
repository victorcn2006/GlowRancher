using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelShopController : MonoBehaviour
{
    public GameObject itemOneLabel;

    // Start is called before the first frame update
    void Start()
    {
        itemOneLabel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateItemOne() {
        itemOneLabel.SetActive(true);
    }
}
