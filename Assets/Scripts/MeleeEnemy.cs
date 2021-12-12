using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{

    public float stopDistance; // define when enemy needs to stop when reaches our player
    private float attackTime; // variable which holds the value, when the enemy is allowed to attack
    public float attackSpeed; // enemy's attack speed

    private void Update()
    {
        // check if player is dead
        if (player != null) 
        {
            // check if enemy is far enough to the player
            if (Vector2.Distance(transform.position, player.position) > stopDistance) 
            {
                // continue to move towards the player
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            } else {
                if (Time.time >= attackTime) 
                {
                    // attack
                    StartCoroutine(Attack());
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }
        }
    }

    // A function is called once a single frame. We want the enemy to attack the player with an animation (leap towards him) and come back to its initial position
    // In this case, we need a coroutine to be able to see the animation attack
    IEnumerator Attack() 
    {
        // deal damage to the player
        player.GetComponent<Player>().TakeDamage(damage);

        // position before enemy hits
        Vector2 originalPosition = transform.position;

        // position when enemy ends its attack. In other words, reach the player
        Vector2 targetPosition = player.position;

        float percent = 0; // we hold the animation progress into a variable. 0 = animation starts, 1 = animation ends
        while (percent <= 1) 
        {
            // increase the value of percent variable, based on time and enemy's attack speed
            percent += Time.deltaTime * attackSpeed;

            // manages to make the animation during the attack
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4; 
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);

            // run the animation over a period of time, because its going to skip to the next frame
            // That's why we need a coroutine in a first place
            yield return null;
        }
    }
}
