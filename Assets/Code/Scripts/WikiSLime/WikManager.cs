using System;
using UnityEngine;
using UnityEngine.InputSystem;  // Necesario para el nuevo sistema de entrada

public class WikiManager : MonoBehaviour
{
    [SerializeField] private WikiSlime wikiMenu;    // Menú de Wiki
    [SerializeField] private InputAction wikiAction; // Acción de entrada para el menú

    private bool isWikiActive = false;  // Variable para controlar si el menú está activo o no

    private void OnEnable()
    {
        // Aseguramos que la acción se habilite al principio
        wikiAction.Enable();
    }

    private void OnDisable()
    {
        // Aseguramos que la acción se deshabilite al final
        wikiAction.Disable();
    }

    private void Start()
    {
        // Desactivamos el menú de Wiki al inicio
        wikiMenu.DesactiveWiki();

        // Aseguramos que el puntero esté oculto al inicio si el menú está desactivado
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;  // Opcional: bloquear el puntero en el centro
    }

    private void Update()
    {
        // Verificamos si la acción de entrada está siendo activada (por ejemplo, tecla 'I' presionada)
        if (wikiAction.triggered)
        {
            // Cambiamos el estado del menú (si está activo, lo desactivamos; si está desactivado, lo activamos)
            isWikiActive = !isWikiActive;

            if (isWikiActive)
            {
                // Activar el menú y mostrar el puntero
                wikiMenu.ActiveWiki();
                Cursor.visible = true; // Mostrar el puntero
                Cursor.lockState = CursorLockMode.None; // Desbloquear el puntero

                // Pausar el juego (detener el tiempo)
                Time.timeScale = 0;  // Detener el tiempo del juego
            }
            else
            {
                // Desactivar el menú y ocultar el puntero
                wikiMenu.DesactiveWiki();
                Cursor.visible = false; // Ocultar el puntero
                Cursor.lockState = CursorLockMode.Locked; // Opcional: bloquear el puntero en el centro

                // Reanudar el juego (restaurar el tiempo normal)
                Time.timeScale = 1;  // Reanudar el tiempo del juego
            }
        }
    }
}
