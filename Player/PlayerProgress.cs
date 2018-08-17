using UnityEngine;
using System.Collections;

public class PlayerProgress
{
    public bool[] stage_clears = new bool[7];
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
            for (int i = 0; i < 6; i++)
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
