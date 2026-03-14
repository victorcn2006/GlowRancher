using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketEvent : MonoBehaviour
{
    [Header("Gloabal")]
    private int _timeZero = 0;

    #region Timer
    [Header("Timer")]
    private const float MAXTIEMPO_EVENTO = 1000;
    private const float MINTIEMPO_EVENTO = 800;
    public float _tiempoRestante = 0;
    #endregion

    #region Event
    [Header("Event")]
    private const float MAXDURACION_EVENTO = 300;
    private const float MINDURACION_EVENTO = 100;
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
        if (_tiempoRestante < _timeZero)
        {
            _tiempoRestante = Random.Range(MINTIEMPO_EVENTO, MAXTIEMPO_EVENTO);         //Generem el temps de l'espera per l'event
        }


        if (!_eventoActivo)
        {
            if (_tiempoRestante > _timeZero)
            {
                _tiempoRestante -= Time.deltaTime; //temps restant
            }
            else
            {
                _tiempoRestante = 0;
                _eventoActivo = true;
                _tiempoEvento = Random.Range(MAXDURACION_EVENTO, MINDURACION_EVENTO);       //generem el temps de l'event
                Event();
            }
        }

    }

    private void Event()
    {
        if (_eventoActivo)
        {
            //aqui aniria el multiplayer de valor de la slimestone
            marketManager.EconomyEvent();
            if (_tiempoEvento > _timeZero)
            {
                _tiempoEvento -= Time.deltaTime;
                _tiempoEvento = 0;
            }
            else
            {
                _eventoActivo = false;
                _tiempoRestante = 0;
            }
        }
    }
}
