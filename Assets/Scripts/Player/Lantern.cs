using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour {

    /* Remaining battery time in seconds */
    public float batteryTime = 20;
    /* Light component representing a lantern */
    private Light lantern;
    /* Tells if the lantern is on */
    public bool isOn;

    /*
     * Initialization method
     */
    private void Awake() {
        lantern = GetComponent<Light>();
        Battery.OnBatteryCollect += AddBatteryTime;
    }

    /*
     * Checks the remaining battery time
     * of the lantern
     */
    private void Update() {
        if (isOn) {
            batteryTime -= Time.deltaTime;

            if (batteryTime <= 2 && batteryTime > 0) {
                StartCoroutine(LastSecondsOfBattery());
            }

            if (batteryTime <= 0) {
                lantern.intensity = 0;
                isOn = false;
                batteryTime = 0;
            }
            Debug.Log(batteryTime);
        }
    }

    /*
     * Turns on and off the lantern quickly during the
     * last seconds of battery
     */
    private IEnumerator LastSecondsOfBattery() {
        while(batteryTime > 0) {
            lantern.intensity = 5;
            yield return new WaitForSeconds(Random.Range(0.5f, 0.8f));
            lantern.intensity = 0;
            yield return new WaitForSeconds(Random.Range(0.5f, 0.8f));
        }
    }

    /*
     * Toggles the state of the lantern
     */
    public void ToggleLantern() {
        isOn = !isOn;
        lantern.intensity = (isOn) ? 5 : 0;
    }

    /*
     * Adds the given amount of battery time
     */
    private void AddBatteryTime(float time) {
        batteryTime += time;
    }
}
