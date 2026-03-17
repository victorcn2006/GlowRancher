using System.Collections;
using DG.Tweening;
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
    public IEnumerator TeleportSequence()
    {
        _map.SetActive(false);
        DoAnimationRotate();
        yield return new WaitForSecondsRealtime(2f);
        yield return new WaitForSecondsRealtime(_delayTime);
        Teleport();
        UpdateGame();


;
    }

    public void Teleport()
    {
        if (_player != null && _teleport != null)
        {

            // Mueve la posición
            _player.transform.position = _teleport.position;

            // Aplica la rotación completa (es la forma correcta)
            _player.transform.rotation = _teleport.rotation;
        }
    }

    private void UpdateGame()
    {
        if (_update != null)
        {
            ;
            _update.UpdateGameState(false);
        }
        _coroutine = null;
    }

    private void DoAnimationRotate()
    {
        if (_player != null && _teleport != null)
        {
            // Usamos DORotate para que la rotación del jugador coincida 
            // exactamente con la rotación del objeto destino (_teleport)
            _player.transform.DORotate(_teleport.eulerAngles, _delayTime)
                .SetEase(Ease.InOutSine)
                .SetUpdate(UpdateType.Normal, true);
        }
    }
}
