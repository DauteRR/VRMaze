using System;
using UnityEngine;
using UnityEngine.AI;

/*
 * Controls the state of the game. 
 */
[RequireComponent(typeof(MazeConstructor))]
[RequireComponent(typeof(RespawnSystem))]
[RequireComponent(typeof(NavMeshSurface))]
public class GameController : MonoBehaviour {

    /* Maze constructor */
    private MazeConstructor mazeConstructor;
    /* Respawn system */
    private RespawnSystem respawnSystem;

    /* Height of the maze */
    public int mazeRows = 15;
    /* Width of the maze */
    public int mazeColumns = 15;

    /**
     * Method which invokes maze constructor generation method.
     */
    void Start() {
        // Maze construction
        mazeConstructor = GetComponent<MazeConstructor>();
        mazeConstructor.GenerateNewMaze(mazeRows, mazeColumns);

        // Spawn system initialization
        respawnSystem = GetComponent<RespawnSystem>();
        respawnSystem.InitilizeRespawnSystem(mazeConstructor.mazeData);

        // Maze nav mesh surface baking
        NavMeshSurface mazeSurface = GetComponent<NavMeshSurface>();
        mazeSurface.BuildNavMesh();

        // Player positioning
        respawnSystem.RespawnPlayer();

        // Enemy respawns
        respawnSystem.RespawnEnemies();

        // Final point
        respawnSystem.SetFinalPoint();
    }
}