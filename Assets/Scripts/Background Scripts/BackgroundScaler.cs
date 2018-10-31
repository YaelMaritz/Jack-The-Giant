using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScaler : MonoBehaviour {

	void Start () {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector3 tempScale = transform.localScale;

        float spriteWidth = sr.sprite.bounds.size.x; // width of the sprite

        float orthographicHeight = Camera.main.orthographicSize * 2.0f; // vertical viewing volume = half of the Camera "Size" Property.
        float orthographicWidth = orthographicHeight * Screen.width / Screen.height; // horizontal viewing volume depends on aspect ratio.

        tempScale.x = orthographicWidth / spriteWidth; // orthographicWidth / spriteWidth = the ratio (multiplier) needed to scale the sprite width to the viewing volume

        transform.localScale = tempScale; // do it.

    }

}
