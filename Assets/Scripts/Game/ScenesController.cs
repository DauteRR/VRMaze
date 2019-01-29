using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour {

    public void OnNewMaze() {
        SceneManager.LoadScene("MazeScene", LoadSceneMode.Single);
    }

    public void Foo() {
        Debug.Log("Foo!");
    }
}
