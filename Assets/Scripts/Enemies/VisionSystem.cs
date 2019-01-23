using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class which wraps the enemies vision system logic
 */
public class VisionSystem : MonoBehaviour {

    /* Distance of the field of view */
    public float viewRadius;
    /* Angle of the field of view */
    [Range(0, 360)]
    public float viewAngle;
    /* Target mask */
    public LayerMask targetMask;
    /* Obstacle mask */
    public LayerMask obstacleMask;
    /* Delay for the FindTargets coroutine */
    public float lookForTargetsDelay = .2f;
    /* List of visible targets */
    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();
    /* Origin point of the field of view */
    [HideInInspector]
    public Vector3 fovOrigin;
    /* Variable to adjust the field of view */
    public Vector3 fovLevel;

    /*
     * Method which keep fovOrigin variable up-to-date
     */
    private void Update() {
        fovOrigin = transform.position + fovLevel;
    }

    /*
     * Method to search for visible target each
     * lookForTargetDelays seconds
     */
    public IEnumerator FindTargets() {
        while (true) {
            yield return new WaitForSeconds(lookForTargetsDelay);
            FindVisibleTargets();
        }
    }

    /*
     * Method which specifies if there is a target
     * in the field of view
     */
    private void FindVisibleTargets() {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(fovOrigin, viewRadius, targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++) {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - fovOrigin).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2) {
                float dstToTarget = Vector3.Distance(fovOrigin, target.position);

                if (!Physics.Raycast(fovOrigin, dirToTarget, dstToTarget, obstacleMask)) {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    /*
     * Checks if there is a target in the field of view
     * @return Result of the comprobation
     */
    public bool IsTargetInFOV() {
        if (visibleTargets.Count > 0) {
            return true;
        }
        return false;
    }

    /*
     * Converts an angle into a direction
     * @param angleInDegrees Angle
     * @param angleIsGlobal
     * @return Direction
     */
    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal) {

        if (!angleIsGlobal) {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(
            Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),
            0,
            Mathf.Cos(angleInDegrees * Mathf.Deg2Rad)
        );
    }
}
