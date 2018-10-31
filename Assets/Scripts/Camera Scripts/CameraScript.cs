using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    private float speed = 1f; // amount of units to move per sec (Not frame!)
    private float acceleration = 0.2f;
    private float maxSpeed = 3.2f;

    [HideInInspector]
    public bool moveCamera;


	// Use this for initialization
	void Start () {
        moveCamera = true;
    }
	
	// Update is called once per frame
	void Update () { 
        if (moveCamera) MoveCamera();
    }

    void MoveCamera()
    {
        Vector3 temp = transform.position; // initial position of Camera

        float oldY = temp.y;
        float newY = temp.y - (speed * Time.deltaTime);
        /* Time.delta.Time = 1 second/(fps of the last frame). 
         * If you multiply it by "speed" (distance you want to move per sec) -
         * you get the distance needed to be rendered every frame so that after 1 sec you move the aformentioned speed.
         * Example: 30 fps (consistantly), 5 speed
         * Time.delta.Time = 1 second / 30 fps = 1/30 = 0.0333333333333333
         * speed * Time.deltaTime = 5 * 0.0333333333333333 = 0.1666666666666667 (distance to render every frame so that after 1 sec you get speed)
         * And truly: 0.1666666666666667 * 30 fp
         * s = 5
         */

        temp.y = Mathf.Clamp(temp.y, oldY, newY); // make sure temp.y is between current camera posistion and where it needs to be according to Time.deltaTime

        transform.position = temp; // make it so

        speed += acceleration * Time.deltaTime; // make it faster for next round
        if (speed > maxSpeed) speed = maxSpeed; // but not too fast
    }
}
