using System;
using UnityEngine;

[RequireComponent(typeof(MazeConstructor))]
public class GameController : MonoBehaviour {

    private MazeConstructor generator;

    public int mazeRows = 15;

    public int mazeColumns = 15;

    void Start() {
        generator = GetComponent<MazeConstructor>();
        generator.GenerateNewMaze(mazeRows, mazeColumns);
    }
}