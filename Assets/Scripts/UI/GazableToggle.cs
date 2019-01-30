using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Class which represents an user interface
 * gazable toggle
 */
public class GazableToggle : Gazable {

    /*
     * Actions to take when the player
     * clicks the button
     */
    public override void OnPointerClick() {
        InputController.isOnMobile = GetComponent<Toggle>().isOn;
    }
}
