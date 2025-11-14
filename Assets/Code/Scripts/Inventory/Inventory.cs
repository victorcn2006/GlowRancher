using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public List<SlotInventario> slots = new List<SlotInventario>();
    private const int maxSlots = 4;
    private const int maxCantidadPorSlot = 20;

    public SlotUI[] slotUI;

    private int slotSeleccionado = 0;

    [Header("Controles del inventario")]
    [SerializeField] private InputAction scrollAction;      // Rueda del ratón
    [SerializeField] private InputAction rightClickAction;  // Clic derecho
    [SerializeField] private InputAction slotKeysAction;    // Teclas 1,2,3,4

    void OnEnable()
    {
        scrollAction.Enable();
        rightClickAction.Enable();
        slotKeysAction.Enable();

        scrollAction.performed += OnScroll;
        rightClickAction.performed += OnRightClick;
        slotKeysAction.performed += OnNumberKey;
    }

    void OnDisable()
    {
        scrollAction.performed -= OnScroll;
        rightClickAction.performed -= OnRightClick;
        slotKeysAction.performed -= OnNumberKey;

        scrollAction.Disable();
        rightClickAction.Disable();
        slotKeysAction.Disable();
    }

    void Start()
    {
        slots.Clear();
        for (int i = 0; i < maxSlots; i++)
        {
            slots.Add(null);
            slotUI[i].ActualizarSlot(null);
        }
        ActualizarSeleccionVisual();
    }

    // -----------------------------------------
    // Selección con teclas 1,2,3,4
    // -----------------------------------------
    private void OnNumberKey(InputAction.CallbackContext ctx)
    {
        string key = ctx.control.name;

        switch (key)
        {
            case "1":
                slotSeleccionado = 0;
                break;
            case "2":
                slotSeleccionado = 1;
                break;
            case "3":
                slotSeleccionado = 2;
                break;
            case "4":
                slotSeleccionado = 3;
                break;
            default:
                return;
        }

        ActualizarSeleccionVisual();
    }

    // Movimiento de rueda del ratón
    private void OnScroll(InputAction.CallbackContext ctx)
    {
        float scroll = ctx.ReadValue<Vector2>().y;

        if (scroll > 0)
            slotSeleccionado = (slotSeleccionado + 1) % maxSlots;
        else if (scroll < 0)
            slotSeleccionado = (slotSeleccionado - 1 + maxSlots) % maxSlots;

        ActualizarSeleccionVisual();
    }

    // Clic derecho → quitar 1 unidad
    private void OnRightClick(InputAction.CallbackContext ctx)
    {
        QuitarUno(slotSeleccionado);
    }

    private void ActualizarSeleccionVisual()
    {
        for (int i = 0; i < slotUI.Length; i++)
            slotUI[i].SetSeleccionado(i == slotSeleccionado);
    }

    // Quitar una unidad del objeto seleccionado
    private void QuitarUno(int indice)
    {
        var slot = slots[indice];
        if (slot == null) return;

        slot.cantidad--; // Quita 1

        Debug.Log($"Quitando 1 de {slot.nombre}, quedan {slot.cantidad}");

        if (slot.cantidad <= 0)
        {
            ExpulsarObjeto(indice);
        }
        else
        {
            slotUI[indice].ActualizarSlot(slot);
        }
    }

    // Expulsar objeto si se queda en 0
    private void ExpulsarObjeto(int indice)
    {
        var slot = slots[indice];
        if (slot == null) return;

        Debug.Log($"Expulsando objeto {slot.nombre} del slot {indice}");

        GameObject prefab = Resources.Load<GameObject>($"Objetos/{slot.nombre}");
        if (prefab != null)
        {
            Instantiate(prefab, transform.position + transform.forward, Quaternion.identity);
        }

        slots[indice] = null;
        slotUI[indice].ActualizarSlot(null);
    }

    // Añadir objetos
    public bool AñadirAlInventario(Sprite icono, string nombre)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] != null && slots[i].nombre == nombre)
            {
                if (slots[i].cantidad < maxCantidadPorSlot)
                {
                    slots[i].cantidad++;
                    slotUI[i].ActualizarSlot(slots[i]);
                    return true;
                }
                else
                    return false;
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

    public bool SacarDelInventario(string nombre)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] != null && slots[i].nombre == nombre)
            {
                slots[i].cantidad--;
                if (slots[i].cantidad <= 0)
                    slots[i] = null;

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

    public SlotInventario(Sprite icono, string nombre, int cantidad)
    {
        this.icono = icono;
        this.nombre = nombre;
        this.cantidad = cantidad;
    }
}
