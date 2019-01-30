using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Class which wraps the logic under the results user interface
 */
public class ResultsUIController : MonoBehaviour {

    /* Time button game object */
    public GameObject timeButtonGameObject;
    /* Defeated enemies button game object */
    public GameObject defeatedEnemiesButtonGameObject;
    /* Solved mazes button game object */
    public GameObject solvedMazesButtonGameObject;

    /*
     * Updates buttons texts according to given values
     */
    public void UpdateButtonsTexts(float time, int defeatedEnemies, int solvedMazes) {
        int seconds = (int)(time % 60);
        int minutes = (int)Math.Floor(time / 60);
        string sec = seconds + "";
        string min = minutes + "";
        if (minutes < 10) {
            min = "0" + min;
        }
        if (seconds < 10) {
            sec = "0" + sec;
        }
        timeButtonGameObject.GetComponentInChildren<Text>().text = "Time: " + min + ":" + sec;
        defeatedEnemiesButtonGameObject.GetComponentInChildren<Text>().text = "Defeated enemies: " + defeatedEnemies;
        solvedMazesButtonGameObject.GetComponentInChildren<Text>().text = "Solved mazes: " + solvedMazes;
    }

    /*
     * Restore buttons texts
     */
    public void RestoreButtonsText() {
        UpdateButtonsTexts(0, 0, 0);
    }

}
