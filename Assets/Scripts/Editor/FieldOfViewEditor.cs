using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*
 * Class which allows us to view the enemy fields of view 
 * in the scene
 */
[CustomEditor (typeof (VisionSystem))]
public class FieldOfViewEditor : Editor {

    /*
     * Method to visualize the field of view in the editor
     */
    private void OnSceneGUI() {
        VisionSystem fov = (VisionSystem)target;
        Handles.color = Color.white;

        Handles.DrawWireArc(
            fov.fovOrigin,
            Vector3.up,
            Vector3.forward,
            360,
            fov.viewRadius
        );

        Vector3 leftViewAngle = fov.DirectionFromAngle(-fov.viewAngle / 2, false);
        Vector3 rightViewAngle = fov.DirectionFromAngle(fov.viewAngle / 2, false);
        Handles.DrawLine(
            fov.fovOrigin,
            fov.fovOrigin + leftViewAngle * fov.viewRadius
        );
        Handles.DrawLine(
            fov.fovOrigin,
            fov.fovOrigin + rightViewAngle * fov.viewRadius
        );

        Handles.color = Color.red;
        foreach(Transform transform in fov.visibleTargets) {
            Handles.DrawLine(fov.fovOrigin, transform.position);
        }
    }
}
