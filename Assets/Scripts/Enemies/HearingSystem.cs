using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class which wraps the enemies hearing system logic
 */
public class HearingSystem : MonoBehaviour
{
    /* Tells if the enemy is hearing a noise */
    [HideInInspector]
    public bool targetDetected;
    /* Noise origin */
    [HideInInspector]
    public Vector3 noisePosition;

    /*
     * Needed to establish if the enemy is hearing
     * a "player noise"
     */
    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.CompareTag("Player")) {
            targetDetected = true;
        }
    }

    /*
     * Needed to establish if the enemy keeps 
     * hearing the "player noise"
     */
    private void OnTriggerStay(Collider collider) {
        if (collider.gameObject.CompareTag("Player")) {
            noisePosition = collider.gameObject.transform.position;
            noisePosition.y = 0;
        }
    }

    /*
     * Needed to establish if the enemy stops 
     * hearing the "player noise"
     */
    private void OnTriggerExit(Collider collider) {
        if (collider.gameObject.CompareTag("Player")) {
            targetDetected = false;
        }
    }
}
