using UnityEngine;
using System.Collections;

public class Teleport: MonoBehaviour
{
    bool istriggered = false;
    public int stageNum = 0;
    protected virtual void Event() {}
    

    void OnTriggerEnter (Collider col)
    {
        if(col.gameObject.name == "Player" && istriggered == false)
        {
            istriggered = true;
            Event();
        }
    }
    void OnTriggerExit(Collider col)
    {
        istriggered = true;
    }
}
