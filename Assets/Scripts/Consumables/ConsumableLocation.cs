using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableLocation : MonoBehaviour {

    /* Vertical gap between the consumable and the consumable location */
    public float verticalOffset;
    /* Consumables prefabs needed for the instantiate */
    public GameObject[] consumablesPrefabs;
    /* Current consumable ar consumable location */
    private Consumable currentConsumable;

    /* Minimum amount of time required to generate a new consumable at this consumable location */
    public float minWaitForNewConsumable;
    /* Maximum amount of time required to generate a new consumable at this consumable location */
    public float maxWaitForNewConsumable;

    /*
     * Initialization method
     */
    private void Awake() {
        Consumable.OnConsumablePick += OnConsumablePick;
        GenerateNewConsumable();
    }

    /*
     * Callback to respond to a consumable pick event
     */
    private void OnConsumablePick(GameObject consumable) {
        if (!currentConsumable.pickedUp) {
            return;
        }
        // TODO add consumable to player active consumables
        consumable.GetComponent<MeshRenderer>().enabled = false;
        consumable.GetComponent<Collider>().enabled = false;
        GenerateNewConsumableDelayed();
    }

    /*
     * Generates a new consumable after a random period of time
     */
    private void GenerateNewConsumableDelayed() {
        float timeToWait = Random.Range(
            minWaitForNewConsumable,
            maxWaitForNewConsumable
        );
        Invoke("GenerateNewConsumable", timeToWait);
    }

    /*
     * Generates a new consumable
     */
    private void GenerateNewConsumable() {
        GameObject selectedConsumable = consumablesPrefabs[Random.Range(0, consumablesPrefabs.Length)];
        Vector3 consumablePosition = transform.position + Vector3.up * verticalOffset;
        currentConsumable = Instantiate(
            selectedConsumable, 
            consumablePosition, 
            selectedConsumable.transform.rotation
        ).GetComponent<Consumable>();
    }
}
