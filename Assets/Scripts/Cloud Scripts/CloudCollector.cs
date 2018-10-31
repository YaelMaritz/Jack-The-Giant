using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCollector : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Cloud" || collision.tag == "Deadly")
        {
            collision.gameObject.SetActive(false);
        }
    }
}
