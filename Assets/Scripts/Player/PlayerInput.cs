using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerInput : MonoBehaviourPun
{
    public PlayerNum playerNum;
    
    [SerializeField, HideInInspector]private PlayerState _state;
    [SerializeField, HideInInspector]private AnimationController _animationController;
    [SerializeField, HideInInspector]private Rigidbody _rb;
    [SerializeField, HideInInspector]private string _movementAxis = "Vertical";
    [SerializeField, HideInInspector]private string _turnAxis = "Horizontal";
    [SerializeField, HideInInspector]private float _movementInput;              
    [SerializeField, HideInInspector]private float _turnInput;
    
    private void Awake()
    {
        _state = GetComponent<PlayerState>();
        _animationController = GetComponent<AnimationController>();
    }

    private void Start()
    {
        //SetControls();
        _rb = GetComponent<Rigidbody>();

        if (!photonView.IsMine)
            this.enabled = false;
    }

    private void Update()
    {
        if (!photonView.IsMine) 
            return;
        
        if(_rb.position.y >= 1)
        {
            _movementInput = Input.GetAxis(_movementAxis);
            _turnInput = Input.GetAxis(_turnAxis);            
        }

        _animationController.SetBodyAnimations(_movementInput, _turnInput, _state.speed, _state.turnSpeed);
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine) 
            return;
        
        if (_rb.position.y >= 1)
        {
            Move();
            Turn();
        }
    }

    private void Move()
    {
        Vector3 movement = transform.forward * (_movementInput * _state.speed * _state.speedBuff * Time.deltaTime);

        _rb.MovePosition(_rb.position + movement);
    }

    private void Turn()
    {
        float turn = _turnInput * _state.turnSpeed * Time.deltaTime;
        Quaternion inputRotation = Quaternion.Euler(0f, turn, 0f);

        _rb.MoveRotation(_rb.rotation * inputRotation);
    }
    
    /*private void SetControls()
    {
        switch (playerNum)
        {
            case PlayerNum.Player1:
                _movementAxis = "Vertical";
                _turnAxis = "Horizontal";
                break;

            case PlayerNum.Player2:
                _movementAxis = "Vertical P2";
                _turnAxis = "Horizontal P2";
                break;

            case PlayerNum.Player3:
                _movementAxis = "Vertical P3";
                _turnAxis = "Horizontal P3";
                break;

            case PlayerNum.Player4:
                _movementAxis = "Vertical P4";
                _turnAxis = "Horizontal P4";
                break;
        }
    }*/
    
    public enum PlayerNum
    {
        Player1,
        Player2,
        Player3,
        Player4,
    }
}
