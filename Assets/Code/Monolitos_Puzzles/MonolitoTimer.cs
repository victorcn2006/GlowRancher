using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonolitoTimer : MonoBehaviour
{
    /* NUMEROS MONOLITOS
     * 0 = TUTORIAL
     * 1 = PLACAS DE PRESION
     * 2 = PARKOUR
     * 3 = MAGICO
     * 4 = HIELO (ES EL MONOLITO DE LA ZONA DE HIELO, NO LA PARED DE HIELO, LA PARED NO CUENTA COMO PUZZLE)
     * */

    public int _monolitoNumber;

    private float _monolitoTime = 0f;
    private bool _completed;

    void Update()
    {
        if (!_completed)
        {
            _monolitoTime += Time.deltaTime;
        }
        
    }

    public int GetMonolitoNumber() {
        return _monolitoNumber;
    }
    public void StopTimer()
    {
        _completed = true;
        if (GameManager.Instance != null) GameManager.Instance.SetTimePuzzle(_monolitoTime, _monolitoNumber);
    }

}
