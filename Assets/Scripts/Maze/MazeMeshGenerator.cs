using System.Collections.Generic;
using UnityEngine;

/**
 * Class which contains the maze visual construction logic
 */
public class MazeMeshGenerator {
    /* How wide are hallways */
    public static float width = 3f;
    /* How tall are hallways */
    public static float height = 3f;

    /*
     * Constructs and returns the maze mesh
     * @param data Maze data
     * @return Maze mesh
     */
    public Mesh FromData(MazeLocation[,] data) {
        Mesh maze = new Mesh();

        List<Vector3> newVertices = new List<Vector3>();
        List<Vector2> newUVs = new List<Vector2>();

        maze.subMeshCount = 2;
        List<int> floorTriangles = new List<int>();
        List<int> wallTriangles = new List<int>();

        int maxRows = data.GetUpperBound(0);
        int maxColumns = data.GetUpperBound(1);
        float halfHeight = height * .5f;

        for (int i = 0; i <= maxRows; i++) {
            for (int j = 0; j <= maxColumns; j++) {
                if (data[i, j] == MazeLocation.FREE) {
                    // floor
                    AddQuad(Matrix4x4.TRS(
                        new Vector3(j * width, 0, i * width),
                        Quaternion.LookRotation(Vector3.up),
                        new Vector3(width, width, 1)
                    ), ref newVertices, ref newUVs, ref floorTriangles);

                    // ceiling
                    /*
                    AddQuad(Matrix4x4.TRS(
                        new Vector3(j * width, height, i * width),
                        Quaternion.LookRotation(Vector3.down),
                        new Vector3(width, width, 1)
                    ), ref newVertices, ref newUVs, ref floorTriangles);
                    */

                    // walls on sides next to blocked grid cells
                    if (i - 1 < 0 || data[i - 1, j] == MazeLocation.WALL) {
                        AddQuad(Matrix4x4.TRS(
                            new Vector3(j * width, halfHeight, (i - .5f) * width),
                            Quaternion.LookRotation(Vector3.forward),
                            new Vector3(width, height, 1)
                        ), ref newVertices, ref newUVs, ref wallTriangles);
                    }

                    if (j + 1 > maxColumns || data[i, j + 1] == MazeLocation.WALL) {
                        AddQuad(Matrix4x4.TRS(
                            new Vector3((j + .5f) * width, halfHeight, i * width),
                            Quaternion.LookRotation(Vector3.left),
                            new Vector3(width, height, 1)
                        ), ref newVertices, ref newUVs, ref wallTriangles);
                    }

                    if (j - 1 < 0 || data[i, j - 1] == MazeLocation.WALL) {
                        AddQuad(Matrix4x4.TRS(
                            new Vector3((j - .5f) * width, halfHeight, i * width),
                            Quaternion.LookRotation(Vector3.right),
                            new Vector3(width, height, 1)
                        ), ref newVertices, ref newUVs, ref wallTriangles);
                    }

                    if (i + 1 > maxRows || data[i + 1, j] == MazeLocation.WALL) {
                        AddQuad(Matrix4x4.TRS(
                            new Vector3(j * width, halfHeight, (i + .5f) * width),
                            Quaternion.LookRotation(Vector3.back),
                            new Vector3(width, height, 1)
                        ), ref newVertices, ref newUVs, ref wallTriangles);
                    }
                }
            }
        }

        maze.vertices = newVertices.ToArray();
        maze.uv = newUVs.ToArray();

        maze.SetTriangles(floorTriangles.ToArray(), 0);
        maze.SetTriangles(wallTriangles.ToArray(), 1);

        maze.RecalculateNormals();

        return maze;
    }

    /*
     * Constructs and adds a quad to the mesh
     * @param transformationMatrix Transformation matrix
     * @param newVertices List of mesh vertices
     * @param newUVs List of mesh UVs
     * @param newTriangles List of mesh triangles
     */
    private void AddQuad(Matrix4x4 transformationMatrix, ref List<Vector3> newVertices,
        ref List<Vector2> newUVs, ref List<int> newTriangles) {
        int index = newVertices.Count;

        // corners before transforming
        Vector3 vert1 = new Vector3(-.5f, -.5f, 0);
        Vector3 vert2 = new Vector3(-.5f, .5f, 0);
        Vector3 vert3 = new Vector3(.5f, .5f, 0);
        Vector3 vert4 = new Vector3(.5f, -.5f, 0);

        newVertices.Add(transformationMatrix.MultiplyPoint3x4(vert1));
        newVertices.Add(transformationMatrix.MultiplyPoint3x4(vert2));
        newVertices.Add(transformationMatrix.MultiplyPoint3x4(vert3));
        newVertices.Add(transformationMatrix.MultiplyPoint3x4(vert4));

        newUVs.Add(new Vector2(1, 0));
        newUVs.Add(new Vector2(1, 1));
        newUVs.Add(new Vector2(0, 1));
        newUVs.Add(new Vector2(0, 0));

        newTriangles.Add(index + 2);
        newTriangles.Add(index + 1);
        newTriangles.Add(index);

        newTriangles.Add(index + 3);
        newTriangles.Add(index + 2);
        newTriangles.Add(index);
    }

}
