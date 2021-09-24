using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsController : MonoBehaviour{

    public PlayerState state;
    public Animation anim;
    
    void OnTriggerEnter(Collider collider){
        
        if (collider.gameObject.tag == "Player"){
            
            ApplyForceOnBody(collider.gameObject);
        }

        if (collider.gameObject.tag == "Hands"){

            ApplyForceOnHands(collider.gameObject);
        }
    }

    void ApplyForceOnBody(GameObject body){

        Rigidbody rb = body.gameObject.GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * state.pushForce, ForceMode.Impulse);

        state.ApplyDebuff();

        anim.Play();
    }

    void ApplyForceOnHands(GameObject hands){

        Rigidbody rb = hands.gameObject.GetComponentInParent<Rigidbody>();

        rb.AddForce(transform.forward * (state.pushForce / 2), ForceMode.Impulse);

        state.ApplyDebuff();

        anim.Play();
    }

    
}
