using UnityEngine;

public class ItemPickUp : MonoBehaviour, IAspirable
{
    public Sprite icono;   // Icono del objeto
    public string nombre;  // Nombre del objeto (debe ser único para que no se confundan)

    public void BeingAspired()
    {
        throw new System.NotImplementedException();
    }

    public void StopBeingAspired()
    {
        throw new System.NotImplementedException();
    }
}