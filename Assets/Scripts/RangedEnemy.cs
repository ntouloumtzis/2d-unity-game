using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{

    public float stopDistance; // when enemy reaches the player's range
    private float attackTime; // when enemy is allowed to attack
    private Animator anim; // access animations from Unity

    public Transform shotPoint;
    public GameObject enemyBullet; // define the projectile the enemy will fire

    // We need to override the Start function of the general enemy script
    public override void Start()
    {
        base.Start(); // call the start function from the enemy script so enemy can follow the player
        anim = GetComponent<Animator>(); // Attach animation components to our ranged enemy
    }

    private void Update()
    {
        // check if player's dead
        if (player != null) 
        {
            // check if enemy is far from player
            if (Vector2.Distance(transform.position, player.position) > stopDistance) 
            {
                // continue to follow the player
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }

            if (Time.time >= attackTime) 
            {
                // increase the attack time
                attackTime = Time.time + timeBetweenAttacks;
                anim.SetTrigger("attack"); // so the animation can work
            }
        }
    }

    // ranged attack function (similar to weapons functionality)
    public void RangedAttack() 
    { 
        Vector2 direction = player.position - shotPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        shotPoint.rotation = rotation;

        // spawn(which projectile type, what position to spawn, what rotation to spawn)
        Instantiate(enemyBullet, shotPoint.position, shotPoint.rotation);
    }
}
