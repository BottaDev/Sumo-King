using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableController : MonoBehaviour{
    
    public float rotationSpeed;
    public float groundLevelY;          // Nivel en el que se posada el objeto
    public Rigidbody rb;
    // 0.08  0.02   0.02
    void Update(){

        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }

    void OnTriggerEnter(Collider other){

        if (other.gameObject.tag == "Ground"){

            rb.isKinematic = true;

            transform.position = new Vector3(transform.position.x, groundLevelY, transform.position.z);
        }

        if (other.gameObject.tag == "Player"){

            Destroy(gameObject, 0.2f);
        }

        if (other.gameObject.tag == "DestroyObject"){

            Destroy(gameObject);
        }
    }
}
