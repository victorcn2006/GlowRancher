using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPuzzle : MonoBehaviour
{
    [SerializeField] private List<GameObject> _magicRocksOrderList = new List<GameObject>();
    private List<GameObject> _activeMagicRocks = new List<GameObject>();

    private MonolitoManager _monolitoManager;

    private void Awake()
    {
        _monolitoManager = GetComponentInChildren<MonolitoManager>();
    }

    public void AddActiveRock(GameObject newRock)
    {
        _activeMagicRocks.Add(newRock);
        CheckRocksOrder();
    }

    private void CheckRocksOrder()
    {
        int index = _activeMagicRocks.Count - 1;
        if (_magicRocksOrderList[index] != _activeMagicRocks[index])
        {
            foreach(GameObject rock in _activeMagicRocks)
            {
                rock.GetComponent<MagicRock>().DeactivateRock();
            }
            _activeMagicRocks.Clear();
        }

        if (_activeMagicRocks.Count == _magicRocksOrderList.Count)
        {
            _monolitoManager.ActivateMonolito();
        }

    }
}
