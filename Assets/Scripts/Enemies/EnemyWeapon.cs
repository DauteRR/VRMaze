using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class which represents an enemy weapon
 */
public class EnemyWeapon : MonoBehaviour {

    /* Damage that the weapon inflicts to the player */
    public int damage;

    /*
     * Collision detection
     */
    private void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag(("Player")) && typeof(BoxCollider) == collider.GetType()) {
            collider.GetComponent<PlayerController>().InflictDamage(damage);
        }
    }
}
