using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour 
{

    public int health; // bosses health
    public Enemy[] enemies; // every time boss takes damage he will spawn a random enemy, so we store their values to an array
    public float spawnOffset; // so the random spawned enemy can spawn anywhere on the map and not to the bosses position

    private int halfHealth; // to detect when boss health drop to half of it, so the chaseBehaviour can execute
    private Animator anim; // store bosses animations

    public int damage; // bosses damage

    // public GameObject blood;
    // public GameObject effect;

    // private Slider healthBar;

    // private SceneTransition sceneTransitions;

    private void Start()
    {
        halfHealth = health / 2; // calculate the bosses health when drops to its half
        anim = GetComponent<Animator>(); // reference to his animations
        // healthBar = FindObjectOfType<Slider>();
        // healthBar.maxValue = health;
        // healthBar.value = health;
        // sceneTransitions = FindObjectOfType<SceneTransition>();
    }

    // everytime boss takes damage, he lose health and spawn an enemy
    public void TakeDamage(int amount)
    {
        // reduce his health
        health -= amount;
        // healthBar.value = health;

        // when bosses health drops to 0
        if (health <= 0)
        {
            // Instantiate(effect, transform.position, Quaternion.identity);
            // Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(this.gameObject); // destroy boss object
            // healthBar.gameObject.SetActive(false);
            // sceneTransitions.LoadScene("Win");
        }

        // if its equal or lower his half health
        if (health <= halfHealth)
        {
            anim.SetTrigger("stage2"); // chase behaviour begins
        }

        // spawn the random enemy to a corresponding spawnOffset
        Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];
        Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0), transform.rotation);

    }

    // when boss finds out the player, then he takes damage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }

}