using System;
using UnityEngine;

/*
 * Controls the state of the game. 
 */
[RequireComponent(typeof(MazeConstructor))]
public class GameController : MonoBehaviour {

    /* Maze constructor */
    private MazeConstructor generator;
    /* Height of the maze */
    public int mazeRows = 15;
    /* Width of the maze */
    public int mazeColumns = 15;

    /**
     * Method which invokes maze constructor generation method.
     */
    void Start() {
        generator = GetComponent<MazeConstructor>();
        generator.GenerateNewMaze(mazeRows, mazeColumns);
    }
}