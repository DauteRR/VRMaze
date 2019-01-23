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

    private void Start() {
        this.agent = GetComponent<NavMeshAgent>();
        this.animatorController = GetComponent<Animator>();
        this.visionSystem = GetComponent<VisionSystem>();
        this.hearingSystem = GetComponent<HearingSystem>();
        this.currentState = EnemyState.IDLE;

        if (visionSystem != null) {
            StartCoroutine(this.visionSystem.FindTargets());
        }
    }

    private void Update() {
        CheckTargetDetection();

        if (Vector3.Distance(agent.destination, agent.transform.position) < DISTANCE_EPSILON)
            agent.isStopped = true;
        if (this.agent.isStopped && currentState != EnemyState.IDLE) {
            OnStateChange(EnemyState.IDLE);
        }
    }

    private void OnStateChange(EnemyState newState) {
        animatorController.SetBool(currentState.ToString(), false);
        animatorController.SetBool(newState.ToString(), true);
        this.currentState = newState;
    }

    private void CheckTargetDetection() {

        // Vision system checking
        if (visionSystem != null &&
            visionSystem.IsTargetInFOV()
        ) {
            agent.SetDestination(visionSystem.visibleTargets[0].position);
            agent.isStopped = false;
            if (currentState != EnemyState.WALKING)
                OnStateChange(EnemyState.WALKING);
        }
    }
}
