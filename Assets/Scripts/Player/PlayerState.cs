using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerState : MonoBehaviourPun 
{
    public float speed = 5;
    public float turnSpeed = 250;
    public int consumablesCount = 0;
    [Range(min: 7, max: 14)]
    public float pushForce = 7;         
    public GameObject crown;
    [HideInInspector] public float speedBuff = 1;
    
    [SerializeField, HideInInspector] private bool _deBuffed = false;
    [SerializeField, HideInInspector] private bool _buffed = false;
    [SerializeField, HideInInspector] private float _debuffTimer = 0;
    [SerializeField, HideInInspector] private float _buffTimer = 0;
    
    private void Update() 
    {
        if (!photonView.IsMine) 
            return;
        
        if (_buffed)
            IncrenseSpeed();

        if (_deBuffed)
            StopSpeed();
    }
    
    private void IncrenseSpeed()
    {
        _buffTimer -= Time.deltaTime;

        if (_buffTimer > 0)
        {
            speedBuff = 1.4f;
        }
        else
        {
            _buffed = false;
            speedBuff = 1f;
        }
    }

    private void StopSpeed()
    {
        _debuffTimer -= Time.deltaTime;

        if (_debuffTimer > 0)
        {
            speed = 0;
        }
        else
        {
            _deBuffed = false;
            speed = 5;
        }
    }

    public void ApplyDebuff()
    {
        _debuffTimer = 0.3f;
        _deBuffed = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack Consumable"))
        {
            if (pushForce >= 14)
            {
                pushForce = 14;
            }
            else 
            {
                pushForce += 0.5f;
                transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            }

            _buffTimer = 1;
            _buffed = true;

            consumablesCount++;
        }

        if (other.gameObject.CompareTag("DestroyObject"))
        {
            GameManager.instance.AddDeathPlayer();

            Destroy(gameObject, 2.5f);
        }
    }
}
