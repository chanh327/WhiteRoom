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
#if UNITY_EDITOR
            return Path.Combine(Application.dataPath, "Resources/playerProgress.json");
#elif UNITY_ANDROID
            return Path.Combine(Application.persistentDataPath, "playerProgress.json");
#endif
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

        Application.targetFrameRate = 50;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public bool LoadProgress()
    {
        string dataAsJson;
        if (File.Exists(ProgressFilePath))
        {
            dataAsJson = File.ReadAllText(ProgressFilePath);
        }
        else
        {
            TextAsset tempJson = Resources.Load("playerProgress") as TextAsset;
            dataAsJson = tempJson.ToString();
        }
        playerProgress = JsonUtility.FromJson<PlayerProgress>(dataAsJson);
        return true;
    }

    public void SaveProgress(int sceneidx)
    {
        playerProgress.Finish(sceneidx);
        string dataAsJson = JsonUtility.ToJson(playerProgress, true);
        File.WriteAllText(ProgressFilePath, dataAsJson);
    }
}
