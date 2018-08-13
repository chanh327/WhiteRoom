using UnityEngine;
using System.Collections;

public class PlayerProgress
{   
    public bool[] stage_clears = new bool[6];

    public void Reset()
    {
        stage_clears = new bool[6];
    }    
    public void Clear(int sceneidx)
    {
        stage_clears[sceneidx] = true;
    }
}
