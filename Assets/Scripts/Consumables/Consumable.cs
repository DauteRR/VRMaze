using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Abstract class representing a game consumable
 */
public abstract class Consumable : Gazable {

    /* Consumable effect duration (seconds) */
    public float duration;
    /* Tells if the consumable has been picked up */
    [HideInInspector]
    public bool pickedUp;

    /* Delegate for consumable pick events */
    public delegate void ConsumablePickedEventHandler(GameObject consumable);
    /* Consumable pick event */
    public static event ConsumablePickedEventHandler OnConsumablePick;

    /*
     * Corutine representing the lifecycle
     * of the consumable effect
     */
    public IEnumerator ConsumableEffect() {
        Activate();
        yield return new WaitForSeconds(duration);
        Deactivate();
        Destroy(gameObject);
    }

    /* 
     * When the pointer is clicked on the consumable,
     * starts the consumable effect
     */
    public override void OnPointerClick() {
        pickedUp = true;
        OnConsumablePick?.Invoke(gameObject);
        StartCoroutine(ConsumableEffect());
    }

    /*
     * Actions to take when the consumable effect starts
     */
    public abstract void Activate();

    /*
     * Actions to take when the consumable effect finishes
     */
    public abstract void Deactivate();
}
