using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float movementSpeed = 3.5f;
    private float gravity = -9.8f;
    private CharacterController controller;

    void Start() {
        controller = GetComponent<CharacterController>();
    }

    void Update() {
        PlayerMovement();
    }

    void PlayerMovement() {
        Vector3 directions = new Vector3(Input.GetAxis("LeftJoystickHorizontal"), 0, Input.GetAxis("LeftJoystickVertical"));
        Vector3 velocity = directions * movementSpeed;
        velocity = Camera.main.transform.TransformDirection(velocity);
        velocity.y += gravity;
        controller.Move(velocity * Time.deltaTime);
    }
}
