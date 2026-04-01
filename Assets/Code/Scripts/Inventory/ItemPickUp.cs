using UnityEngine;

public class ItemPickUp : MonoBehaviour, IAspirable
{
    public Sprite icono;   // Icono del objeto
    public string nombre;  // Nombre del objeto (debe ser único para que no se confundan)

    public void BeingAspired()
    {

    }

    public void StopBeingAspired()
    {

    }
}
