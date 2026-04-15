using UnityEngine;
using System.Collections.Generic;

public class SiloLogic : MonoBehaviour
{
    [Header("Configuración")]
    public List<SlotInventario> slotsSilo = new List<SlotInventario>();
    public SlotUI[] slotUISilo; // Arrastra aquí los slots de la UI del Silo
    public Inventory playerInventory;
    private const int MAX_CANTIDAD_SILO = 99;

    void Awake()
    {
        // Inicializar la lista según la cantidad de slots visuales que tengas
        for (int i = 0; i < slotUISilo.Length; i++)
        {
            slotsSilo.Add(null);
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

    // Se llama al hacer clic en un botón de la UI del Silo
    public void ExtraerDelSilo(int indice)
    {
        if (indice < 0 || indice >= slotsSilo.Count || slotsSilo[indice] == null) return;

        // IMPORTANTE: Pasamos el icono y el nombre EXACTOS que tiene el silo
        bool exito = playerInventory.AñadirAlInventario(slotsSilo[indice].icono, slotsSilo[indice].nombre);

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
        {
            slotUISilo[i].ActualizarSlot(slotsSilo[i]);
        }
    }
}
