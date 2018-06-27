using UnityEngine;
using System.Collections;

public class Goal: MonoBehaviour
{
    bool istriggered = false;

    void OnTriggerEnter (Collider col)
    {
        if(col.gameObject.name == "Player" && istriggered == false)
        {
            istriggered = true;
            GameManager.instance.ClearStage();
        }
    }
    void OnTriggerExit(Collider other)
    {
        istriggered = true;
    }
}
