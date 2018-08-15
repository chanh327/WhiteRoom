using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Credits : MonoBehaviour
{
	float endTime;
	float endPos;
	float upSpeed;
    RectTransform rect;

	private GameObject setting;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
		setting = GameObject.Find("Setting");
    }

    void Start()
    {
		setting.SetActive(false);

		endTime = 50f;
		endPos = rect.sizeDelta.y - 720;
		upSpeed = 1f;

        StartCoroutine(UpCredits());
    }

    private IEnumerator UpCredits()
    {
		float upTemp = 0;
        float time = 0;
        while (time < endTime)
        {
			time += Time.deltaTime;
            upTemp += upSpeed;
			if(rect.localPosition.y < endPos)
				rect.localPosition = Vector2.Lerp(rect.localPosition, new Vector2(0, upTemp), time);

            yield return null;
        }

		LevelLoader.instance.LoadLevel(0);
    }

	public void Skip()
	{
		LevelLoader.instance.LoadLevel(0);
	}
}
