using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InteractiveShop _shop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     cuan comprem el incinerador es desactivara el panell i es activara el holograma del incinerador.
     */
    public void IncineratorBuyed()
    {
        _shop.HandleKeyboardToggle();

        /*
         Aqui anira el holograma quan estigui fet, pero per ara es desactivara el panell i es activara el holograma del incinerador.
         */
    }
}
