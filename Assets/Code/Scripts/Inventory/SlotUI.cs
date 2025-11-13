using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SlotUI : MonoBehaviour
{
    public Image icono;       // Imagen que muestra el icono del objeto
    public TextMeshProUGUI cantidadTexto;
    private SlotInventario slotActual;

    [Header("Colores de selección")]
    [SerializeField] private Image fondoSlot; //Imagen de fondo del slot (así no tocas el icono del objeto)
    [SerializeField] private Color colorNormal = Color.white;
    [SerializeField] private Color colorSeleccionado = Color.yellow;

    public void ActualizarSlot(SlotInventario slot)
    {
        slotActual = slot;

        if (slot == null)
        {
            icono.enabled = false;
            cantidadTexto.text = "";
        }
        else
        {
            icono.enabled = true;
            icono.sprite = slot.icono;
            cantidadTexto.text = slot.cantidad.ToString();
        }
    }

    // 🔹 Método que cambia visualmente el color del slot seleccionado
    public void SetSeleccionado(bool seleccionado)
    {
        if (fondoSlot != null)
            fondoSlot.color = seleccionado ? colorSeleccionado : colorNormal;
    }
}