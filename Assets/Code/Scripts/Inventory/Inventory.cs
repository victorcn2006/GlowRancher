using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public List<SlotInventario> slots = new List<SlotInventario>();
    private const int MAX_SLOTS = 4;
    private const int MAX_CANTIDAD_POR_SLOT = 20;

    public SlotUI[] slotUI;

    private int _slotSeleccionado = 0;

    [Header("Controles del inventario")]
    [SerializeField] private InputAction _scrollAction;      // Rueda del ratón
    [SerializeField] private InputAction _rightClickAction;  // Clic derecho
    [SerializeField] private InputAction _slotKeysAction;    // Teclas 1,2,3,4

    void OnEnable()
    {
        _scrollAction.Enable();
        _rightClickAction.Enable();
        _slotKeysAction.Enable();

        _scrollAction.performed += OnScroll;
        //rightClickAction.performed += OnRightClick;
        _slotKeysAction.performed += OnNumberKey;
    }

    void OnDisable()
    {
        _scrollAction.performed -= OnScroll;
        //rightClickAction.performed -= OnRightClick;
        _slotKeysAction.performed -= OnNumberKey;

        _scrollAction.Disable();
        //rightClickAction.Disable();
        _slotKeysAction.Disable();
    }

    void Start()
    {
        slots.Clear();
        for (int i = 0; i < MAX_SLOTS; i++)
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
                _slotSeleccionado = 0;
                break;
            case "2":
                _slotSeleccionado = 1;
                break;
            case "3":
                _slotSeleccionado = 2;
                break;
            case "4":
                _slotSeleccionado = 3;
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
            _slotSeleccionado = (_slotSeleccionado + 1) % MAX_SLOTS;
        else if (scroll < 0)
            _slotSeleccionado = (_slotSeleccionado - 1 + MAX_SLOTS) % MAX_SLOTS;

        ActualizarSeleccionVisual();
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
            slot.cantidad--; //Quita 1
            objectName = slot.nombre;
            slotUI[_slotSeleccionado].ActualizarSlot(slot);
            if (slot.cantidad <= 0)
            {
                ExpulsarObjeto(_slotSeleccionado);
            }
        }

        return objectName;
    }

    // Quitar una unidad del objeto seleccionado
    /*
    public string QuitarUino()
    {
        string objectName = "noName";
        var slot = slots[slotSeleccionado];
        if (slot == null) return null;

        slot.cantidad--; // Quita 1

        Debug.Log($"Quitando 1 de {slot.nombre}, quedan {slot.cantidad}");

        if (slot.cantidad <= 0)
        {
            ExpulsarObjeto(slotSeleccionado);
        }
        else
        {
            slotUI[slotSeleccionado].ActualizarSlot(slot);
        }
        return objectName;
    }*/

    private void ExpulsarObjeto(int indice)
    {
        var slot = slots[indice];

        slots[indice] = null;
        slotUI[indice].ActualizarSlot(null);
    }


    /*
    // Expulsar objeto si se queda en 0
    private string ExpulsarObjeto(int indice)
    {
        var slot = slots[indice];
        if (slot == null) return null;

        Debug.Log($"Expulsando objeto {slot.nombre} del slot {indice}");

        GameObject prefab = Resources.Load<GameObject>($"Objetos/{slot.nombre}");
        if (prefab != null)
        {
            //Instantiate(prefab, transform.position + transform.forward, Quaternion.identity);
        }

        slots[indice] = null;
        slotUI[indice].ActualizarSlot(null);
        return slot.nombre;
    }*/

    // Añadir objetos
    public bool AñadirAlInventario(Sprite icono, string nombre)
    {
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
    /*
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

    */
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
