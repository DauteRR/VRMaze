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
    ATTACKING,
    DEAD
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

    /* Health of the enemy */
    [SerializeField]
    public int health;
    /* State of the enemy */
    private EnemyState currentState;

    /* Distance for stopping the agent when reachs its destination*/
    private const float DESTINATION_EPSILON = 1.5f;

    /* Maximum distance for the wandering */
    private float wanderRadius = 25;
    /* Amount of time for idle state after wandering */
    private float idleTime = 3;
    /* Timestamp after the enemy stops walking */
    private float stopTimeStamp;

    /* Delegate for enemy death events */
    public delegate void EnemyDeathEventHandler(GameObject enemy);
    /* Enemy death event */
    public static event EnemyDeathEventHandler OnEnemyDeath;

    /*
     * Initialization method
     */
    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        animatorController = GetComponent<Animator>();
        visionSystem = GetComponent<VisionSystem>();
        hearingSystem = GetComponent<HearingSystem>();
        currentState = EnemyState.IDLE;

        if (visionSystem != null) {
            StartCoroutine(visionSystem.FindTargets());
        }

        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        // Ignore collisions between noise enemy collider and player body collider
        Physics.IgnoreCollision(playerGameObject.GetComponent<BoxCollider>(), GetComponent<SphereCollider>());
    }

    /*
     * Changes the state of the enemy according to conditions
     */
    private void Update() {

        CheckHealth();
        if (currentState == EnemyState.DEAD)
            return;

        CheckTargetDetection();
        if (!agent.isStopped && Vector3.Distance(agent.destination, agent.transform.position) < DESTINATION_EPSILON) {
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
     * Checks the health of the enemy, if it is less or equal
     * to 0 the enemy state changes to DEAD
     */
    private void CheckHealth() {
        if (health <= 0) {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            OnStateChange(EnemyState.DEAD);
            StartCoroutine(DestroyAfter(5));
        }
    }

    /*
     * Destroyes the game object after a few seconds and notifies the
     * subscribers of the `onEnemyDeath` event
     */
    private IEnumerator DestroyAfter(float seconds) {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);

        OnEnemyDeath?.Invoke(gameObject);
    }

    /*
     * Orders the enemy to wander through the maze
     */
    private void Wander() {
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

    /*
     * Inflicts the given amount of damage to the enemy
     * @param amountOfDamage
     */
    public void InflictDamage(int amountOfDamage) {
        health -= amountOfDamage;
        health = Mathf.Clamp(health, 0, 100);
    }

    /*
    private void OnTriggerEnter(Collider collider) {
        if (currentState == EnemyState.ATTACKING &&
            collider.CompareTag(("Player")) &&
            typeof(BoxCollider) == collider.GetType()
        ) {
            collider.gameObject.GetComponent<PlayerController>().InflictDamage(50);
        }
    }
    */
}
