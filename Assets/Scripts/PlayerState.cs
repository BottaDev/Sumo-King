using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    public float speed = 5;
    public float turnSpeed = 250;
    public int consumablesCount = 0;
    [Range(min: 7, max: 14)]
    public float pushForce = 7;         // La fuerza para empujar a otro jugador (7 Min - 14 Max)
    public GameObject crown;

    [HideInInspector]
    public float speedBuff = 1;
    
    bool deBuffed = false;
    bool buffed = false;
    float debuffTimer = 0;
    float buffTimer = 0;
    

    void Update() {
        
        if (buffed){
            IncrenseSpeed();
        }

        if (deBuffed){
            StopSpeed();
        }
    }
    
    void IncrenseSpeed(){       // Se ejecuta al tomar un consumible

        buffTimer -= Time.deltaTime;

        if (buffTimer > 0){
            speedBuff = 1.4f;
        }else{

            buffed = false;

            speedBuff = 1f;
        }
    }

    void StopSpeed(){       // Se ejecuta al empujar a un jugador

        debuffTimer -= Time.deltaTime;

        if (debuffTimer > 0){
            speed = 0;
        }else{

            deBuffed = false;

            speed = 5;
        }
    }

    public void ApplyDebuff(){

        debuffTimer = 0.3f;
        deBuffed = true;
    }

    void OnTriggerEnter(Collider other){
        
        if (other.gameObject.tag == "Attack Consumable"){

            if (pushForce >= 14){
                pushForce = 14;
            } else {
                pushForce += 0.5f;
                transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            }

            buffTimer = 1;
            buffed = true;

            consumablesCount++;
        }

        if (other.gameObject.tag == "DestroyObject"){

            GameManager.instance.AddDeathPlayer();

            Destroy(gameObject, 2.5f);
        }
    }

    
}
