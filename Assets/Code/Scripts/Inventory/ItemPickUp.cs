using UnityEngine;

public class ItemPickUp : MonoBehaviour, IAspirable
{
    public Sprite icono;   // Icono del objeto
    public string nombre;  // Nombre del objeto (debe ser único para que no se confundan)

    public void BeingAspired()
    {
        Debug.Log("Que me chucla");
    }

    public void StopBeingAspired()
    {
        throw new System.NotImplementedException();
    }
    /*
private void OnTriggerEnter(Collider other)
{
   if (other.CompareTag("Player"))
   {
       Inventory inventario = other.GetComponent<Inventory>();
       if (inventario != null)
       {
           // Añadimos el objeto al inventario
           if (inventario.AñadirAlInventario(icono, nombre))
           {
               Destroy(gameObject); // Destruir el objeto en el mundo
           }
       }
   }
}*/
}