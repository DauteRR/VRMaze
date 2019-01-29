using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class which represents a weapon shot
 */
public class Shot : MonoBehaviour
{
    /* Direction of the shot */
    private Vector3 shotDirection;
    /* Amount of seconds that the shot will be kept instantiated */
    private float shotLifeTime = 3;
    /* Velocity of the shot */
    public float shotSpeed;
    /* Damage of the shot */
    public int damage;
    /* Collision prefab */
    public GameObject collisionPrefab;

    /*
     * Initialization method
     */
    private void Start() {
        shotDirection = Camera.main.transform.forward;
        Destroy(gameObject, shotLifeTime);
    }

    /*
     * Shot movement
     */
    private void Update () {
        transform.position += shotDirection * Time.deltaTime * shotSpeed;
    }

    /*
     * Collision detection
     */
    private void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag(("Maze"))) {
            GameObject collision = Instantiate(collisionPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(collision, 3);
        }

        if (collider.CompareTag(("Enemy")) && typeof(BoxCollider) == collider.GetType()) {
            GameObject collision = Instantiate(collisionPrefab, gameObject.transform.position, Quaternion.identity);
            Instantiate(collisionPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(collision, 3);
            collider.gameObject.GetComponent<EnemyController>().InflictDamage(damage);
        }
    }

}
