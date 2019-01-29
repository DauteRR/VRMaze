using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Class which represents the lantern of the player
 */
public class Lantern : MonoBehaviour {

    /* Remaining battery time in seconds */
    private float batteryTime = 20;
    /* Light component representing a lantern */
    private Light lantern;
    /* Tells if the lantern is on */
    public bool isOn;
    /* User interface battery text */
    public Text batteryText;
    /* User interface lantern icon */
    public Image lanternImage;

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

            if (batteryTime < 0) {
                batteryTime = 0;
            }

            UpdateBatteryText();
        }

        lantern.intensity = (isOn && batteryTime > 0) ? 5 : 0;
    }

    /*
     * Updates the user interface element
     * battery text
     */
    private void UpdateBatteryText() {
        int seconds = (int)(batteryTime % 60); 
        int minutes = (int)Math.Floor(batteryTime / 60);
        string sec = seconds + "";
        string min = minutes + "";
        if (minutes < 10) {
            min = "0" + min;
        }
        if (seconds < 10) {
            sec = "0" + sec;
        }

        batteryText.text = min + ":" + sec;
    }

    /*
     * Turns on and off the lantern quickly during the
     * last seconds of battery
     */
    private IEnumerator LastSecondsOfBattery() {
        while(batteryTime > 0) {
            lantern.intensity = 5;
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 0.8f));
            lantern.intensity = 0;
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 0.8f));
        }
    }

    /*
     * Toggles the state of the lantern
     */
    public void ToggleLantern() {
        isOn = !isOn;
        lanternImage.enabled = isOn;
        batteryText.enabled = isOn;
    }

    /*
     * Adds the given amount of battery time
     */
    private void AddBatteryTime(float time) {
        batteryTime += time;
    }
}
