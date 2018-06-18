using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private static PlayerProgress playerProgress = null;
    private int stageNum;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        playerProgress = new PlayerProgress();
    }
    public void ExitStage()
    {
        playerProgress.Save(stageNum);
        Debug.Log("ExitStage");
    }
    public void ExitGame()
    {
        
    }

    public void SaveProgress()
    {

    }
}
