using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //OnCollisionEnter
        if (collision.collider.tag == "Gem")
        {
            //que destrueixi la Slimestone

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
