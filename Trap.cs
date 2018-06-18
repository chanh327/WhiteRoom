using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour
{
    public SpawnManager spawnManager;
    private Transform[] candidates;

   bool istriggered = false;

    void OnTriggerEnter (Collider col)
    {
        if(col.gameObject.name == "Player" && istriggered == false)
        {
            istriggered = true;
            StartCoroutine(spawnManager.SpawnPlayer());
        }
    }
    void OnTriggerExit(Collider other)
    {
        istriggered = true;
    }
}