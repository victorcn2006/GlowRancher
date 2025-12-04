using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incinerator : Building
{
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.GetComponent<IAspirable>() != null) {
            Destroy(collision.gameObject);
        }
        
    }
}
