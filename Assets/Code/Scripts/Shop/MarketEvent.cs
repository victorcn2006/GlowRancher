using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketEvent : MonoBehaviour
{
    #region Timer
    [Header("Timer")]
    private float _MaxTiempoEvento = 10;
    private float _MinTiempoEvento = 0;
    public float _tiempoRestante = 0;
    #endregion

    #region Event
    [Header("Event")]
    private float _MaxDuracionEvento = 10;
    private float _MinDuracionEvento = 1;
    private float _tiempoEvento = 0;
    private bool _eventoActivo = false;
    #endregion


    [Header("Refs")]
    public MarketManager marketManager;

    private void Start()
    {
        GameObject _marketManagerFinder = GameObject.Find("MarketManager");//trobar el gameobject
        marketManager = _marketManagerFinder.GetComponent<MarketManager>();
    }
    private void Update()
    {
        TimeToEvent();
    }


    private void TimeToEvent()
    {
        if (_tiempoRestante < 0)
        {
            _tiempoRestante = Random.Range(_MinTiempoEvento, _MaxTiempoEvento);         //Generem el temps de l'espera per l'event
            Debug.Log("In Espera");
        }


        if (!_eventoActivo)
        {
            if (_tiempoRestante > 0)
            {
                _tiempoRestante -= Time.deltaTime; //temps restant
            }
            else
            {
                Debug.Log("Out Espera");
                _tiempoRestante = 0;
                _eventoActivo = true;
                _tiempoEvento = Random.Range(_MaxDuracionEvento, _MinDuracionEvento);       //generem el temps de l'event
                Event();
            }
        }

    }

    private void Event()
    {
        if (_eventoActivo)
        {
            //aqui aniria el multiplayer de valor de la slimestone
            Debug.Log("Millora de preu");
            marketManager.EconomyEvent();
            if (_tiempoEvento > 0)
            {
                Debug.Log("EventUnderway");
                _tiempoEvento -= Time.deltaTime;
                _tiempoEvento = 0;

            }
            else
            {
                Debug.Log("Out Event");
                _eventoActivo = false;
                _tiempoRestante = 0;
                Debug.Log("Preu torna a la normalitat");
            }
        }
    }
}
