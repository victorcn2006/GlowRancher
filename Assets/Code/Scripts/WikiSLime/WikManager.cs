using System;
using UnityEngine;
using UnityEngine.InputSystem;  // Necesario para el nuevo sistema de entrada

public class WikiManager : MonoBehaviour
{
    [SerializeField] private WikiSlime _wikiMenu;    // Menú de Wiki
    [SerializeField] private InputAction _wikiAction; // Acción de entrada para el menú

    private bool _isWikiActive = false;  // Variable para controlar si el menú está activo o no

    private void OnEnable()
    {
        // Aseguramos que la acción se habilite al principio
        _wikiAction.Enable();
    }

    private void OnDisable()
    {
        // Aseguramos que la acción se deshabilite al final
        _wikiAction.Disable();
    }

    private void Start()
    {
        // Desactivamos el menú de Wiki al inicio
        _wikiMenu.DesactiveWiki();
        InputManager.Instance.SetWikiOpen(false);
        // Aseguramos que el puntero esté oculto al inicio si el menú está desactivado
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;  // Opcional: bloquear el puntero en el centro
    }

    private void Update()
    {
        // Verificamos si la acción de entrada está siendo activada (por ejemplo, tecla 'I' presionada)
        if (_wikiAction.triggered)
        {
            if (InputManager.Instance != null && InputManager.Instance.isPaused)
                return;
            // Cambiamos el estado del menú (si está activo, lo desactivamos; si está desactivado, lo activamos)
            _isWikiActive = !_isWikiActive;

            if (_isWikiActive)
            {
                // Activar el menú y mostrar el puntero
                _wikiMenu.ActiveWiki();
                Cursor.visible = true; // Mostrar el puntero
                Cursor.lockState = CursorLockMode.None; // Desbloquear el puntero
                Time.timeScale = 0f;
                InputManager.Instance.SetWikiOpen(true);
            }
            else
            {
                // Desactivar el menú y ocultar el puntero
                _wikiMenu.DesactiveWiki();
                Cursor.visible = false; // Ocultar el puntero
                Cursor.lockState = CursorLockMode.Locked; // Opcional: bloquear el puntero en el centro
                Time.timeScale = 1f;
                InputManager.Instance.SetWikiOpen(false);
            }
        }
    }
}
