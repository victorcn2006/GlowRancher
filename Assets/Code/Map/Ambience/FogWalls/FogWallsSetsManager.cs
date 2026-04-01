using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FogWallsSetsManager : MonoBehaviour
{
    public static FogWallsSetsManager Instance;

    [SerializeField] private int _fogSet; //Guardar para saber que fogSet se tiene que mostrar al iniciar la escena

    [SerializeField] private List<GameObject> _fogWallSets = new List<GameObject>();
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

    }

    private void Start()
    {
        UpdateFogSet(_fogSet);

    }

    private void Update()
    {
        //UpdateFogSet(_fogSet); //descomentar para debugear
    }


    public void UpdateFogSet(int fogSet) //Llamar al cargar el mapa
    {
        _fogSet = fogSet;
        foreach (GameObject fogWallSet in _fogWallSets)
        {
            if (fogWallSet.gameObject == _fogWallSets[fogSet].gameObject)
            {
                fogWallSet.SetActive(true);
            }
            else fogWallSet.SetActive(false);
        }

    }

}
