using UnityEngine;
using UnityEngine.EventSystems;

public class Silo : Building
{
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private GameObject _firstSlot;

    private bool _playerInside;


    protected override void Awake() {
        position = this.transform.position;
    }

    private void OnEnable()
    {
        InputManager.Instance.OnInteractPerformed.AddListener(ActiveInventoryPanel);
    }

    private void OnDisable()
    {
        InputManager.Instance.OnInteractPerformed.RemoveListener(ActiveInventoryPanel);
    }

    public void SetPlayerInside(bool value)
    {
        _playerInside = value;

        if (!value)
            _inventoryPanel.SetActive(false);
    }

    public void ToggleInventory()
    {
        if (_playerInside)
        {
            if (GameManager.Instance != null) GameManager.Instance.AddSiloOpened();
            _inventoryPanel.SetActive(!_inventoryPanel.activeSelf);
            if (_inventoryPanel.activeSelf)
                EventSystem.current.SetSelectedGameObject(_firstSlot);
        }
    }

    private void ActiveInventoryPanel()
    {
        if (_playerInside)
            _inventoryPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_firstSlot);
    }
}
