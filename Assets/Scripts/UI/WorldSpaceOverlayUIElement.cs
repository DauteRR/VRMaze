using UnityEngine;
using UnityEngine.UI;

/*
 * Script to attach to any user interface element to transform it
 * into a world space overlay element
 */
public class WorldSpaceOverlayUIElement : MonoBehaviour {

    /* Compare function */
    public UnityEngine.Rendering.CompareFunction comparison = UnityEngine.Rendering.CompareFunction.Always;

    /*
     * Initialization method
     */
    private void Start() {
        MaskableGraphic uiElement = GetComponent<MaskableGraphic>();
        Material existingGlobalMat = uiElement.materialForRendering;
        Material updatedMaterial = new Material(existingGlobalMat);
        updatedMaterial.SetInt("unity_GUIZTestMode", (int)comparison);
        uiElement.material = updatedMaterial;
    }
}