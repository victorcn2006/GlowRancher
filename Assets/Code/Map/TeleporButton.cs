using System.Collections;
using UnityEngine;

public class TeleporButton : MonoBehaviour
{
    [SerializeField] private GameObject _map;
    [SerializeField] private InteractiveMap _update;
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _teleport;
    [SerializeField] private float _delayTime = 2.0f;
    Coroutine _coroutine;
    public void OnButtonClick()
    {
        // IMPORTANTE: La lógica debe estar DENTRO de la corrutina
        if (_coroutine == null)
        {
            _coroutine = GameManager.Instance.StartCoroutine(TeleportSequence());
        }
    }

    //IEnumerator TeleportSequence()
    //{
    //    Debug.Log("Esperando " + _delayTime + " segundos...");

    //    yield return new WaitForSeconds(_delayTime);

    //    if (_player != null && _teleport != null)
    //    {
    //        // Intentar obtener el CharacterController
    //        CharacterController cc = _player.GetComponent<CharacterController>();

    //        // Si tiene CharacterController, hay que apagarlo un milisegundo
    //        if (cc != null) cc.enabled = false;

    //        // Teletransporte
    //        _player.transform.position = _teleport.position;
    //        _player.transform.rotation = _teleport.rotation;

    //        // Lo volvemos a encender
    //        if (cc != null) cc.enabled = true;

    //        Debug.Log("¡Teletransportado!");
    //    }

    //    // Cerramos el mapa después de movernos
    //    if (_map != null) _map.SetActive(false);
    //    if (_update != null) _update.UpdateGameState(false);
    //}


    public IEnumerator TeleportSequence()
    {
        Debug.Log("A");
        _map.SetActive(false);
        yield return new WaitForSecondsRealtime(2.0f);

        Debug.Log("BA");
        if (_player != null && _teleport != null)
        {
        Debug.Log("B");
            // Mueve la posición
            _player.transform.position = _teleport.position;

            // Aplica la rotación completa (es la forma correcta)
            _player.transform.rotation = _teleport.rotation;
        }

        Debug.Log("C");

        if (_update != null)
        {
        Debug.Log("D");
            _update.UpdateGameState(false);
        }
        _coroutine = null;
    }


}
