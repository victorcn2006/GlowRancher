
using System;
using UnityEngine;

public class Silo : Building
{
    [SerializeField] private GameObject _inventoryPanel;

    protected override void Awake() {
        position = this.transform.position;
    }
}
