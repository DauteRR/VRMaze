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
    private const float DISTANCE_EPSILON = 1.5f;
    /* Maximum distance for the wandering */
    private float wanderRadius = 25;
    /* Amount of time for idle state after wandering */
    private float idleTime = 3;
    /* Timestamp after the enemy stops walking */
    private float stopTimeStamp;

    /*
     * Initialization method
     */
    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        animatorController = GetComponent<Animator>();
        visionSystem = GetComponent<VisionSystem>();
        hearingSystem = GetComponent<HearingSystem>();
        currentState = EnemyState.IDLE;
        //agent.height = 0.5f;
        agent.baseOffset = 0;


        if (visionSystem != null) {
            StartCoroutine(visionSystem.FindTargets());
        }
    }

    /*
     * 
     */
    private void Update() {
        CheckTargetDetection();
        if (!agent.isStopped && Vector3.Distance(agent.destination, agent.transform.position) < DISTANCE_EPSILON) {
            stopTimeStamp = Time.time;
            agent.isStopped = true;
            agent.velocity = Vector3.zero;

            OnStateChange(EnemyState.IDLE);

            if ((visionSystem != null && visionSystem.IsTargetInFOV()) ||
                (hearingSystem != null && hearingSystem.targetDetected)) {
                OnStateChange(EnemyState.ATTACKING);
            }
        }
        if (agent.isStopped && Time.time - stopTimeStamp > idleTime) {
            Wander();
        }
    }


    /*
     * Orders the enemy to wander through the maze
     */
    void Wander() {
        Vector3 nextPosition = Random.insideUnitSphere * wanderRadius;
        nextPosition.y = 0;
        agent.SetDestination(agent.transform.position + nextPosition);
        agent.isStopped = false;
        OnStateChange(EnemyState.WALKING);
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

        if (currentState == EnemyState.ATTACKING)
            return;

        // Vision system checking
        if (visionSystem != null &&
            visionSystem.IsTargetInFOV()
        ) {
            agent.SetDestination(visionSystem.visibleTargets[0].position);
            agent.isStopped = false;
            OnStateChange(EnemyState.WALKING);
        }

        // Hearing system checking
        if (hearingSystem != null &&
            hearingSystem.targetDetected
        ) {
            agent.SetDestination(hearingSystem.noisePosition);
            agent.isStopped = false;
            OnStateChange(EnemyState.WALKING);
        }
    }
}
