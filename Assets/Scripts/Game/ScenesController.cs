using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Scenes controller
 */
public class ScenesController : MonoBehaviour {

    /* Rows of the current maze */
    [HideInInspector]
    public int currentMazeRows;
    /* Columns of the current maze */
    [HideInInspector]
    public int currentMazeColumns;
    /* Quantity of defeated enemies through all solved mazes */
    [HideInInspector]
    public int totalAmountOfEnemiesDefeated;
    /* Quantity of solved mazes during gameplay */
    [HideInInspector]
    public int totalAmountOfSolvedMazes;
    /* Timer to measure while the player is solving a maze */
    [HideInInspector]
    public float timer;
    /* Tells if the player is currently solving a maze */
    [HideInInspector]
    public bool playerSolvingMaze;
    /* Tells if the player solved the last maze */
    private bool lastMazeWasSolved;

    /*
     * Initialization method
     */
    private void Awake() {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnMainMenuSceneLoaded;
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }

    /*
     * Starts the gameplay
     */
    public void StartPlaying() {
        RestoreValues();
        OnNewMaze();
    }

    /*
     * Starts a new maze
     */
    public void NextMaze() {
        OnNewMaze();
    }

    /* 
     * Creates a new maze
     */
    private void OnNewMaze() {
        SceneManager.LoadScene("MazeScene", LoadSceneMode.Single);
        playerSolvingMaze = true;
    }

    /*
     * Called a mazes finishes
     */
    public void OnFinishedMaze(bool solved) {
        playerSolvingMaze = false;
        currentMazeRows += 2;
        currentMazeColumns += 2;
        if (solved)
            totalAmountOfSolvedMazes += 1;
        totalAmountOfEnemiesDefeated += 
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().defeatedEnemies;
        lastMazeWasSolved = solved;

        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }

    /*
     * Called when the player starts again after being defeated
     */
    private void RestoreValues() {
        currentMazeRows = 9;
        currentMazeColumns = 9;
        totalAmountOfSolvedMazes = 0;
        totalAmountOfEnemiesDefeated = 0;
        timer = 0;
        GameObject.FindGameObjectWithTag("ResultsUI").GetComponent<ResultsUIController>().RestoreButtonsText();
    }

    /*
     * Updates the timer if the player is solving a maze
     */
    private void Update() {
        if (playerSolvingMaze) {
            timer += Time.deltaTime;
        }
    }

    /*
     * Finishes the game
     */
    public void Exit() {
        Application.Quit();
    }

    /*
     * Callback to respond to an main menu scene load event
     */
    private void OnMainMenuSceneLoaded(Scene scene, LoadSceneMode mode) {

        if (scene.name != "MainMenuScene")
            return;
        GameObject.FindGameObjectWithTag("MainMenuUI").GetComponent<MainMenuUIController>().SetContinuePlaying(lastMazeWasSolved);
        GameObject.FindGameObjectWithTag("ResultsUI").
            GetComponent<ResultsUIController>().UpdateButtonsTexts(
                timer, 
                totalAmountOfEnemiesDefeated, 
                totalAmountOfSolvedMazes
            );
    }
}
