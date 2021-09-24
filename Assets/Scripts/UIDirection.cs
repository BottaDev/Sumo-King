using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDirection : MonoBehaviour{

    // This class is used to make sure world space UI
    // elements such as the health bar face the correct direction.

    public bool useRelativePosition = true;     // Use relative position should be used for this gameobject?
    public bool useRelativeRotation = true;     // Use relative rotation should be used for this gameobject?
    public Canvas canvas;


    private Vector3 relativePosition;         // The local position at the start of the scene.
    private Quaternion relativeRotation;      // The local rotatation at the start of the scene.


    void Start(){
        relativePosition = transform.localPosition;
        relativeRotation = transform.localRotation;

        canvas.worldCamera = Camera.main;
    }


    void Update(){
        if (useRelativeRotation)
            transform.rotation = relativeRotation;

        if (useRelativePosition)
            transform.position = transform.parent.position + relativePosition;
    }
}
