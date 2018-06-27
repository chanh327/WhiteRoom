using System.Collections;
using UnityEngine;

[System.Serializable]
public class Stage
{
    [SerializeField]
    private int stageNum;
    [SerializeField]
    public bool isLock = false;
    [SerializeField]
    private bool isCleared = false;
    [SerializeField]
    private float timeRecord = 0f;

    public int StageNum
    {
        get { return stageNum;}
    }

    public bool IsCleared
    {
        get { return isCleared;}
    }

    public float TimeRecord
    {
        get { return timeRecord;}
    }

    public void Clear(float time)
    {
        isCleared = true;
        Debug.Log(time);
        timeRecord = time;
    }

    public Stage(int stageNum)
    {
        this.stageNum = stageNum;
    }
}