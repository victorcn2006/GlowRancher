using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    public Image icono;
    public TextMeshProUGUI cantidadTexto;

    [Header("Colores de selección")]
    [SerializeField] private Image _fondoSlot;
    [SerializeField] private Color _colorNormal = Color.white;
    [SerializeField] private Color _colorSeleccionado = Color.yellow;

    public void ActualizarSlot(SlotInventario slot)
    {
        if (slot == null || slot.cantidad <= 0)
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

    public void SetSeleccionado(bool seleccionado)
    {
        if (_fondoSlot != null)
            _fondoSlot.color = seleccionado ? _colorSeleccionado : _colorNormal;
    }
}
