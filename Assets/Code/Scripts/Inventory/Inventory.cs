using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class Inventory : MonoBehaviour
{

    [Header("Configuración")]
    [SerializeField] private int _maxSlots = 4;
    [SerializeField] private int _maxCantidadPorSlot = 20;

    [Header("Referencias UI")]
    [SerializeField] private SlotUI[] _slotUI;

    private List<SlotInventario> _slots;
    private int _slotSeleccionado = 0;

    public SlotInventario SelectedSlot => (_slotSeleccionado < _slots.Count) ? _slots[_slotSeleccionado] : null;

    private void Awake()
    {
        InicializarInventario();
    }

    private void OnEnable()
    {
        if (InputManager.Instance == null) return;
        InputManager.Instance.OnInventoryScroll.AddListener(HandleScroll);
        InputManager.Instance.OnInventorySlotKey.AddListener(HandleSlotKey);
    }

    void OnDisable()
    {
        if (InputManager.Instance == null) return;
        InputManager.Instance.OnInventoryScroll.RemoveListener(HandleScroll);
        InputManager.Instance.OnInventorySlotKey.RemoveListener(HandleSlotKey);
    }

    private void InicializarInventario()
    {
        _slots = new List<SlotInventario>(new SlotInventario[_maxSlots]);

        for (int i = 0; i < _maxSlots; i++)
        {
            ActualizarSlotEnUI(i);
        }
        ActualizarSeleccionVisual();
    }

    #region Input Handling


    private void HandleScroll(float scroll)
    {
        if (Mathf.Abs(scroll) < 0.1f) return;

        int direccion = scroll > 0 ? 1 : -1;
        _slotSeleccionado = (_slotSeleccionado + direccion + _maxSlots) % _maxSlots;
        ActualizarSeleccionVisual();
    }

    private void HandleSlotKey(int index)
    {
        if (index < 0 || index >= _maxSlots) return;
        _slotSeleccionado = index;
        ActualizarSeleccionVisual();
    }
    #endregion
    #region Lógica del Item
    public bool AñadirAlInventario(Sprite icono, string nombre)
    {
        // Buscar si ya existe el objeto en algún slot
        for (int i = 0; i < _maxSlots; i++)
        {
            if (_slots[i] != null && _slots[i].nombre == nombre)
            {
                if (_slots[i].cantidad < _maxCantidadPorSlot)
                {
                    _slots[i].cantidad++;
                    _slotUI[i].ActualizarSlot(_slots[i]);
                    return true;
                }
                else
                    return false;
            }
        }

        // Buscar un slot vacío
        for (int i = 0; i < _maxSlots; i++)
        {
            if (_slots[i] == null)
            {
                _slots[i] = new SlotInventario(icono, nombre, 1);
                ActualizarSlotEnUI(i);
                return true;
            }
        }

        return false;
    }
    
    public string UsarItemSeleccionado()
    {
        SlotInventario slot = _slots[_slotSeleccionado];
        if (slot == null) return null;

        string nombreItem = slot.nombre;
        slot.cantidad--;

        if (slot.cantidad <= 0)
        {
            VaciarSlot(_slotSeleccionado);
        }
        else
        {
            ActualizarSlotEnUI(_slotSeleccionado);
        }
        return nombreItem;
    }


    public void VaciarSlot(int index)
    {
        _slots[index] = null;
        ActualizarSlotEnUI(index);
    }
    #endregion
    #region Actualización de la UI

    private void ActualizarSlotEnUI(int index)
    {
        if (index < _slotUI.Length)
        {
            _slotUI[index].ActualizarSlot(_slots[index]);
        }
    }

    private void ActualizarSeleccionVisual()
    {
        for (int i = 0; i < _slotUI.Length; i++)
        {
            _slotUI[i].SetSeleccionado(i == _slotSeleccionado);
        }
    }

    #endregion

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

    public bool EsMismoItem(string nombreItem) => nombre == nombreItem;
}
