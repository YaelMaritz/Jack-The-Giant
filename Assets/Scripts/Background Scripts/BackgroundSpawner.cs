using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour {

    private GameObject[] backgournds;

    private float lastBackgourndPositionY;

    // Init Functions

    private void Awake()
    {
        backgournds = GameObject.FindGameObjectsWithTag("Background");
    }

    void Start()
    {
        GetLastYPosition();
    }

    // Trigger Function

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Background") {
            if (collision.transform.position.y == lastBackgourndPositionY) {

                Vector3 temp = collision.transform.position; // position of last background
                float height = ((BoxCollider2D)collision).size.y; // cast collision as a BoxCollider2D to easily get height

                // go through all the backgrounds, find all the none-active ones, set them at the end, and activate them
                for (int i = 0; i < backgournds.Length; i++) {
                    if (!backgournds[i].activeInHierarchy) { 

                        temp.y -= height;
                        lastBackgourndPositionY = temp.y;
                        backgournds[i].transform.position = temp;

                        backgournds[i].SetActive(true);
                    }
                }
            }
        }
    }

    // Helper functions

    void GetLastYPosition()
    {
        // needed because FindGameObjectsWithTag throws the objects into the array at no order
        lastBackgourndPositionY = backgournds[0].transform.position.y;
        for (int i = 1; i < backgournds.Length; i++) {
            if (lastBackgourndPositionY > backgournds[i].transform.position.y)
                lastBackgourndPositionY = backgournds[i].transform.position.y;
        }
    }
}
