using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public PlayerProgress playerProgress;
    public int curStageNum;
    private static string ProgressFilePath
    {
        get
        {
            return Path.Combine(Application.dataPath, "Resources/playerProgress.json");
        }
    }
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        curStageNum = SceneManager.GetActiveScene().buildIndex;;
    }
    void Start()
    {
        LoadProgress();
    }
    public void ClearStage()
    {
        LevelLoader.instance.LoadLevel(0);
        SaveProgress();
        curStageNum = 0;
        //Debug.Log(playerProgress.stages[0].IsCleared);
    }
    public void ReturnToMenu()
    {
        TimeManager.instance.StopRecord();
        LevelLoader.instance.LoadLevel(0);
        curStageNum = 0;
    }
    public void QuitGame()
    {
        SaveProgress();
        Application.Quit();
    }

    public void LoadLevel(int sceneIndex)
    {
        curStageNum = sceneIndex;
        if(curStageNum != 0)
            TimeManager.instance.StartRecord();
    }

    public bool LoadProgress()
    {
        if (File.Exists(ProgressFilePath))
        {
            string dataAsJson = File.ReadAllText(ProgressFilePath);
            playerProgress = JsonUtility.FromJson<PlayerProgress>(dataAsJson);
            return true;
        }
        return false;
    }

    public void SaveProgress()
    {
        Debug.Log("SaveProgress");
        string dataAsJson = JsonUtility.ToJson(playerProgress, true);
        File.WriteAllText(ProgressFilePath, dataAsJson);
    }
}
