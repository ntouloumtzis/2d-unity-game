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
    public void TakeDamage(int amount) {
        health -= amount;

        // check whether enemy's health drop to zero, then destroy
        if (health <= 0) 
        {
            int randomNumber = Random.Range(0, 101);
            if (randomNumber < pickupChance) {
                GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];
                Instantiate(randomPickup, transform.position, transform.rotation);
            }

            int randHealth = Random.Range(0, 101);
            if (randHealth < healthPickupChance) {
                Instantiate(healthPickup, transform.position, transform.rotation);
            }

            Destroy(this.gameObject);
        }
    }

    
}