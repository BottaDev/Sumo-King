using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class HandsController : MonoBehaviourPun
{
    public PlayerState state;
    public Animation anim;

    private GameObject _bodyParty;
    
    private void ApplyForceOnBody(GameObject body)
    {
        if (!photonView.IsMine) 
            return;

        _bodyParty = body;
        
        Player player = body.GetComponentInParent<PhotonView>().Owner;
        photonView.RPC("ApplyForceOnBodyRPC", player);

        state.ApplyDebuff();
        anim.Play();
    }

    private void ApplyForceOnHands(GameObject hands)
    {
        if (!photonView.IsMine) 
            return;

        _bodyParty = hands;
        
        Player player = hands.GetComponentInParent<PhotonView>().Owner;
        photonView.RPC("ApplyForceOnHandsRPC", player);

        state.ApplyDebuff();
        anim.Play();
    }

    [PunRPC]
    private void ApplyForceOnHandsRPC()
    {
        Rigidbody rb = _bodyParty.gameObject.GetComponentInParent<Rigidbody>();
        rb.AddForce(transform.forward * (state.pushForce / 2), ForceMode.Impulse);
    }
    
    [PunRPC]
    private void ApplyForceOnBodyRPC()
    {
        Rigidbody rb = _bodyParty.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * state.pushForce, ForceMode.Impulse);
    }
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
            ApplyForceOnBody(col.gameObject);

        if (col.gameObject.CompareTag("Hands"))
            ApplyForceOnHands(col.gameObject);
        
    }
}
