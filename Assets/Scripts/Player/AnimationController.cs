using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class AnimationController : MonoBehaviourPun{

    public Animator bodyAnimator;
    public Animator leftHandAnimator;
    public Animator rightHandAnimator;

    private Rigidbody _rb;
    private float _movementInput;
    private float _turnInput;
    private float _speed;
    private float _turnSpeed;
    
    private static readonly int Running = Animator.StringToHash("Running");
    private static readonly int Turning = Animator.StringToHash("Turning");
    private static readonly int Falling = Animator.StringToHash("Falling");

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    public void SetBodyAnimations(float movementInput, float turnInput, float speed, float turnSpeed)
    {
        if (!photonView.IsMine) 
            return;
        
        if (_rb.position.y >= 1)
        {
            _movementInput = movementInput;
            _turnInput = turnInput;
            _speed = speed;
            _turnSpeed = turnSpeed;
            
            photonView.RPC("SetRunAnimation", RpcTarget.All);
            photonView.RPC("SetTurnAnimation", RpcTarget.All);
        } 
        else
        {
            photonView.RPC("SetFallAnimation", RpcTarget.All);
        }
    }

    [PunRPC]
    private void SetFallAnimation()
    {
        bodyAnimator.SetBool(Falling, true);

        leftHandAnimator.enabled = true;
        rightHandAnimator.enabled = true;
    }
    
    [PunRPC]
    private void SetTurnAnimation()
    {
        if ((_turnInput != 0) && (bodyAnimator.GetBool(Running) == false) && (_turnSpeed > 0)){
            bodyAnimator.SetBool(Turning, true);
        } else{
            bodyAnimator.SetBool(Turning, false);
        }
    }
    
    [PunRPC]
    private void SetRunAnimation()
    {
        if ((_movementInput != 0) && (_speed > 0)){
                bodyAnimator.SetBool(Running, true);
        } else{
                bodyAnimator.SetBool(Running, false);
        }
    }
    
}
