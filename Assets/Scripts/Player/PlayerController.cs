using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Class containing all logic related to the player 
 */
public class PlayerController : MonoBehaviour {

    /* Movement speed */
    public float movementSpeed = 3.5f;
    /* Gravity constant */
    private const float gravity = -9.8f;
    /* Character controller component */
    private CharacterController controller;

    /*
     * Method to retrieve the character controller component.
     */
    void Start() {
        controller = GetComponent<CharacterController>();
    }

    void Update() {

        if (Input.GetButtonUp("ToggleLantern")) {
            ToggleLantern();
        }

        PlayerMovement();
    }

    /*
     * Method to control the player movement
     */
    void PlayerMovement() {
        Vector3 directions = new Vector3(Input.GetAxis("LeftJoystickHorizontal"), 0, Input.GetAxis("LeftJoystickVertical"));
        Vector3 velocity = directions * movementSpeed;
        velocity = Camera.main.transform.TransformDirection(velocity);
        velocity.y += gravity;
        controller.Move(velocity * Time.deltaTime);
    }

    /*
     * Toggles the state of the lantern
     */
    void ToggleLantern() {
        Light lantern = GetComponentInChildren<Light>();
        lantern.enabled = !lantern.enabled;
    }
}
