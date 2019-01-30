using UnityEngine;
using UnityEngine.AI;

/*
 * Enumeration of the possible states of a maze location
 */
public enum MazeLocation {
    FREE,
    WALL,
    PLAYER_RESPAWN,
    ENEMY_RESPAWN,
    CONSUMABLE_LOCATION,
    FINAL_POINT
}

/*
 * Class which contains the funcionalities needed
 * to control the maze construction.
 */
public class MazeConstructor : MonoBehaviour {

    /* Option for debug mode */
    public bool showDebug;
    /* Floor material */
    [SerializeField] private Material mazeMat1;
    /* Walls material */
    [SerializeField] private Material mazeMat2;
    /* Maze data */
    public MazeLocation[,] mazeData {
        get; private set;
    }
    /* Maze data generator */
    private MazeDataGenerator dataGenerator;
    /* Maze mesh generator */
    private MazeMeshGenerator meshGenerator;

    /*
     * Method to initialize maze data and mesh generator
     */
    private void Awake() {
        dataGenerator = new MazeDataGenerator();
        meshGenerator = new MazeMeshGenerator();
    }

    /*
     * Method which generates a new maze
     * @param rows Heihgt of the new maze
     * @param columns Width of the new maze
     */
    public void GenerateNewMaze(int rows, int columns) {
        if (rows % 2 == 0 && columns % 2 == 0) {
            Debug.LogError("Odd numbers work better for dungeon size.");
        }
        mazeData = dataGenerator.FromDimensions(rows, columns);
        DisplayMaze();
    }

    private void OnGUI() {
        if (showDebug)
            ShowDebug();
    }

    /*
     * Method to visualize the maze as a string,
     * for debugging purposes
     */
    private void ShowDebug() {
        MazeLocation[,] maze = mazeData;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        string mazeRepresentation = "";
        for (int i = rMax; i >= 0; i--) {
            for (int j = 0; j <= cMax; j++) {
                if (maze[i, j] == 0) {
                    mazeRepresentation += "....";
                } else {
                    mazeRepresentation += "==";
                }
            }
            mazeRepresentation += "\n";
        }
        GUI.Label(new Rect(20, 20, 500, 500), mazeRepresentation);
    }

    /*
     * Method to display the generated maze
     */
    public void DisplayMaze() {
        GameObject mazeGameObject = new GameObject();
        mazeGameObject.transform.position = Vector3.zero;
        mazeGameObject.name = "Procedural Maze";
        mazeGameObject.tag = "Maze";

        MeshFilter meshFilter = mazeGameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = meshGenerator.FromData(mazeData);

        MeshCollider meshCollider = mazeGameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = meshFilter.mesh;

        MeshRenderer meshRenderer = mazeGameObject.AddComponent<MeshRenderer>();
        meshRenderer.materials = new Material[2] { mazeMat1, mazeMat2 };

        // We assigne the "Maze" layer to the maze game object
        mazeGameObject.layer = 10;
    }

}
