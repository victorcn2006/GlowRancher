using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonTP : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _teleport;
    public void OnButtonClick()
    {
        if (_player != null && _teleport != null)
        {
            // Copia la posición exacta del destino
            _player.position = _teleport.localPosition;

        }
    }
}
