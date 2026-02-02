using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public List<SlotInventario> slots = new List<SlotInventario>();
    private const int MAX_SLOT = 4;
    private const int MAX_CANTIDAD_POR_SLOT = 20;

    public SlotUI[] slotUI;

    private int _slotSeleccionado = 0;


    void OnEnable()
    {
        InputManager.Instance.OnInventoryScroll.AddListener(OnScroll);
        InputManager.Instance.OnInventorySlotKey.AddListener(OnSlotKey);
        InputManager.Instance.OnInventoryRightClick.AddListener(OnRightClick);
    }

    void OnDisable()
    {
        InputManager.Instance.OnInventoryScroll.RemoveListener(OnScroll);
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

    // Movimiento de rueda del ratón
    private void OnScroll(float scroll)
    {
        if (scroll > 0)
            _slotSeleccionado = (_slotSeleccionado + 1) % MAX_SLOT;
        else if (scroll < 0)
            _slotSeleccionado = (_slotSeleccionado - 1 + MAX_SLOT) % MAX_SLOT;

        ActualizarSeleccionVisual();
    }

    // Selección con teclas 1,2,3,4
    private void OnSlotKey(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= MAX_SLOT) return;

        _slotSeleccionado = slotIndex;
        ActualizarSeleccionVisual();
    }

    private void OnRightClick()
    {
        QuitarUno();
    }

    private void ActualizarSeleccionVisual()
    {
        for (int i = 0; i < slotUI.Length; i++)
            slotUI[i].SetSeleccionado(i == _slotSeleccionado);
    }

    public string QuitarUno()
    {
        string objectName = "NoName";

        var slot = slots[_slotSeleccionado];
        if (slot == null) return null;

        if (slot.cantidad > 0)
        {
            slot.cantidad--; // Quita 1
            objectName = slot.nombre;
            slotUI[_slotSeleccionado].ActualizarSlot(slot);
            if (slot.cantidad <= 0)
            {
                ExpulsarObjeto(_slotSeleccionado);
            }
        }

        return objectName;
    }

    private void ExpulsarObjeto(int indice)
    {
        var slot = slots[indice];

        slots[indice] = null;
        slotUI[indice].ActualizarSlot(null);
    }

    // Añadir objetos
    public bool AñadirAlInventario(Sprite icono, string nombre)
    {
        // Buscar si ya existe el objeto en algún slot
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] != null && slots[i].nombre == nombre)
            {
                if (slots[i].cantidad < MAX_CANTIDAD_POR_SLOT)
                {
                    slots[i].cantidad++;
                    slotUI[i].ActualizarSlot(slots[i]);
                    return true;
                }
                else
                    return false;
            }
        }

        // Buscar un slot vacío
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

[System.Serializable]
public class SlotInventario
{
    public Sprite icono;
    public string nombre;
    public int cantidad;
    public GameObject slotObject;

    public SlotInventario(Sprite icono, string nombre, int cantidad)
    {
        this.icono = icono;
        this.nombre = nombre;
        this.cantidad = cantidad;
    }
}
