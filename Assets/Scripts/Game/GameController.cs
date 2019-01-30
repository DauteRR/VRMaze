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

    /*
     * Initialization method
     */
    private void Start() {
        ScenesController scenesController = 
            GameObject.FindGameObjectWithTag("ScenesController").GetComponent<ScenesController>();

        mazeConstructor = GetComponent<MazeConstructor>();
        respawnSystem = GetComponent<RespawnSystem>();
        mazeSurface = GetComponent<NavMeshSurface>();

        // Maze construction
        mazeConstructor.GenerateNewMaze(scenesController.currentMazeRows, scenesController.currentMazeColumns);

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