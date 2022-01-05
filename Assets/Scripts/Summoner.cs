using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy
{
    // define restrictions coordinates
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    
    private Vector2 targetPosition; // store the position of a selected area of the map, where he'll spawn his minions
    private Animator anim; // store the animation values from Unity 
    public Enemy enemyToSummon; // store the desired enemy

    public float timeBetweenSummons; // summon speed
    private float summonTime; // when summoner is allowed to summon an enemy
    public float attackSpeed; // summoner's attack speed
    public float stopDistance; // how far can summoner hit the player, so he needs to stop
    private float attackTime; // when is he allowed to attack

    // We need to override the Start function of the general enemy script (we don't want to run the enemy script's start function)
    public override void Start()
    {
        base.Start(); // call the start function from the enemy script

        // create a random number, based on coordinates and select it
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(randomX, randomY);
        anim = GetComponent<Animator>(); // for the animations to work
    }

    private void Update()
    {
        // check if player's alive
        if (player != null) 
        {
            // check if current position and target position is greater than a really small value
            if (Vector2.Distance(transform.position, targetPosition) > .5f) 
            {
                // continue to target position and play the run animation
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                anim.SetBool("isRunning", true);
            } else {
                // when reached the desired target position, play the idle animation
                anim.SetBool("isRunning", false);

                if (Time.time >= summonTime) 
                {
                    // increase the summon time
                    summonTime = Time.time + timeBetweenSummons;

                    // call an animation event, once the summoner finished the summoning animation. We create an event in Unity
                    anim.SetTrigger("summon"); 
                } 
            }

            // check if summoner is near the player, so he can melee attack
            if (Vector2.Distance(transform.position, player.position) < stopDistance) 
            {
                if (Time.time > attackTime) 
                {
                    // increase the melee attack time
                    attackTime = Time.time + timeBetweenAttacks;

                    // make a melee attack
                    StartCoroutine(Attack());
                } 
            }
        }
    }

    // summon function
    public void Summon() 
    {
        // check if player's dead
        if (player != null) 
        {
            // spawn a minion (which enemy to summon, position to spawn, rotation to spawn)
            Instantiate(enemyToSummon, transform.position, transform.rotation);
        }
    }

    // the same attack coroutine as a melee enemy 
    IEnumerator Attack() 
    {
        player.GetComponent<Player>().TakeDamage(damage);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0;
        while (percent <= 1) 
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }

}
