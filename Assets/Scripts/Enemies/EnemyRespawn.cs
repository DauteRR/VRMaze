using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class containing the enemy respawn logic
 */
public class EnemyRespawn : Gazable {

    /* Enemies prefabs needed for the instantiate method during the respawn */
    public GameObject[] enemiesPrefabs;
    /* Minimum amount of time to see a new enemy respawning */
    public float minWaitForRespawnEnemy;
    /* Maximum amount of time to see a new enemy respawning */
    public float maxWaitForRespawnEnemy;
    /* Maximum amount of instantiated enemies */
    public int maxEnemies;
    /* Instantiated enemies */
    private List<GameObject> instantiatedEnemies;

    /* Tells if the respawn is active */
    private bool activeRespawn;
    /* Minimum amount of time to wait after the respawn is deactivated */
    public float minWaitForActivateRespawn;
    /* Maximum amount of time to wait after the respawn is deactivated */
    public float maxWaitForActivateRespawn;

    /*
     * Initialization method
     */
    private void Start() {
        instantiatedEnemies = new List<GameObject>();
        EnemyController.OnEnemyDeath += onEnemyDeath;
        activeRespawn = true;

        StartCoroutine(RespawnEnemies());
    }

    /*
     * Callback to respond to an enemy death event
     */
    private void onEnemyDeath(GameObject enemy) {
        instantiatedEnemies.Remove(enemy);
    }

    /*
     * Respawns enemies after a random period of time
     */
    private IEnumerator RespawnEnemies() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(
                minWaitForRespawnEnemy,
                maxWaitForRespawnEnemy
            ));
            RespawnEnemy();
        }
    }

    /*
     * Respawns a new random enemy if there are not enough
     */
    private void RespawnEnemy() {
        if (!activeRespawn || instantiatedEnemies.Count >= maxEnemies) {
            return;
        }

        GameObject enemyPrefab = enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)];
        GameObject newEnemy = Instantiate(
            enemyPrefab,
            gameObject.transform.position,
            Quaternion.identity
        );
        instantiatedEnemies.Add(newEnemy);
    }

    /* 
     * When the pointer is clicked on the respawn,
     * deactivates the respawn temporarily
     */
    public override void OnPointerClick() {
        StartCoroutine(TemporarilyDeactivateRespawn());
    }

    /*
     * Deactivates the respawn temporarily
     */
    private IEnumerator TemporarilyDeactivateRespawn() {
        activeRespawn = false;
        ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in particleSystems) {
            ps.Pause();
        }
        yield return new WaitForSeconds(Random.Range(
            minWaitForActivateRespawn,
            maxWaitForActivateRespawn
        ));
        activeRespawn = true;

        foreach (ParticleSystem ps in particleSystems) {
            ps.Play();
        }
    }
}
