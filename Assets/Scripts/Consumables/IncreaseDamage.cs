using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class representing the increase damage consumable
 */
public class IncreaseDamage : Consumable {

    /* Delegate for increase damage collect events */
    public delegate void IncreaseDamageCollectEventHandler(bool increaseDamage);
    /* Increase damage collect event */
    public static event IncreaseDamageCollectEventHandler OnIncreaseDamageCollect;

    /*
     * Actions to take when the consumable effect starts
     */
    public override void Activate() {
        OnIncreaseDamageCollect?.Invoke(true);
    }

    /*
     * Actions to take when the consumable effect finishes
     */
    public override void Deactivate() {
        OnIncreaseDamageCollect?.Invoke(false);
    }
}