using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Class containing all logic related to the player 
 */
public class PlayerController : MonoBehaviour {

    /* Movement speed */
    public float movementSpeed = 3.5f;
    /* Rotation speed */
    public float rotationSpeed = 1.5f;
    /* Gravity constant */
    private const float gravity = -9.8f;
    /* Character controller component */
    private CharacterController controller;
    /* Lantern of the player */
    private Light lantern;
    /* Establish if the player is moving */
    private bool isMoving;
    /* Represents the noises of the player */
    private SphereCollider noiseCollider;

    /*
     * Method to retrieve the character components.
     */
    void Start() {
        controller = GetComponent<CharacterController>();
        noiseCollider = GetComponent<SphereCollider>();
        lantern = GetComponentInChildren<Light>();
    }

    void Update() {

        if (InputController.GetButtonUp(InputController.GetPS4ButtonName("L3"))) {
            ToggleLantern();
        }
        PlayerMovement();
    }

    /*
     * Method to control the player movement
     */
    void PlayerMovement() {
        // Movement
        Vector3 directions = new Vector3(
            InputController.GetAxis("LeftJoystickHorizontal"), 
            0, 
            InputController.GetAxis("LeftJoystickVertical"
        ));
        isMoving = (directions != Vector3.zero);
        noiseCollider.radius = (isMoving) ? 5 : 1;
        noiseCollider.radius += (lantern.enabled) ? 1 : 0;
        Vector3 velocity = directions * movementSpeed;
        velocity = Camera.main.transform.TransformDirection(velocity);
        velocity.y += gravity;
        controller.Move(velocity * Time.deltaTime);

        // Rotation
        if (Input.GetButton(InputController.GetPS4ButtonName("R2"))) {
            transform.Rotate(Vector3.up * rotationSpeed);
        }
        if (Input.GetButton(InputController.GetPS4ButtonName("L2"))) {
            transform.Rotate(Vector3.down * rotationSpeed);
        }

    }

    /*
     * Toggles the state of the lantern
     */
    void ToggleLantern() {
        lantern.enabled = !lantern.enabled;
        noiseCollider.radius = 2;
    }

    public void Foo() {
        Debug.Log("Foo");
    }
}
