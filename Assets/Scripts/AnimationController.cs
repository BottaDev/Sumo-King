using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour{

    public Animator bodyAnimator;
    public Animator leftHandAnimator;
    public Animator rightHandAnimator;

    Rigidbody rb;

    void Start(){

        rb = GetComponent<Rigidbody>();
    }

    public void SetBodyAnimations(float movementInput, float turnInput, float speed, float turnSpeed){

        if (rb.position.y >= 1){

            if ((movementInput != 0) && (speed > 0)){
                bodyAnimator.SetBool("Running", true);
            } else{
                bodyAnimator.SetBool("Running", false);
            }

            if ((turnInput != 0) && (bodyAnimator.GetBool("Running") == false) && (turnSpeed > 0)){
                bodyAnimator.SetBool("Turning", true);
            } else{
                bodyAnimator.SetBool("Turning", false);
            }
        } else{
            
            // CAE AL VACIO
            
            bodyAnimator.SetBool("Falling", true);

            leftHandAnimator.enabled = true;

            rightHandAnimator.enabled = true;
        }
    }

}
