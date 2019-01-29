using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class which represents a weapon shot
 */
public class Shot : MonoBehaviour
{
    /* Direction of the shot */
    private Vector3 shotDirection;
    /* Velocity of the shot */
    public float shotSpeed;
    /* Damage of the shot */
    public float damage;

    /*
     * Initialization method
     */
    private void Start() {
        shotDirection = Camera.main.transform.forward;
    }

    /*
     * Shot movement
     */
    private void Update () {
        transform.position += shotDirection * Time.deltaTime * shotSpeed;
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("Hola");
    }
}
