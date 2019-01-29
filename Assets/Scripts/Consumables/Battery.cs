using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class representing the battery consumable
 */
public class Battery : Consumable {

    /* Delegate for battery collect events */
    public delegate void BatteryCollectEventHandler(float time);
    /* Battery collect event */
    public static event BatteryCollectEventHandler OnBatteryCollect;
    /* Time to add when a battery consumable is collected */
    public float timeToAdd = 20;

    /*
     * Actions to take when the consumable effect starts
     */
    public override void Activate() {
        OnBatteryCollect?.Invoke(timeToAdd);
    }

    /*
     * Actions to take when the consumable effect finishes
     */
    public override void Deactivate() { }
}
