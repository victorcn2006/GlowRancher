using UnityEngine;

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
