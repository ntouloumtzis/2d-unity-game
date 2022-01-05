using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // to reference the player and hide the variable to Unity inspector (because its public)
    [HideInInspector]
    public Transform player;

    public int health; // enemy's health
    public float speed; // the enemy's speed
    public float timeBetweenAttacks; // the enemy's attack speed
    public int damage; // enemy's damage

    public int pickupChance;
    public GameObject[] pickups;

    public int healthPickupChance;
    public GameObject healthPickup;

    public virtual void Start() 
    {
        // We define the Player tag to our player
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // how much damage the enemy takes. The function takes as parameter the damage amount
    public void TakeDamage(int amount) 
    {
        health -= amount;

        // check whether enemy's health drop to zero, then destroy
        if (health <= 0) 
        {
            // the random.range function excludes the last number, so that's why we need 101
            int randomNumber = Random.Range(0, 101);
            if (randomNumber < pickupChance) {

                // select what to spawn from our variety of pickups
                GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];

                // spawn it
                Instantiate(randomPickup, transform.position, transform.rotation);
            }

            // the same as weapon pickups
            int randHealth = Random.Range(0, 101);
            if (randHealth < healthPickupChance) 
            {
                Instantiate(healthPickup, transform.position, transform.rotation);
            }

            Destroy(this.gameObject);
        }
    }
}