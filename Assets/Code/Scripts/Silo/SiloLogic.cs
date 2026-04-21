using UnityEngine;
using System.Collections.Generic;

public class SiloLogic : MonoBehaviour
{
    [Header("Configuración")]
    public List<SlotInventario> slotsSilo = new List<SlotInventario>();
    [SerializeField] private Inventory _playerInventory;

    private SlotUI[] slotUISilo;
    private const int MAX_CANTIDAD_SILO = 99;


    private void Start()
    {
        if (_playerInventory == null)
            _playerInventory = FindObjectOfType<Inventory>();
        // El panel es únic a l'escena, el busquem per tag
        GameObject panel = GameObject.FindWithTag("SiloPanel");
        if (panel != null)
        {
            slotUISilo = panel.GetComponentsInChildren<SlotUI>(true);
            slotsSilo.Clear();
            for (int i = 0; i < slotUISilo.Length; i++)
                slotsSilo.Add(null);
        }
        else
        {
            Debug.LogError("[SiloLogic] No s'ha trobat cap GameObject amb tag 'SiloPanel'!");
        }
    }

    public bool AñadirAlSilo(Sprite icono, string nombre)
    {
        // 1. Intentar apilar
        for (int i = 0; i < slotsSilo.Count; i++)
        {
            if (slotsSilo[i] != null && slotsSilo[i].nombre == nombre && slotsSilo[i].cantidad < MAX_CANTIDAD_SILO)
            {
                slotsSilo[i].cantidad++;
                slotUISilo[i].ActualizarSlot(slotsSilo[i]);
                return true;
            }
        }
        // 2. Buscar hueco vacío
        for (int i = 0; i < slotsSilo.Count; i++)
        {
            if (slotsSilo[i] == null)
            {
                slotsSilo[i] = new SlotInventario(icono, nombre, 1);
                slotUISilo[i].ActualizarSlot(slotsSilo[i]);
                return true;
            }
        }
        return false;
    }

    public void ExtraerDelSilo(int indice)
    {
        if (indice < 0 || indice >= slotsSilo.Count || slotsSilo[indice] == null) return;

        SlotInventario item = slotsSilo[indice];
        Debug.Log($"<color=cyan>[SILO]</color> Extrayendo Item: <b>{item.nombre}</b> | Cantidad: {item.cantidad}");

        bool exito = _playerInventory.AñadirAlInventario(slotsSilo[indice].icono, slotsSilo[indice].nombre);
        if (exito)
        {
            slotsSilo[indice].cantidad--;
            if (slotsSilo[indice].cantidad <= 0) slotsSilo[indice] = null;
            slotUISilo[indice].ActualizarSlot(slotsSilo[indice]);
        }
    }

    public void RefrescarUI()
    {
        for (int i = 0; i < slotUISilo.Length; i++)
            slotUISilo[i].ActualizarSlot(slotsSilo[i]);
    }
}
