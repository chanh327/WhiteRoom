using System.Collections;
using UnityEngine;

public class StageHandler : MonoBehaviour
{
    public void LoadStage(int stageIndex)
    {
        LevelLoader.instance.LoadLevel(stageIndex);
    }
}