
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Class which wraps the logic under the main menu
 */
public class MainMenuUIController : MonoBehaviour {

    /* Start button game object of the main menu */
    public GameObject startButtonGameObject;
    /* Exit button game object of the main menu */
    public GameObject exitButtonGameObject;
    /* Next button game object of the main menu */
    public GameObject nextButtonGameObject;
    /* Needed to establish the controls */
    public GameObject mobileToggleGameObject;
    /* 
     * Tells if the player continues its game or starts a new one
     * when the start button is clicked
     */
    [HideInInspector]
    public bool continuePlaying = false;

    /*
     * Initialization method
     */
    private void Start() {
        ScenesController scenesController =
            GameObject.FindGameObjectWithTag("ScenesController").GetComponent<ScenesController>();
        startButtonGameObject.GetComponent<Button>().onClick.AddListener(scenesController.StartPlaying);
        exitButtonGameObject.GetComponent<Button>().onClick.AddListener(scenesController.Exit);
        nextButtonGameObject.GetComponent<Button>().onClick.AddListener(scenesController.NextMaze);
    }

    /*
     * Setter method for continuePlaying attribute.
     * Based on its value updates the main menu UI
     */
    public void SetContinuePlaying(bool continuePlaying) {
        this.continuePlaying = continuePlaying;

        ScenesController scenesController =
            GameObject.FindGameObjectWithTag("ScenesController").GetComponent<ScenesController>();

        startButtonGameObject.SetActive(!continuePlaying);
        nextButtonGameObject.SetActive(continuePlaying);
        mobileToggleGameObject.SetActive(!continuePlaying);
        Vector3 position = exitButtonGameObject.transform.position;
        position.x = (continuePlaying) ? 0 : 0.83f;
        exitButtonGameObject.transform.position = position;
    }
}
