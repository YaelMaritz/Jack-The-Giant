using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounds : MonoBehaviour {

    private float maxX, minX;

	void Start () {
        SetMinMax();

    }
	
	void Update () {
		if (transform.position.x < minX) {
            Vector3 temp = transform.position; // position of object with this script, in this case the player
            temp.x = minX;
            transform.position = temp;
        }

        if (transform.position.x > maxX) {
            Vector3 temp = transform.position;
            temp.x = maxX;
            transform.position = temp;
        }
    }

    private void SetMinMax()
    {
        /* ScreenToWorldPoint:
         * Convert from the native platform's screen coordinate system (pixels) to Unity's World coordinate system (units).
         * In this case the vector originates at the center of the screen (camera is at world origin) and ends at top and right edges of the view screen.
         */
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        maxX = bounds.x;
        minX = -bounds.x;
    }
}
