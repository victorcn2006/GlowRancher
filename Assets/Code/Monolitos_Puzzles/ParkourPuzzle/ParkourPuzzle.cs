using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourPuzzle : MonoBehaviour
{
    [SerializeField] private List<GameObject> _parkourRocks;

    public void UnlockParkour()
    {
        foreach(GameObject rock in _parkourRocks)
        {
            rock.gameObject.SetActive(true);
        }
    }


}
