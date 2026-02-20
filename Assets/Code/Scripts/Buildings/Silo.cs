using UnityEngine;

public class Silo : Building
{
    [SerializeField] private GameObject _inventoryPanel;

    private bool _playerInside;


    protected override void Awake() {
        position = this.transform.position;
    }

    private void OnEnable()
    {
        InputManager.Instance.OnInteract.AddListener(ActiveInventoryPanel);
    }

    private void OnDisable()
    {
        InputManager.Instance.OnInteract.RemoveListener(ActiveInventoryPanel);
    }

    public void SetPlayerInside(bool value)
    {
        _playerInside = value;

        if (!value)
            _inventoryPanel.SetActive(false);
    }

    private void ActiveInventoryPanel()
    {
        if (_playerInside)
            _inventoryPanel.SetActive(true);
    }
}
