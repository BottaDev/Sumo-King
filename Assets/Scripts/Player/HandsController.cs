using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class HandsController : MonoBehaviourPun
{
    public PlayerState state;
    public Animation anim;

    private void ApplyForceOnBody(GameObject body)
    {
        Rigidbody rb = body.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * state.pushForce, ForceMode.Impulse);

        state.ApplyDebuff();
        anim.Play();
    }

    private void ApplyForceOnHands(GameObject hands)
    {
        Rigidbody rb = hands.gameObject.GetComponentInParent<Rigidbody>();
        rb.AddForce(transform.forward * (state.pushForce / 2), ForceMode.Impulse);

        state.ApplyDebuff();
        anim.Play();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
            ApplyForceOnBody(collider.gameObject);

        if (collider.gameObject.CompareTag("Hands"))
            ApplyForceOnHands(collider.gameObject);
        
    }
}
