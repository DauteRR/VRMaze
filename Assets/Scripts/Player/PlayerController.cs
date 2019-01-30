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
    /* Rotation speed */
    public float rotationSpeed = 1.5f;
    /* Gravity constant */
    private float gravity = 9.8f;
    /* Character controller component */
    private CharacterController controller;
    /* Establish if the player is moving */
    private bool isMoving;


    /* Represents the noises of the player */
    private SphereCollider noiseCollider;
    /* Tells if the player makes noises or not */
    private bool inaudiblePlayer;

    private float currentNoise;

    /* Lantern of the player */
    private Lantern lantern;

    /* Health of the player */
    [Range(0, 100)]
    [SerializeField]
    private int health;
    /* Shield of the player */
    [Range(0, 100)]
    [SerializeField]
    private int shield;

    /* User interface health text */
    public Text healthText;
    /* User interface shield text */
    public Text shieldText;

    /* Amount of defeated enemies */
    public int defeatedEnemies;


    /*
     * Method to retrieve the character components.
     */
    private void Start() {
        controller = GetComponent<CharacterController>();
        noiseCollider = GetComponent<SphereCollider>();
        lantern = GetComponentInChildren<Lantern>();

        Health.OnHealthCollect += AddHealth;
        Shield.OnShieldCollect += AddShield;
        InaudiblePlayer.OnInaudiblePlayerCollect += SetInaudiblePlayer;

        EnemyController.OnEnemyDeath += (enemy) => defeatedEnemies++;
    }

    /*
     * Updates the player state every frame
     */
    private void Update() {

        if (InputController.GetButtonUp(InputController.GetPS4ButtonName("L3"))) {
            lantern.ToggleLantern();
        }
        PlayerMovement();

        if (health <= 0 || InputController.GetButtonUp(InputController.GetPS4ButtonName("PS"))) {
            ScenesController scenesController =
                GameObject.FindGameObjectWithTag("ScenesController").GetComponent<ScenesController>();
            scenesController.OnFinishedMaze(false);
        }
    }

    /*
     * Method to control the player movement
     */
    private void PlayerMovement() {
        // Movement
        Vector3 directions = new Vector3(
            InputController.GetAxis("LeftJoystickHorizontal"), 
            0, 
            InputController.GetAxis("LeftJoystickVertical"
        ));
        isMoving = (directions != Vector3.zero);
        Vector3 velocity = directions * movementSpeed;
        velocity = Camera.main.transform.TransformDirection(velocity);
        velocity.y = 0;
        velocity.y -= gravity;
        controller.Move(velocity * Time.deltaTime);

        // Noise
        noiseCollider.radius = (isMoving) ? 5 : 1;
        noiseCollider.radius += (lantern.isOn) ? 1 : 0;
        noiseCollider.radius += currentNoise;
        noiseCollider.radius = (inaudiblePlayer) ? 0 : noiseCollider.radius;

        // Rotation
        if (Input.GetButton(InputController.GetPS4ButtonName("R2"))) {
            transform.Rotate(Vector3.up * rotationSpeed);
        }
        if (Input.GetButton(InputController.GetPS4ButtonName("L2"))) {
            transform.Rotate(Vector3.down * rotationSpeed);
        }
    }

    /*
     * Corutine to register new noises
     */
    public IEnumerator NewNoise(float time, float intensity) {
        currentNoise += intensity;
        yield return new WaitForSeconds(time);
        currentNoise -= intensity;
    }

    /*
     * Setter method form inaudible player
     */
    private void SetInaudiblePlayer(bool inaudiblePlayer) {
        this.inaudiblePlayer = inaudiblePlayer;
    }

    /*
     * Adds the given amount of health
     * to the player health
     */
    private void AddHealth(int healthToAdd) {
        health += healthToAdd;
        health = Mathf.Clamp(health, 0, 100);
        healthText.text = health + "";
    }

    /*
     * Adds the given amount of shield
     * to the player shield
     */
    private void AddShield(int shieldToAdd) {
        shield += shieldToAdd;
        shield = Mathf.Clamp(shield, 0, 100);
        shieldText.text = shield + "";
    }

    /*
     * Inflicts the given amount of damage to the player
     * @param amountOfDamage
     */
    public void InflictDamage(int amountOfDamage) {
        GetComponent<AudioSource>().Play();
        shield -= amountOfDamage;
        if (shield < 0) {
            health += shield;
        }

        shield = Mathf.Clamp(shield, 0, 100);
        health = Mathf.Clamp(health, 0, 100);

        shieldText.text = shield + "";
        healthText.text = health + "";
    }
}