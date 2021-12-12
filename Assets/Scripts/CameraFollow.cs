using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target; // to assign the target to Unity
    public float speed; // to assign speed of camera to Unity

    // to assign restrictions to the player to Unity
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Start()
    {
        // current position of the camera = target's position
        transform.position = target.position;
    }

    private void LateUpdate()
    {
        // check in case target dies (When player dies target === null)
        if (target != null)
        {
            // we set restrictions (clamp) to the player, so he shouldn't exceed the coordinates we'll provide to Unity
            float clampedX = Mathf.Clamp(target.position.x, minX, maxX);
            float clampedY = Mathf.Clamp(target.position.y, minY, maxY);

            // Smoothly move from one point to another based on speed (current position, restrictions, speed of the camera)
            transform.position = Vector2.Lerp(transform.position, new Vector2(clampedX, clampedY), speed);
        }
    }
}