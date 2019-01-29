using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class representing the hint consumable
 */
public class Hint : Consumable {

    /* Hint prefab needed for the instantiate method */
    public GameObject hintPrefab;
    /* Instantiated hint game object */
    private GameObject instantiatedHint;

    /*
     * Actions to take when the consumable effect starts
     */
    public override void Activate() {
        instantiatedHint = Instantiate(
            hintPrefab, 
            GameObject.FindGameObjectWithTag("FinalPoint").transform.position,
            hintPrefab.transform.rotation
        );
    }

    /*
     * Actions to take when the consumable effect finishes
     */
    public override void Deactivate() {
        Destroy(instantiatedHint);
    }
}
