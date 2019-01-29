using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class representing the health consumable
 */
public class Health : Consumable {

    /* Delegate for health collect events */
    public delegate void HealthCollectEventHandler(int health);
    /* Health collect event */
    public static event HealthCollectEventHandler OnHealthCollect;
    /* Health to add when a health consumable is collected */
    public int healthToAdd = 25;

    /*
     * Actions to take when the consumable effect starts
     */
    public override void Activate() {
        OnHealthCollect?.Invoke(healthToAdd);
    }

    /*
     * Actions to take when the consumable effect finishes
     */
    public override void Deactivate() { }
}
