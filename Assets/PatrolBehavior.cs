using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour {

    public float speed; // define boss' speed

    private GameObject[] patrolPoints; // array which holds all the patrol points

    int randomPoint; // boss will choose patrol points on the map. This is a checkpoint where the boss chooses a random point and then another one and so on

    // start function
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        // follow those points
        patrolPoints = GameObject.FindGameObjectsWithTag("point");

        // choose which point to follow
        randomPoint = Random.Range(0, patrolPoints.Length);
	}

    // update function (every single frame)
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        // move towards the desired random point
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, patrolPoints[randomPoint].transform.position, speed * Time.deltaTime);

        // detect whether reaches this random point
        if (Vector2.Distance(animator.transform.position, patrolPoints[randomPoint].transform.position) < 0.1f)
        {
            // and so recalculate the random point with a new one
            randomPoint = Random.Range(0, patrolPoints.Length);
        }

    }

    // once the boss stops playing his walk animation
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}

}