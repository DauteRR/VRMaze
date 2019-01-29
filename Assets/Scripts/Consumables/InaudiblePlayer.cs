using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class representing the inaudible player consumable
 */
public class InaudiblePlayer : Consumable {

    /* Delegate for inaudible player collect events */
    public delegate void InaudiblePlayerCollectEventHandler(bool inaudiblePlayer);
    /* Inaudible player collect event */
    public static event InaudiblePlayerCollectEventHandler OnInaudiblePlayerCollect;

    /*
     * Actions to take when the consumable effect starts
     */
    public override void Activate() {
        OnInaudiblePlayerCollect?.Invoke(true);
    }

    /*
     * Actions to take when the consumable effect finishes
     */
    public override void Deactivate() {
        OnInaudiblePlayerCollect?.Invoke(false);
    }
}