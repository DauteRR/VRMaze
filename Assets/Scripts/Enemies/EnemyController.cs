using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Enumeration of possible enemy states
 */
enum EnemyState {
    WALKING,
    IDLE,
    ATTACKING
}

/*
 * Class which controls the enemies behaviour
 */
public class EnemyController : MonoBehaviour {

    /* Navigation mesh agent */
    private NavMeshAgent agent;
    /* Animation controller */
    private Animator animatorController;
    /* Vision system (optional)*/
    private VisionSystem visionSystem;
    /* Hearing system (optional)*/
    private HearingSystem hearingSystem;
    /* State of the enemy */
    private EnemyState currentState;
    /* Distance for stopping the agent when reachs its destination*/
    private const float DISTANCE_EPSILON = 2f;

    /*
     * Initialization method
     */
    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        animatorController = GetComponent<Animator>();
        visionSystem = GetComponent<VisionSystem>();
        hearingSystem = GetComponent<HearingSystem>();
        currentState = EnemyState.IDLE;

        if (visionSystem != null) {
            StartCoroutine(visionSystem.FindTargets());
        }
    }

    /*
     * 
     */
    private void Update() {
        CheckTargetDetection();

        if (Vector3.Distance(agent.destination, agent.transform.position) < DISTANCE_EPSILON) {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            if (currentState != EnemyState.IDLE)
                OnStateChange(EnemyState.IDLE);
        }
    }

    /*
     * Changes the state of the enemy and its animation
     */
    private void OnStateChange(EnemyState newState) {
        animatorController.SetBool(currentState.ToString(), false);
        animatorController.SetBool(newState.ToString(), true);
        currentState = newState;
    }

    /*
     * Method which establish if the enemy detects the player,
     * if so, the enemy follow the player by the navigation mesh
     */
    private void CheckTargetDetection() {
        // Vision system checking
        if (visionSystem != null &&
            visionSystem.IsTargetInFOV() &&
            agent.isStopped
        ) {
            agent.SetDestination(visionSystem.visibleTargets[0].position);
            agent.isStopped = false;
            if (currentState != EnemyState.WALKING)
                OnStateChange(EnemyState.WALKING);
        }

        // Hearing system checking
        if (hearingSystem != null && 
            hearingSystem.targetDetected &&
            agent.isStopped
        ) {
            agent.SetDestination(hearingSystem.noisePosition);
            agent.isStopped = false;
            if (currentState != EnemyState.WALKING)
                OnStateChange(EnemyState.WALKING);
        } 
    }
}
