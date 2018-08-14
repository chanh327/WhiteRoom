using UnityEngine;
using System.Collections;

public class PlayerProgress
{
    public bool[] stage_clears = new bool[6];
    public bool watchEnding;

    public void Reset()
    {
        stage_clears = new bool[6];
    }

    public void Finish(int sceneidx)
    {
        stage_clears[sceneidx] = true;
    }

    public bool IsAllClear
    {
        get
        {
            bool clear = true;
            for (int i = 0; i < stage_clears.Length; i++)
            {
                if (stage_clears[i] == false)
                {
                    clear = false;
                    break;
                }
            }
            return clear;
        }

    }
}
