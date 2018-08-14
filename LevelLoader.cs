using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance = null;
    public GameObject loadingScreen;
    private Image loadingScreenImg;

    private GameObject restart;
    private GameObject select;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        loadingScreenImg = loadingScreen.GetComponent<Image>();

        restart = GameObject.Find("Restart");
        select = GameObject.Find("Select");
        SettingOff();
    }

    void Start()
    {
        loadingScreen.SetActive(false);
    }

    public void LoadLevel(int sceneIndex)
    {
        if (sceneIndex == 0)
            SettingOff();
        else
            SettingOn();
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void TutorialLoad()
    {
        StartCoroutine(TutorialLoadAsynchronously(1));
    }

    public void SettingOn()
    {
        restart.SetActive(true);
        select.SetActive(true);
    }

    public void SettingOff()
    {
        restart.SetActive(false);
        select.SetActive(false);
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        yield return new WaitForSeconds(0.24f);
        Color fade = Color.black;
        fade.a = 0f;

        loadingScreen.SetActive(true);
        while (fade.a < 1)
        {
            loadingScreenImg.color = fade;
            fade.a += 0.04f;
            yield return null;
        }

        fade.a = 1f;
        loadingScreenImg.color = fade;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            yield return null;
        }

        while (fade.a > 0)
        {
            loadingScreenImg.color = fade;
            fade.a -= 0.03f;
            yield return null;
        }

        fade.a = 0f;
        loadingScreenImg.color = fade;
        loadingScreen.SetActive(false);
        SoundManager.instance.PlayBGM(sceneIndex);
    }

    IEnumerator TutorialLoadAsynchronously(int sceneIndex)
    {
        Color fade = Color.black;

        loadingScreen.SetActive(true);

        fade.a = 1f;
        loadingScreenImg.color = fade;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            yield return null;
        }

        while (fade.a > 0)
        {
            loadingScreenImg.color = fade;
            fade.a -= 0.03f;
            yield return null;
        }

        fade.a = 0f;
        loadingScreenImg.color = fade;
        loadingScreen.SetActive(false);
        SoundManager.instance.PlayBGM(sceneIndex);
    }
}
