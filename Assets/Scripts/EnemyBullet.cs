using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    private Player playerScript; // how much damage does it deal to the player
    private Vector2 targetPosition; // store the player's position

    public float speed; // projectile's speed
    public int damage; // projectile's damage

    private void Start()
    {
        // point to the player
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPosition = playerScript.transform.position;
    }

    private void Update()
    {
        // if bullet didn't reach player
        if (Vector2.Distance(transform.position, targetPosition) > .1f) 
        {
            // continue to move forward
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        } else {
            // destroy when reached player
            Destroy(gameObject);
        }
    }

    // the same function like in projectile script
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "Player") 
        {
            playerScript.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
