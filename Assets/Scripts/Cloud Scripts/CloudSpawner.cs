using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject[] clouds;

    private float distanceBetweenClouds = 3f;

    private float minX, maxX;

    private float lastCloudPositionY;

    private float controlX;

    [SerializeField]
    private GameObject[] collectables;

    private GameObject player;

    // Init Functions

	void Awake () {
        controlX = 0;
        SetMinMax();
        CreateClouds();
        player = GameObject.Find("Player");
    }

    void Start()
    {
        PositionPlayer();
    }

    // Trigger Function

    void OnTriggerEnter2D(Collider2D collision)
    {
        // if object being bumped into is a cloud (or a bad cloud)
        if (collision.tag == "Cloud" || collision.tag == "Deadly")
        {
            // if it's the last cloud
            if (collision.transform.position.y == lastCloudPositionY)
            {
                Shuffle(clouds);
                Shuffle(collectables);

                Vector3 temp = collision.transform.position; // position of last cloud
                for(int i = 0; i < clouds.Length; i++)
                {
                    if (!clouds[i].activeInHierarchy) // if NOT active
                    {
                        temp.x = ZigzagPositionX(temp);
                        temp.y -= distanceBetweenClouds;
                        clouds[i].transform.position = temp;

                        clouds[i].SetActive(true);

                        lastCloudPositionY = temp.y;
                    }
                }
            }
        }
    }

    // Helper functions

    void CreateClouds()
    {
        Shuffle(clouds);

        float positionY = 0f; // first cloud will be half way up viewscreen

        for (int i = 0; i < clouds.Length; i++)
        {
            lastCloudPositionY = positionY; // find & save position of last cloud created

            Vector3 temp = clouds[i].transform.position;
            temp.x = ZigzagPositionX(temp);
            temp.y = positionY;
            clouds[i].transform.position = temp;

            positionY -= distanceBetweenClouds; // prep for next round
        }
    }

    void PositionPlayer()
    {
        GameObject[] darkClouds = GameObject.FindGameObjectsWithTag("Deadly");
        GameObject[] lightClouds = GameObject.FindGameObjectsWithTag("Cloud");

        // make sure the dark clouds are NOT the highest.
        for (int i = 0; i < darkClouds.Length; i++)
        {
            if (darkClouds[i].transform.position.y == 0)
            {
                // switch places with the first regular cloud.
                Vector3 t = darkClouds[i].transform.position;
                darkClouds[i].transform.position = new Vector3(lightClouds[0].transform.position.x, lightClouds[0].transform.position.y, lightClouds[0].transform.position.z);
                lightClouds[0].transform.position = t;
            }
        }

        // find the highest cloud's y position (compare and up poistion if necessary).
        Vector3 temp = lightClouds[0].transform.position;
        for (int i = 1; i < lightClouds.Length; i++)
        {
            if (temp.y < lightClouds[i].transform.position.y)
            {
                temp = lightClouds[i].transform.position;
            }
        }

        // position player on highest cloud.
        temp.y += 0.8f;
        player.transform.position = temp;
    }

    private void SetMinMax()
    {
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)); // ScreenToWorldPoint = convert from the native screen's coordinate system to Unity World coordinate system
        maxX = bounds.x - 0.5f;
        minX = -bounds.x + 0.5f;
    }

    void Shuffle(GameObject[] arrayToShuffle)
    {
        for (int i = 0; i < arrayToShuffle.Length; i++)
        {
            GameObject temp = arrayToShuffle[i];
            int random = Random.Range(i, arrayToShuffle.Length);

            arrayToShuffle[i] = arrayToShuffle[random];
            arrayToShuffle[random] = temp;
        }
    }

    private float ZigzagPositionX(Vector3 target)
    {
        Vector3 temp = target;
        if (controlX == 0)
        {
            temp.x = Random.Range(0.0f, maxX);
            controlX = 1;
        }
        else if (controlX == 1)
        {
            temp.x = Random.Range(0.0f, minX);
            controlX = 2;
        }
        else if (controlX == 2)
        {
            temp.x = Random.Range(1.0f, maxX);
            controlX = 3;
        }
        else if (controlX == 3)
        {
            temp.x = Random.Range(-1.0f, minX);
            controlX = 0;
        }
        return temp.x;
    }

}
