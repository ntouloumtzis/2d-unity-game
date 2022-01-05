using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour {

    public float speed; // bosses speed

    private GameObject[] patrolPoints; // store all random points
    int randomPoint; // store the next random point on the map, who will calculate new random points each time he reaches one.

    // start function
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        // so boss can decide which random point to reach at (from the ones we already provide on Unity)
        patrolPoints = GameObject.FindGameObjectsWithTag("patrolPoints");
        randomPoint = Random.Range(0, patrolPoints.Length);
	}

    // update function
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // so boss can walk up to the random point
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, patrolPoints[randomPoint].transform.position, speed * Time.deltaTime);

        // when he reaches the desired random point
        if (Vector2.Distance(animator.transform.position, patrolPoints[randomPoint].transform.position) < 0.1f)
        {
            // recalculate the random point variable with a new one
            randomPoint = Random.Range(0, patrolPoints.Length);
        }
    }

    // when boss stops playing his walk animation
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}
}