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
        StartCoroutine(RespawnEnemy());
    }

    /*
     * Coroutine to respawn an enemy in one of the given respawn points
     */
    private IEnumerator RespawnEnemy() {
        while (true) {
            yield return new WaitForSeconds(
                Random.Range(
                    minWaitForRespawnEnemy,
                    maxWaitForRespawnEnemy
                )
            );
            if (instantiatedEnemies.Count < maxEnemies) {
                instantiatedEnemies.Add(Instantiate(
                    enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)],
                    gameObject.transform.position,
                    Quaternion.identity
                ));
            }
        }
    }
}
