using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    float endTime;
    float endPos;
    float upSpeed;
    RectTransform rect;

    private GameObject setting;
    private GameObject skip;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        setting = GameObject.Find("Setting");
        skip = GameObject.Find("Skip");
    }

    void Start()
    {
        setting.SetActive(false);

        endTime = 50f;
        endPos = rect.sizeDelta.y - 720;
        upSpeed = 0.95f;

        StartCoroutine(UpCredits());
    }

    void LateUpdate()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            upSpeed = 5;
        }
        else
        {
            upSpeed = 0.95f;
        }
#elif UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            upSpeed = 5;
        }
        else
        {
            upSpeed = 0.95f;
        }
#endif
    }

    private IEnumerator UpCredits()
    {
        float upTemp = 0;
        float time = 0;
        while (rect.localPosition.y < endPos)
        {
            time += Time.deltaTime;
            upTemp += upSpeed;

            rect.localPosition = Vector2.Lerp(rect.localPosition, new Vector2(0, upTemp), time);

            yield return null;
        }
        GameManager.instance.SaveProgress(SceneManager.GetActiveScene().buildIndex);
        LevelLoader.instance.LoadLevel(0);
    }

    public void Skip()
    {
        LevelLoader.instance.LoadLevel(0);
    }
}
