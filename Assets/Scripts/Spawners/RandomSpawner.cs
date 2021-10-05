using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RandomSpawner : MonoBehaviourPun
{
    public GameObject consumable;
    public float spawnRate;        // Cuanto menor el  valor, mayor la cantidad de consumibles
    public float boundaryX;        // Limite de arena de spawn en X
    public float boundaryZ;        // Limite de arena de spawn en Z
    public float dropHeight = 35;

    [HideInInspector] public bool matchIsOver = false;         // La ronda termino o no
    
    [SerializeField, HideInInspector] private bool spawnRateLimiter = false;

    private IEnumerator StartDelay(float timer)
    {
        yield return new WaitForSeconds(timer);

        StartCoroutine(nameof(SpawnConsumables));
    }

    private IEnumerator SpawnConsumables()
    {
        while (!matchIsOver){

            yield return new WaitForSeconds(spawnRate);

            Vector3 position = new Vector3(Random.Range(-boundaryX, boundaryX), dropHeight, Random.Range(-boundaryZ, boundaryZ));
            Quaternion rotation = new Quaternion(0, Random.Range(0f, 360f), 0, 0);
            PhotonNetwork.Instantiate("Prefabs/Props/" + consumable.name, position, rotation);
        } 
    } 

    public void ChangeSpawnRate()
    {
        if (!spawnRateLimiter)
        {
            spawnRateLimiter = true;

            spawnRate /= 2;
        }
    }

    public void EndMatch()
    {
        matchIsOver = true;
    }
}
