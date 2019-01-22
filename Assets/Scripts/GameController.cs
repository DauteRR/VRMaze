using System;
using UnityEngine;

/*
 * Controls the state of the game. 
 */
[RequireComponent(typeof(MazeConstructor))]
[RequireComponent(typeof(SpawnSystem))]
public class GameController : MonoBehaviour {

    /* Maze constructor */
    private MazeConstructor mazeConstructor;
    /* Spawn system */
    private SpawnSystem spawnSystem;

    /* Height of the maze */
    public static int mazeRows = 15;
    /* Width of the maze */
    public static int mazeColumns = 15;

    /**
     * Method which invokes maze constructor generation method.
     */
    void Start() {
        // Maze construction
        mazeConstructor = GetComponent<MazeConstructor>();
        mazeConstructor.GenerateNewMaze(mazeRows, mazeColumns);

        // Player positioning
        spawnSystem = GetComponent<SpawnSystem>();
        spawnSystem.InitilizeSpawnSystem(mazeConstructor.mazeData);
        GameObject.FindGameObjectWithTag("Player").transform.position = spawnSystem.GetPlayerSpawnPosition();
    }
}