using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class which represents a maze position.
 */
public class Position {
    /* Row */
    public int row;
    /* Column */
    public int column;

    /*
     * Constructor
     */
    public Position(int row, int column) {
        this.row = row;
        this.column = column;
    }
}

/*
 * Class which contains all the spawn logic
 */
public class SpawnSystem : MonoBehaviour
{
    /* Flag to indicate if the system is initialized */
    private bool isInitialized = false;
    /* Maze representation */
    private MazeLocation[,] mazeData;
    /* Height of the maze */
    private int rows;
    /* Width of the maze */
    private int columns;

    /*
     * Method to "initialize" spawn system. A copy of
     * the maze is needed to provide valid locations
     * for spawning.
     * @param maze Maze data
     */
    public void InitilizeSpawnSystem(MazeLocation[,] maze) {
        this.mazeData = maze;
        this.isInitialized = true;
        this.rows = maze.GetLength(0);
        this.columns = maze.GetLength(1);
    }

    /*
     * Returns a valid (FREE) position
     * @param newMazeLocation Maze location
     * @return Valid position
     */
    private Position GenerateValidPosition(MazeLocation newMazeLocation) {

        if (!this.isInitialized) {
            throw new System.Exception("Spawn system not initialized");
        }

        while (true) {
            int row = Random.Range(1, rows);
            int column = Random.Range(1, columns);
            if (this.mazeData[row, column] == MazeLocation.FREE) {
                this.mazeData[row, column] = newMazeLocation;
                return new Position(row, column);
            }
        }
    }

    /*
     * Returns the player spawn position
     * @return Player spawn position
     */
    public Vector3 GetPlayerSpawnPosition() {
        Position spawnPosition = GenerateValidPosition(MazeLocation.PLAYER_SPAWN);
        MazeLocation p = mazeData[spawnPosition.row, spawnPosition.column];
        MazeLocation q = mazeData[spawnPosition.column, spawnPosition.row];
        
        return new Vector3(
            spawnPosition.column * MazeMeshGenerator.width, 
            1.09f, 
            spawnPosition.row * MazeMeshGenerator.width
        );
    }

    
}
