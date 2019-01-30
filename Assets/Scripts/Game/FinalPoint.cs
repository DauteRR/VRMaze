using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPoint : Gazable {

    public override void OnPointerClick() {
        ScenesController scenesController =
            GameObject.FindGameObjectWithTag("ScenesController").GetComponent<ScenesController>();
        scenesController.OnFinishedMaze(true);
    }
}
