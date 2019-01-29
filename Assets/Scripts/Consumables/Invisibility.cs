using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class representing the invisibility consumable
 */
public class Invisibility : Consumable {

    /*
     * Actions to take when the consumable effect starts
     */
    public override void Activate() {
        GameObject.FindGameObjectWithTag("Player").layer = LayerMask.NameToLayer("Enemies");
    }

    /*
     * Actions to take when the consumable effect finishes
     */
    public override void Deactivate() {
        GameObject.FindGameObjectWithTag("Player").layer = LayerMask.NameToLayer("Player");
    }
}
