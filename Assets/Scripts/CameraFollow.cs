using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour{

    public Camera cam;

    Transform target = null;
    bool sizeLimiter = false;

    void LateUpdate(){

        if (target != null){

            transform.position = target.position + new Vector3(-11, 20, -11);

            if (!sizeLimiter){
                ChangeSize();
            }
        }
    }

    void ChangeSize(){
        
        if (cam.orthographicSize > 5){
            cam.orthographicSize -= 0.1f;
        } else {
            sizeLimiter = true;
        }
    }

    public void SelectTarget(GameObject player){

        target = player.transform;
    }

}
