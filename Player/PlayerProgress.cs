using UnityEngine;
using System.Collections;

public class PlayerProgress
{
    public Stage[] stages;

    public bool SaveRecord(int stageNum, float time)
    {
        for(int i=0; i<stages.Length; i++)
        {
            if(stageNum == stages[i].StageNum)
            {
                stages[i].Clear(time);
                return true;
            }
        }
        return false;
    }
}
