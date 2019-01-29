using System;
using UnityEngine;
using UnityEngine.AI;

/*
 * Controls the state of the game. 
 */
[RequireComponent(typeof(MazeConstructor))]
[RequireComponent(typeof(RespawnSystem))]
[RequireComponent(typeof(NavMeshSurface))]
[RequireComponent(typeof(InputController))]
public class GameController : MonoBehaviour {

    /* Maze constructor */
    private MazeConstructor mazeConstructor;
    /* Respawn system */
    private RespawnSystem respawnSystem;
    /* Maze navigation mesh surface */
    private NavMeshSurface mazeSurface;

    /* Height of the maze */
    public int initialMazeRows = 9;
    /* Width of the maze */
    public int initialMazeColumns = 9;

    public int solvedMazes = 0;

    /**
     * Method which invokes maze constructor generation method.
     */
    private void Awake() {
        mazeConstructor = GetComponent<MazeConstructor>();
        respawnSystem = GetComponent<RespawnSystem>();
        mazeSurface = GetComponent<NavMeshSurface>();

    }

    private void Start() {
        OnNewMaze();
    }

    public void OnNewMaze() {
        // Maze construction
        mazeConstructor.GenerateNewMaze(initialMazeRows, initialMazeColumns);

        // Spawn system initialization
        respawnSystem.InitilizeRespawnSystem(mazeConstructor.mazeData);

        // Maze nav mesh surface baking
        mazeSurface.BuildNavMesh();

        // Player positioning
        respawnSystem.SetPlayerRespawn();

        // Enemy respawns
        respawnSystem.SetEnemyRespawnsPositions();

        // Final point
        respawnSystem.SetFinalPoint();

        // Consumables
        respawnSystem.SetConsumableLocations();
    }
}