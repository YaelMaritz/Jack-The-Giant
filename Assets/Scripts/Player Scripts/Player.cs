using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 8f, maxVelocity = 4f;
    private Animator playerAnimator;
    private Rigidbody2D playerRigidbody;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Start () {
		
	}
	
	// called every 3-4 frames (Best function for code that deals with physics)
	void FixedUpdate () {
        PlayerMoveKeyboard();
    }

    void PlayerMoveKeyboard()
    {
        float forceX = 0f;
        float velocity = Mathf.Abs(playerRigidbody.velocity.x);
        float direction = Input.GetAxisRaw("Horizontal"); // AxisRaw: Left arrow & A = -1, Nothing = 0, Right arrow & D = 1

        // going right
        if (direction > 0)
        {
            if (velocity < maxVelocity)
            {
                // apply max speed
                forceX = speed;

                // face the right way
                Vector3 temp = transform.localScale;
                temp.x = 1.3f;
                transform.localScale = temp;

                // make animation play
                playerAnimator.SetBool("Walk", true); 
            }
            
        }
        // going left
        else if (direction < 0)
        {
            if (velocity < maxVelocity)
            {
                forceX = -speed;

                Vector3 temp = transform.localScale;
                temp.x = -1.3f;
                transform.localScale = temp;

                playerAnimator.SetBool("Walk", true);
            }
        }
        else
        {
            playerAnimator.SetBool("Walk", false);
        }
        playerRigidbody.AddForce(new Vector2(forceX, 0));
    }
}
