using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class which represents a player weapon
 */
public class Weapon : MonoBehaviour {

    /* Origin position for instantiated shots */
    public Transform shotSpawnPosition;
    /* Normal ammo prefab */
    public GameObject normalAmmoPrefab;
    /* Special ammo prefab */
    public GameObject specialAmmoPrefab;
    /* Selected kind of ammo */
    private GameObject selectedAmmo;
    /* Fire rate of the weapon */
    public float fireRate;
    /* Needed for cadency control */
    private float nextFire;
    /* Reference to player controller */
    private PlayerController playerController;

    /*
     * Initialization method
     */
    private void Start() {
        selectedAmmo = normalAmmoPrefab;
        IncreaseDamage.OnIncreaseDamageCollect += SelectAmmo;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    /*
     * Shooting method
     */
    private void Update() {

        Vector3 lineOrigin = shotSpawnPosition.position;
        Debug.DrawRay(lineOrigin, Camera.main.transform.forward * 20, Color.green);

        if (InputController.GetButton(InputController.GetPS4ButtonName("R1")) && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(selectedAmmo, shotSpawnPosition.position, Camera.main.transform.rotation);
            StartCoroutine(playerController.NewNoise(1, 3));
        }
    }

    /*
     * Selects the ammo for the weapon
     */
    private void SelectAmmo(bool specialAmmoEnabled) {
        selectedAmmo = (specialAmmoEnabled) ? specialAmmoPrefab : normalAmmoPrefab;
    }
}
