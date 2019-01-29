using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class representing the shield consumable
 */
public class Shield : Consumable {

    /* Delegate for shield collect events */
    public delegate void ShieldCollectEventHandler(int shield);
    /* Shield collect event */
    public static event ShieldCollectEventHandler OnShieldCollect;
    /* Shield to add when a shield consumable is collected */
    public int shieldToAdd = 25;

    /*
     * Actions to take when the consumable effect starts
     */
    public override void Activate() {
        OnShieldCollect?.Invoke(shieldToAdd);
    }

    /*
     * Actions to take when the consumable effect finishes
     */
    public override void Deactivate() { }
}
