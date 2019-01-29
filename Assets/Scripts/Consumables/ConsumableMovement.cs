using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableMovement : MonoBehaviour {

    /* Rotation velocity */
    private float rotationSpeed = 50;
    /* Movement velocity */
    private float movementSpeed = 0.3f;
    /* Maximum vertical offset */
    private float verticalOffset = 0.25f;
    /* Tells if the consumable is going up */
    private bool isGoingUp;
    /* Initial y coord of the consumable */
    private float initialY;

    /*
     * Initialization method
     */
    private void Start() {
        initialY = transform.position.y;
        isGoingUp = true;
    }

    /*
     * Method which contains the movement logic
     */
    private void Update() {
        Vector3 rotation = (transform.rotation.x != 0) ? Vector3.forward : Vector3.up;
        transform.Rotate(rotation * Time.deltaTime * rotationSpeed);

        if (transform.position.y >= initialY + verticalOffset) {
            isGoingUp = false;
        }
        if (transform.position.y <= initialY - verticalOffset) {
            isGoingUp = true;
        }

        Vector3 movement = (isGoingUp) ? Vector3.up : Vector3.down;
        transform.position += movement * Time.deltaTime * movementSpeed;
    }
}
