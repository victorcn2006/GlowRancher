using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{

    public static Inventory instance { get; private set; }

    public List<SlotInventario> slots = new List<SlotInventario>();
    private const int MAX_SLOTS = 4;
    private const int MAX_QUANTITY_SLOT = 20;

    public SlotUI[] slotUI;

    private int slotSelected = 0;

    private void Awake() {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    private void Start() {
        InitializeSlots();
        SubscribeToInputEvents();
    }
    private void OnDestroy() {
        UnsubscribeFromInputEvents();
    }
    #region Initialization
    private void InitializeSlots() {
        slots.Clear();
        for (int i = 0; i < MAX_SLOTS; i++)
        {
            slots.Add(null);
            slotUI[i].ActualizarSlot(null);
        }
        UpdateVisualSlot();
    }
    #endregion

    #region Event Subscription
    private void SubscribeToInputEvents() {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnInventoryScroll += HandleScroll;
            InputManager.Instance.OnSlotSelected += HandleSlotSelection;
        }
        else
        {
            Debug.LogError("Inventory: InputManager instance not found!");
        }
    }

    private void UnsubscribeFromInputEvents() {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnInventoryScroll -= HandleScroll;
            InputManager.Instance.OnSlotSelected -= HandleSlotSelection;
        }
    }
    #endregion

    #region Input Handlers
    private void HandleScroll(float scrollValue) {
        if (scrollValue > 0)
            slotSelected = (slotSelected + 1) % MAX_SLOTS;
        else if (scrollValue < 0)
            slotSelected = (slotSelected - 1 + MAX_SLOTS) % MAX_SLOTS;

        UpdateVisualSlot();
    }
    #endregion

    #region Visual Update
    private void UpdateVisualSlot() {
        for (int i = 0; i < slotUI.Length; i++)
            slotUI[i].SetSeleccionado(i == slotSelected);
    }
    #endregion

    #region Inventory Management
    public string RemoteItem() {
        string objectName = "NoName";

        var slot = slots[slotSelected];
        if (slot == null) return null;

        if (slot.cantidad > 0)
        {
            slot.cantidad--;
            objectName = slot.nombre;
            slotUI[slotSelected].ActualizarSlot(slot);

            if (slot.cantidad <= 0)
            {
                ThrowObject(slotSelected);
            }
        }

        return objectName;
    }

    private void ThrowObject(int indice) {
        var slot = slots[indice];
        slots[indice] = null;
        slotUI[indice].ActualizarSlot(null);
    }

    public bool AddToInventory(Sprite icono, string nombre) {
        // Try to add to existing slot
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] != null && slots[i].nombre == nombre)
            {
                if (slots[i].cantidad < MAX_QUANTITY_SLOT)
                {
                    slots[i].cantidad++;
                    slotUI[i].ActualizarSlot(slots[i]);
                    return true;
                }
                else
                    return false;
            }
        }

        // Add to empty slot
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
    #endregion

    #region Input Handlers

    // ADD this missing method too:
    private void HandleSlotSelection(int slotIndex) {
        if (slotIndex >= 0 && slotIndex < MAX_SLOTS)
        {
            slotSelected = slotIndex;
            UpdateVisualSlot();
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
    public GameObject slotObject;

    public SlotInventario(Sprite icono, string nombre, int cantidad)
    {
        this.icono = icono;
        this.nombre = nombre;
        this.cantidad = cantidad;
    }
}
