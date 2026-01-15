using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SlotUI : MonoBehaviour
{
    public Image icono;       // Imagen que muestra el icono del objeto
    public TextMeshProUGUI cantidadTexto;

    [Header("Colores de selección")]
    [SerializeField] private Image _fondoSlot; //Imagen de fondo del slot (así no tocas el icono del objeto)
    [SerializeField] private Color _colorNormal = Color.white;
    [SerializeField] private Color _colorSeleccionado = Color.yellow;

    public void ActualizarSlot(SlotInventario slot)
    {
        var slotActual = slot;

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

    //Método que cambia visualmente el color del slot seleccionado
    public void SetSeleccionado(bool seleccionado)
    {
        if (_fondoSlot != null)
            _fondoSlot.color = seleccionado ? _colorSeleccionado : _colorNormal;
    }
}
