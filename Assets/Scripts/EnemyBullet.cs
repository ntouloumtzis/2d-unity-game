using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    private Player playerScript; // how much damage does it deal to the player
    private Vector2 targetPosition; // store the player's position

    public float speed; // projectile's speed
    public int damage; // projectile's damage

    public GameObject effect;

    private void Start()
    {
        // point to the player
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPosition = playerScript.transform.position;
    }

    private void Update()
    {
        // when bullet reaches the player
        if ((Vector2)transform.position == targetPosition) 
        {
            // spawn the particle effect
            Instantiate(effect, transform.position, Quaternion.identity);

            // destroy when reached player
            Destroy(gameObject);
        } else {
            // if bullet didn't reach player, then continue to move forward
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
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
