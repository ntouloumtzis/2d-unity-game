using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject projectile; // What bullet to projectile
    public Transform shotPoint; // From where do we want to spawn
    public float timeBetweenShots; // attack speed of the weapon
    private float shotTime; // this variable will contain the exact time we can shoot at
    
    private void Update()
    {
        // the Input.mousePosition returns pixel coordinates and transform.position returns Unity's default unit size. 
        // We cannot calculate two different unit sizes. 
        // That's why, we need to calculate the Input.mousePosition in units, using the below function and then subtract it with the weapons position.
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        // We need to transfrom the above direction into an angle, using the below function and, then multiply it with Mathf.Rad2Deg to convert it into degrees instead of radians (1 radian = 180/π degrees)
        // So we calculate the angle the weapon must rotate around to face the cursor
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Convert our float angle into a Unity rotation (Quaternion) and we rotate the weapon with the Z axis (Vector3.forward)
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        // Finally, the initial weapon's rotation is equal to our desired rotation value.
        transform.rotation = rotation;

        // check when player press the left mouse button
        if(Input.GetMouseButton(0)) {
            // check if we're allowed to shoot
            if (Time.time >= shotTime) {
                // spawn projectile (the type we choose from Unity, the spawn location, the weapons rotation)
                Instantiate(projectile, shotPoint.position, transform.rotation);

                // recalculate the shotTime
                shotTime = Time.time + timeBetweenShots;
            }
        }
    }
}
