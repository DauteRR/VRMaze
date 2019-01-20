using System.Collections.Generic;
using UnityEngine;

public class MazeDataGenerator {
    public float placementThreshold;

    public MazeDataGenerator() {
        placementThreshold = .1f;
    }

    public int[,] FromDimensions(int sizeRows, int sizeCols) {
        int[,] maze = new int[sizeRows, sizeCols];
        int maxRows = maze.GetUpperBound(0);
        int maxColumns = maze.GetUpperBound(1);

        for (int i = 0; i <= maxRows; i++) {
            for (int j = 0; j <= maxColumns; j++) {

                if (i == 0 || j == 0 || i == maxRows || j == maxColumns) {
                    maze[i, j] = 1;
                } else if (i % 2 == 0 && j % 2 == 0) {

                    if (Random.value > placementThreshold) {

                        maze[i, j] = 1;
                        int a = (Random.value < .5) ? 0 : (Random.value < .5 ? -1 : 1);
                        int b = (a != 0) ? 0 : (Random.value < .5 ? -1 : 1);
                        maze[i + a, j + b] = 1;
                    }
                }
            }
        }

        return maze;
    }
}
