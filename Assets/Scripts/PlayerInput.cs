using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour{
    
    public enum PlayerNum{
        Player1,
        Player2,
        Player3,
        Player4,
    }

    public int playerNumber;
    public PlayerNum playerNum;
    public PlayerState state;
    public AnimationController animationController;

    Rigidbody rb;
    string turnAxis;
    string movementAxis;
    float movementInput;              
    float turnInput;

    void Start(){

        SetControls();

        rb = GetComponent<Rigidbody>();
    }

    void Update(){

        if(rb.position.y >= 1){

            movementInput = Input.GetAxis(movementAxis);
            turnInput = Input.GetAxis(turnAxis);            
        }

        animationController.SetBodyAnimations(movementInput, turnInput, state.speed, state.turnSpeed);
    }

    void FixedUpdate(){

        if (rb.position.y >= 1){

            Move();

            Turn();
        }
    }

    void Move(){

        Vector3 movement = transform.forward * movementInput * state.speed * state.speedBuff * Time.deltaTime;

        rb.MovePosition(rb.position + movement);
    }

    void Turn(){
        
        float turn = turnInput * state.turnSpeed * Time.deltaTime;

        Quaternion inputRotation = Quaternion.Euler(0f, turn, 0f);

        rb.MoveRotation(rb.rotation * inputRotation);
    }
    
    void SetControls(){

        switch (playerNum){

            case PlayerNum.Player1:
                movementAxis = "Vertical";
                turnAxis = "Horizontal";
                break;

            case PlayerNum.Player2:
                movementAxis = "Vertical P2";
                turnAxis = "Horizontal P2";
                break;

            case PlayerNum.Player3:
                movementAxis = "Vertical P3";
                turnAxis = "Horizontal P3";
                break;

            case PlayerNum.Player4:
                movementAxis = "Vertical P4";
                turnAxis = "Horizontal P4";
                break;
        }
    }
}
