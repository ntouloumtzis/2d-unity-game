using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed; // speed of the bullet
    public float lifeTime; // how long the bullet survives in the game

    public GameObject explosion; // this variable holds our explosion particle effect for the projectile, when it is destroyed

    public int damage; // the damage the enemy takes

    private void Start()
    {
        // Invoke(our function which will destroy our projectile, how long will it survive in the game). 
        // In other words, when lifeTime is passed, destroy the bullet
        Invoke("DestroyProjectile", lifeTime);        
    }

    private void Update()
    {
        // move projectile (forward * the speed of the projectile * frame-rate independent)
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjectile() 
    {
        // destroy projectile
        Destroy(gameObject);

        // spawn(What are we spawning, at what position, at what rotation)
        Instantiate(explosion, transform.position, Quaternion.identity);
    }

    // a built-in Unity function, which stores the object that it is collided with (in our case projectile with an enemy)
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        // check if our object is collided with the below tag
        if (collision.tag == "Enemy") 
        {
            // call our function, so enemy's health drop
            collision.GetComponent<Enemy>().TakeDamage(damage);
            DestroyProjectile();
        }
    }
}
