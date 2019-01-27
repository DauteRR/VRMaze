using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class containing the enemy respawn logic
 */
public class EnemyRespawn : MonoBehaviour
{

    /* Enemies prefabs needed for the instantiate method during the respawn */
    public GameObject[] enemiesPrefabs;
    /* Minimum amount of time to see a new enemy respawning */
    public float minWaitForRespawnEnemy;
    /* Maximum amount of time to see a new enemy respawning */
    public float maxWaitForRespawnEnemy;
    /* Maximum amount of instantiated enemies */
    public int maxEnemies = 1;
    /* Instantiated enemies */
    private List<GameObject> instantiatedEnemies;

    /*
     * Initialization method
     */
    private void Start() {
        instantiatedEnemies = new List<GameObject>();
        EnemyController.onEnemyDeath += onEnemyDeath;
    }

    /*
     * Callback to respond to an enemy death event
     */
    private void onEnemyDeath(GameObject enemy) {
        instantiatedEnemies.Remove(enemy); 
        RespawnEnemyDelayed();
    }

    /*
     * Respawns an enemy after a random period of time
     */
    private void RespawnEnemyDelayed() {
        var timeToWait = Random.Range(
            minWaitForRespawnEnemy,
            maxWaitForRespawnEnemy
        );
        Invoke("RespawnEnemy", timeToWait);
    }

    /*
     * Respawns a new random enemy if there are not enough
     */
    private void RespawnEnemy() {
        if (instantiatedEnemies.Count >= maxEnemies) {
            return;
        }

        var enemyPrefab = enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)];
        var newEnemy = Instantiate(
            enemyPrefab,
            gameObject.transform.position,
            Quaternion.identity
        )
        instantiatedEnemies.Add(newEnemy);
    }
}
