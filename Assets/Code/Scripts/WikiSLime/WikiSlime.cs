using UnityEngine;
using UnityEngine.UI;

public class WikiSlime : MonoBehaviour
{
    // Referencia al GameObject que contiene la UI de la wiki
    public GameObject wikiMenu;
    public GameObject wikiInfo;
    // Referencias a los GameObjects de los slimes
    public GameObject slime1;
    public GameObject slime2;
    // (Añadir más slimes si es necesario)

    // Referencia al GameObject actualmente activo
    private GameObject currentActiveSlime;

    // Referencias a los botones (si usas botones UI)
    public Button slime1Button;
    public Button slime2Button;
    // (Añadir más botones si es necesario)

    // Función para mostrar la UI de la wiki
    public void ActiveWiki()
    {
        Debug.Log("Activando la UI de la wiki.");
        wikiMenu.SetActive(true);
    }

    // Función para ocultar la UI de la wiki
    public void DesactiveWiki()
    {
        Debug.Log("Desactivando la UI de la wiki.");
        wikiMenu.SetActive(false);
    }

    void Start()
    {
        // Asegúrate de que slime1 y slime2 no sean nulos
        if (slime1 == null || slime2 == null)
        {
            Debug.LogError("¡Faltan asignaciones de slimes en el Inspector!");
            return;
        }

        // Inicializa el primer slime activo
        currentActiveSlime = slime1; // Esto lo puedes cambiar si otro slime es el primero por defecto
        Debug.Log("Activando el slime inicial: " + slime1.name);
        currentActiveSlime.SetActive(true); // Asegurarse de que el primer slime está activo al principio

        // Desactivar otros slimes
        if (slime2 != null)
        {
            Debug.Log("Desactivando slime2.");
            slime2.SetActive(false);
        }

        // Asignar los listeners a los botones
        if (slime1Button != null && slime2Button != null)
        {
            slime1Button.onClick.AddListener(ShowSlime1);
            slime2Button.onClick.AddListener(ShowSlime2);
        }
        else
        {
            Debug.LogError("¡Faltan asignaciones de botones en el Inspector!");
        }
    }

    // Función para mostrar el slime 1
    void ShowSlime1()
    {
        Debug.Log("Mostrar slime 1");
        SwitchSlime(slime1);
    }

    // Función para mostrar el slime 2
    void ShowSlime2()
    {
        Debug.Log("Mostrar slime 2");
        SwitchSlime(slime2);
    }

    // Función para manejar el cambio de slime
    void SwitchSlime(GameObject newSlime)
    {
        Debug.Log("Desactivando el slime actual: " + currentActiveSlime.name);
        // Desactivar el slime actual
        if (currentActiveSlime != null)
        {
            currentActiveSlime.SetActive(false);
        }

        // Activar el nuevo slime
        currentActiveSlime = newSlime;
        Debug.Log("Activando el nuevo slime: " + newSlime.name);
        currentActiveSlime.SetActive(true);
    }
}
