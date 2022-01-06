using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour 
{
    public Weapon weaponToEquip; // store the weapon pickup

    public GameObject effect; // store the particle pickup effect

    // able to detect collisions
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        // check if player is found
        if (collision.tag == "Player")
        {
            // spawn the particle effect
            Instantiate(effect, transform.position, Quaternion.identity);

            // reference to player calling our function
            collision.GetComponent<Player>().ChangeWeapon(weaponToEquip);

            // destroy when equipped
            Destroy(gameObject);
        }
    }
}