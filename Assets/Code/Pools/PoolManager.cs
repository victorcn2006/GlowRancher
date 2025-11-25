using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [System.Serializable]
    public class Pool
    {
        public string id;
        public GameObject prefab;
        [HideInInspector] public List<GameObject> objects = new List<GameObject>();
    }

    public List<Pool> pools;
    private Dictionary<string, Pool> poolsDictionary;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        poolsDictionary = new Dictionary<string, Pool>();

        // Inicializamos las listas y el diccionario, pero NO instanciamos objetos
        foreach (var pool in pools)
        {
            pool.objects = new List<GameObject>();
            poolsDictionary.Add(pool.id, pool);
        }
    }

    public GameObject GetFirstAvailableObject(string id)
    {
        if (id == null)
        {
            id = "NullId";
        }

        if (!poolsDictionary.ContainsKey(id))   
        {
            Debug.Log("No existe pool con ID: " + id);
            return null;
        }

        Pool actualPool = poolsDictionary[id];

        // Buscamos un objeto libre
        foreach (GameObject obj in actualPool.objects)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // Si no hay libres, instanciamos uno nuevo dinámicamente
        GameObject newObj = Instantiate(actualPool.prefab);
        actualPool.objects.Add(newObj);
        newObj.SetActive(true);
        return newObj;
    }
}
