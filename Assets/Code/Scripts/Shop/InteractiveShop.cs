using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractiveShop : MonoBehaviour
{
    [Header("Refs")]
    public GameObject ShopPanel;


    private bool isShopActive = false; // Variable per saber si la botiga està activa

    private void Update()
    {
        // Potser voldràs posar una altra condició per activar/desactivar la botiga (tecla específica).
    }

    public void ToggleShop()
    {
        if (isShopActive)
        {
            DesActivateObject();
            Cursor.visible = false;
        }
        else
        {
            ActivateObject();
            Cursor.visible = true;
        }
    }

    public void ActivateObject()
    {
        Debug.Log("Shop activation");
        ShopPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None; // Mostra el cursor
        Time.timeScale = 0;

        isShopActive = true; // Marquem que la botiga està activa
    }

    public void DesActivateObject()
    {
        Debug.Log("Shop desactivation");
        ShopPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked; // Oculta el cursor
        Time.timeScale = 1;

        isShopActive = false; // Marquem que la botiga està desactivada
    }
}
