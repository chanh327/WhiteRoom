using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public PlayerProgress playerProgress;
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
        LoadProgress();
    }
    void Start()
    {
        //playerProgress.Reset();
        //SaveProgress(1);
    }
    public void QuitGame()
    {
        Application.Quit();
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
    public void SaveProgress(int sceneidx)
    {
        playerProgress.Finish(sceneidx - 1);
        string dataAsJson = JsonUtility.ToJson(playerProgress, true);
        File.WriteAllText(ProgressFilePath, dataAsJson);
    }
}
