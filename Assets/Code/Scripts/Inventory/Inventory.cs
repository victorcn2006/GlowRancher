using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<SlotInventario> slots = new List<SlotInventario>();
    private const int MAX_SLOT = 4;
    private const int MAX_CANTIDAD_POR_SLOT = 20;

    public SlotUI[] slotUI;
    private int _slotSeleccionado = 0;

    // Esta variable es clave para conectar con el SiloLogic
    [HideInInspector] public SiloLogic siloAbierto = null;

    void OnEnable()
    {
        InputManager.Instance.OnScroll.AddListener(OnScroll);
        InputManager.Instance.OnInventorySlotKey.AddListener(OnSlotKey);
        InputManager.Instance.OnInventoryRightClick.AddListener(OnRightClick);
    }

    void OnDisable()
    {
        InputManager.Instance.OnScroll.RemoveListener(OnScroll);
        InputManager.Instance.OnInventorySlotKey.RemoveListener(OnSlotKey);
        InputManager.Instance.OnInventoryRightClick.RemoveListener(OnRightClick);
    }

    void Start()
    {
        slots.Clear();
        for (int i = 0; i < MAX_SLOT; i++)
        {
            slots.Add(null);
            slotUI[i].ActualizarSlot(null);
        }
        ActualizarSeleccionVisual();
    }

    private void OnScroll(float scroll)
    {
        scroll *= -1;
        if (scroll > 0) _slotSeleccionado = (_slotSeleccionado + 1) % MAX_SLOT;
        else if (scroll < 0) _slotSeleccionado = (_slotSeleccionado - 1 + MAX_SLOT) % MAX_SLOT;
        ActualizarSeleccionVisual();
    }

    private void OnSlotKey(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= MAX_SLOT) return;
        _slotSeleccionado = slotIndex;
        ActualizarSeleccionVisual();
    }

    private void OnRightClick()
    {
        // Si hay un silo abierto, intentamos transferir el objeto seleccionado
        if (siloAbierto != null && slots[_slotSeleccionado] != null)
        {
            SlotInventario item = slots[_slotSeleccionado];
            bool pudoGuardar = siloAbierto.AñadirAlSilo(item.icono, item.nombre);

            if (pudoGuardar)
            {
                QuitarUno();
            }
        }
    }

    private void ActualizarSeleccionVisual()
    {
        for (int i = 0; i < slotUI.Length; i++)
            slotUI[i].SetSeleccionado(i == _slotSeleccionado);
    }

    public string QuitarUno()
    {
        if (slots[_slotSeleccionado] == null) return null;

        // Guardamos el nombre antes de borrar el slot
        string nombreObjeto = slots[_slotSeleccionado].nombre;

        slots[_slotSeleccionado].cantidad--;

        if (slots[_slotSeleccionado].cantidad <= 0)
        {
            slots[_slotSeleccionado] = null;
        }

        slotUI[_slotSeleccionado].ActualizarSlot(slots[_slotSeleccionado]);

        return nombreObjeto; // Este nombre es el que usa la aspiradora para disparar
    }

    public bool AñadirAlInventario(Sprite icono, string nombre)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Debug.Log(nombre);
            if (slots[i] != null && slots[i].nombre == nombre && slots[i].cantidad < MAX_CANTIDAD_POR_SLOT)
            {
                slots[i].cantidad++;
                slotUI[i].ActualizarSlot(slots[i]);
                return true;
            }
        }

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = new SlotInventario(icono, nombre, 1);
                slotUI[i].ActualizarSlot(slots[i]);
                return true;
            }
        }
        return false;
    }
}
