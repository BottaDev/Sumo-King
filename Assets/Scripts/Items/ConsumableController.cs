using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ConsumableController : MonoBehaviourPun
{
    public float rotationSpeed;
    public float groundLevelY;          // Nivel en el que se posada el objeto
    public Rigidbody rb;
    
    private void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            rb.isKinematic = true;
            transform.position = new Vector3(transform.position.x, groundLevelY, transform.position.z);
        }

        if (other.gameObject.CompareTag("Player"))
            Destroy(gameObject, 0.2f);

        if (other.gameObject.CompareTag("DestroyObject"))
            Destroy(gameObject);
    }
}
