using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Goal: Teleport
{
    protected override void  Event()
    {
        GameManager.instance.SaveProgress(SceneManager.GetActiveScene().buildIndex);
        LevelLoader.instance.LoadLevel(stageNum);
    }
}