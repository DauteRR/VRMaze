using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class representing the x rays vision consumable
 */
public class XRaysVision : Consumable {

    /* Floor material */
    public Material floorMaterial;
    /* Wall material */
    public Material wallMaterial;
    /* Transparent walls material */
    public Material xRaysWallMaterial;

    /*
     * Actions to take when the consumable effect starts
     */
    public override void Activate() {
        Material[] newMaterials = {floorMaterial, xRaysWallMaterial };
        GameObject maze = GameObject.FindGameObjectWithTag("Maze");
        maze.GetComponent<Renderer>().materials = newMaterials;
    }

    /*
     * Actions to take when the consumable effect finishes
     */
    public override void Deactivate() {
        Material[] newMaterials = { floorMaterial, wallMaterial };
        GameObject maze = GameObject.FindGameObjectWithTag("Maze");
        maze.GetComponent<Renderer>().materials = newMaterials;
    }
}