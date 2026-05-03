using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private GameObject _keyboardPanel;
    [SerializeField] private GameObject _gamepadPanel;
    [SerializeField] private UICarrousel _deviceCarrousel;

    private void OnEnable()
    {
        _deviceCarrousel.OnItemChanged += HandleItemChanged;
    }

    private void OnDisable()
    {
        _deviceCarrousel.OnItemChanged -= HandleItemChanged;
    }

    private void HandleItemChanged(UICarrousel.CarrouselItem item)
    {
        if (item.title == "Keyboard")
        {
            _keyboardPanel.SetActive(true);
            _gamepadPanel.SetActive(false);
        }
        else if (item.title == "Gamepad")
        {
            _keyboardPanel.SetActive(false);
            _gamepadPanel.SetActive(true);
        }
    }
}
