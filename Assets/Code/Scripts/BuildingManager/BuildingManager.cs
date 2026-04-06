using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InteractiveShop _shop;
    [SerializeField] private DroneZone _droneZone;
    [SerializeField] private Transform _spawnPlace;
    

    [Header("Prefs")]
    [SerializeField] private GameObject _incineratorHologram;
    [SerializeField] private GameObject _fusionerHologram;
    [SerializeField] private GameObject _cageHologram;
    [SerializeField] private GameObject _planterHologram;
    [SerializeField] private GameObject _siloHologram;


    /*
     cuan comprem el incinerador es desactivara el panell i es activara el holograma del incinerador.
     */
    public void IncineratorBuyed()
    {
        _shop.HandleKeyboardToggle();
        _droneZone.CallDrone(_incineratorHologram);
        //Instantiate(_incineratorHologram, _spawnPlace.position, _spawnPlace.rotation);
    }

    public void FusionerBuyed()
    {
        _shop.HandleKeyboardToggle();

        Instantiate(_fusionerHologram, _spawnPlace.position, _spawnPlace.rotation);
    }

    public void CageBuyed()
    {
        _shop.HandleKeyboardToggle();

        Instantiate(_cageHologram, _spawnPlace.position, _spawnPlace.rotation);
    }
    public void PlanterBuyed()
    {
        _shop.HandleKeyboardToggle();

        Instantiate(_planterHologram, _spawnPlace.position, _spawnPlace.rotation);
    }
    public void SiloBuyed()
    {
        _shop.HandleKeyboardToggle();

        Instantiate(_siloHologram, _spawnPlace.position, _spawnPlace.rotation);
    }
}
