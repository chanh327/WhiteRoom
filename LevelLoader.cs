using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance = null;
    public GameObject loadingScreen;
    private Image loadingScreenImg;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        loadingScreenImg = loadingScreen.GetComponent<Image>();
    }
    void Start()
    {
        loadingScreen.SetActive(false);
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
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
        yield return new WaitForSeconds(0.1f);
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
    }
}
