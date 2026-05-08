using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class References : MonoBehaviour
{
    public static References Instance { get; private set; }

    [Header("HOUSE REFERENCES")]
    public GameObject _houseShopPanel;
    public PlayerCameraMovement _playerCameraMovement;

    //[Header("SHOP REFERENCES")]


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }
}
