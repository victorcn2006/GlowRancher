using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAspiratorTutorial : MonoBehaviour
{

    [SerializeField] GameObject _lightGo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DeleteObject(_lightGo);
        }
    }

    public void DeleteObject(GameObject obj) => Destroy(obj);
}
