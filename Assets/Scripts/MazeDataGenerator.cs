using System.Collections.Generic;
using UnityEngine;

/**
 * Class which contains the maze construction logic
 */
public class MazeDataGenerator {
    
    /* Value needed for the maze creation */
    public float placementThreshold = .1f;

    /*
     * Creates a maze with the given dimensions
     * @param sizeRows Height of the maze
     * @param sizeCols Width of the maze
     * @return Maze data
     */
    public MazeLocation[,] FromDimensions(int sizeRows, int sizeCols) {
        MazeLocation[,] maze = new MazeLocation[sizeRows, sizeCols];
        int maxRows = maze.GetUpperBound(0);
        int maxColumns = maze.GetUpperBound(1);

        for (int i = 0; i <= maxRows; i++) {
            for (int j = 0; j <= maxColumns; j++) {

                if (i == 0 || j == 0 || i == maxRows || j == maxColumns) {
                    maze[i, j] = MazeLocation.WALL;
                } else if (i % 2 == 0 && j % 2 == 0) {

                    if (Random.value > placementThreshold) {
                        maze[i, j] = MazeLocation.WALL;
                        int a = (Random.value < .5) ? 0 : (Random.value < .5 ? -1 : 1);
                        int b = (a != 0) ? 0 : (Random.value < .5 ? -1 : 1);
                        maze[i + a, j + b] = MazeLocation.WALL;
                    }
                }
            }
        }

        return maze;
    }
}
