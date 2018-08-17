using System.Collections;
using UnityEngine;

public class Intro : MonoBehaviour
{
    void Start()
    {
        PlayerProgress playerProgress = GameManager.instance.playerProgress;
        if (playerProgress.stage_clears[0] == false)
        {
            LevelLoader.instance.StartLoad(2);
        }
        else
        {
            LevelLoader.instance.StartLoad(1);
        }
    }
}
