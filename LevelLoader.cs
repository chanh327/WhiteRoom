﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance = null;
    public GameObject loadingScreen;
    private Image loadingScreenImg;

    public GameObject setting;
    public GameObject restart;
    public GameObject select;
    public GameObject menu;

    private bool ing;

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
        ing = false;
        SettingOff();
    }

    public void LoadLevel(int sceneIndex)
    {
        if (!ing)
            StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void StartLoad(int sceneIndex)
    {
        StartCoroutine(StartLoadAsynchronously(sceneIndex));
    }

    public void SettingOn()
    {
        menu.SetActive(false);
        restart.SetActive(true);
        select.SetActive(true);
    }

    public void SettingOff()
    {
        menu.SetActive(false);
        restart.SetActive(false);
        select.SetActive(false);
    }

    public void LoadLevel()
    {
        if (!ing)
            StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        ing = true;
        yield return new WaitForSeconds(0.24f);
        loadingScreen.SetActive(true);
        Color fade = Color.black;
        fade.a = 0f;

        while (fade.a < 1)
        {
            loadingScreenImg.color = fade;
            fade.a += 0.04f;

            if (SoundManager.instance.source.volume > 0)
                SoundManager.instance.source.volume -= 0.04f;
            yield return null;
        }

        fade.a = 1f;
        loadingScreenImg.color = fade;
        SoundManager.instance.source.volume = 0;

        if (sceneIndex == 1 || sceneIndex == 2)
        {
            SettingOff();
        }
        else
        {
            SettingOn();
        }

        if (!setting.activeSelf)
        {
            setting.SetActive(true);
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            yield return null;
        }

        SoundManager.instance.PlayBGM(sceneIndex);

        while (fade.a > 0)
        {
            loadingScreenImg.color = fade;
            fade.a -= 0.04f;

            if (SoundManager.instance.source.volume < 1)
                SoundManager.instance.source.volume += 0.04f;

            yield return null;
        }

        fade.a = 0f;
        loadingScreenImg.color = fade;
        SoundManager.instance.source.volume = 1f;

        loadingScreen.SetActive(false);
        ing = false;
    }

    IEnumerator StartLoadAsynchronously(int sceneIndex)
    {
        loadingScreen.SetActive(true);
        Color fade = new Color32(35, 31, 32, 255);

        fade.a = 1f;
        loadingScreenImg.color = fade;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            yield return null;
        }

        SoundManager.instance.PlayBGM(sceneIndex);

        while (fade.a > 0)
        {
            loadingScreenImg.color = fade;
            fade.a -= 0.04f;
            yield return null;
        }

        fade.a = 0f;
        loadingScreenImg.color = fade;
        loadingScreen.SetActive(false);
    }

    public void SetLoadingScreenAlpha(Color c)
    {
        loadingScreenImg.color = c;
    }
}
