using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotator : MonoBehaviour{

    public float speed;

    void OnEnable(){

        transform.rotation = new Quaternion(0, 180f, 0, 0);
    }

    void Update(){

        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
