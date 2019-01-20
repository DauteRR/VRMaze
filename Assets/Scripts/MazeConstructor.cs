using UnityEngine;

public class MazeConstructor : MonoBehaviour {
    public bool showDebug;

    [SerializeField] private Material mazeMat1;
    [SerializeField] private Material mazeMat2;

    public int[,] data {
        get; private set;
    }

    private MazeDataGenerator dataGenerator;
    private MazeMeshGenerator meshGenerator;

    void Awake() {
        dataGenerator = new MazeDataGenerator();
        meshGenerator = new MazeMeshGenerator();
    }

    public void GenerateNewMaze(int rows, int columns) {
        if (rows % 2 == 0 && columns % 2 == 0) {
            Debug.LogError("Odd numbers work better for dungeon size.");
        }

        data = dataGenerator.FromDimensions(rows, columns);
        DisplayMaze();
    }

    void OnGUI() {
        if (!showDebug) {
            return;
        }

        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        string msg = "";
        for (int i = rMax; i >= 0; i--) {
            for (int j = 0; j <= cMax; j++) {
                if (maze[i, j] == 0) {
                    msg += "....";
                } else {
                    msg += "==";
                }
            }
            msg += "\n";
        }
        GUI.Label(new Rect(20, 20, 500, 500), msg);
    }

    private void DisplayMaze() {
        GameObject gameObject = new GameObject();
        gameObject.transform.position = Vector3.zero;
        gameObject.name = "Procedural Maze";
        gameObject.tag = "GeneratedMesh";

        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = meshGenerator.FromData(data);

        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = meshFilter.mesh;

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.materials = new Material[2] { mazeMat1, mazeMat2 };
    }

}
