using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPoint : Gazable {

    public override void OnPointerClick() {
        Debug.Log("Final point reached!");
    }
}
