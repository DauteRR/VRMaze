using System;
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
        isMoving = (directions != Vector3.zero);
        noiseCollider.radius = (isMoving) ? 5 : 1;
        noiseCollider.radius += (lantern.enabled) ? 1 : 0;
        Vector3 velocity = directions * movementSpeed;
        velocity = Camera.main.transform.TransformDirection(velocity);
        velocity.y += gravity;
        controller.Move(velocity * Time.deltaTime);
    }

    /*
     * Toggles the state of the lantern
     */
    void ToggleLantern() {
        lantern.enabled = !lantern.enabled;
        noiseCollider.radius = 2;
    }
}
