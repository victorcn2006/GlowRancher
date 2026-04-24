using UnityEngine;
using System.Collections.Generic;

public class SiloLogic : MonoBehaviour
{
    [Header("Configuración")]
    // Usamos una lista fija de 24 para que coincida con la UI
    public List<SlotInventario> slotsSilo = new List<SlotInventario>();
    [SerializeField] private Inventory _playerInventory;

    private SlotUI[] slotUISilo;
    private const int MAX_CANTIDAD_POR_SLOT = 99;
    private const int TOTAL_SLOTS = 24;

    private void Awake()
    {
        // Garantizamos que la lista siempre tenga 24 posiciones al iniciar
        if (slotsSilo == null || slotsSilo.Count == 0)
        {
            slotsSilo = new List<SlotInventario>();
            for (int i = 0; i < TOTAL_SLOTS; i++)
            {
                slotsSilo.Add(null);
            }
        }
    }

    private void Start()
    {
        if (_playerInventory == null) _playerInventory = FindObjectOfType<Inventory>();
    }

    public void VincularUI(GameObject panel)
    {
        if (panel != null)
        {
            // Importante: GetComponentsInChildren debe encontrar exactamente 24 scripts SlotUI
            slotUISilo = panel.GetComponentsInChildren<SlotUI>(true);
        }
    }

    public bool AñadirAlSilo(Sprite icono, string nombre)
    {
        // PASO 1: Buscar si ya existe el ítem para apilarlo
        for (int i = 0; i < slotsSilo.Count; i++)
        {
            if (slotsSilo[i] != null && slotsSilo[i].nombre == nombre)
            {
                if (slotsSilo[i].cantidad < MAX_CANTIDAD_POR_SLOT)
                {
                    slotsSilo[i].cantidad++;
                    RefrescarUI();
                    return true;
                }
            }
        }

        // PASO 2: Si no se pudo apilar, buscar el PRIMER hueco vacío disponible
        for (int i = 0; i < slotsSilo.Count; i++)
        {
            if (slotsSilo[i] == null || string.IsNullOrEmpty(slotsSilo[i].nombre))
            {
                slotsSilo[i] = new SlotInventario(icono, nombre, 1);
                RefrescarUI();
                return true;
            }
        }

        Debug.LogWarning("El silo está completamente lleno.");
        return false;
    }

    public void ExtraerDelSilo(int indice)
    {
        if (indice < 0 || indice >= slotsSilo.Count || slotsSilo[indice] == null) return;

        // Intentamos mover al inventario del jugador
        bool pudoAñadir = _playerInventory.AñadirAlInventario(slotsSilo[indice].icono, slotsSilo[indice].nombre);

        if (pudoAñadir)
        {
            slotsSilo[indice].cantidad--;

            // Si llegamos a cero, el slot vuelve a ser nulo (vacío)
            if (slotsSilo[indice].cantidad <= 0)
            {
                slotsSilo[indice] = null;
            }

            RefrescarUI();
        }
    }

    public void RefrescarUI()
    {
        if (slotUISilo == null || slotUISilo.Length == 0) return;

        for (int i = 0; i < slotUISilo.Length; i++)
        {
            if (i < slotsSilo.Count)
            {
                // Actualizamos el slot de la UI con el dato (null o con objeto)
                slotUISilo[i].ActualizarSlot(slotsSilo[i]);
            }
        }
    }
}
