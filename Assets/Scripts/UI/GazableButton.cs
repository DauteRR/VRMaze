using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Class which represents an user interface
 * gazable button
 */
public class GazableButton : Gazable {

    /*
     * Actions to take when the player
     * clicks the button
     */
    public override void OnPointerClick() {
        //Trigger click
        //GetComponent<Button>().onClick.Invoke();
    }
}
