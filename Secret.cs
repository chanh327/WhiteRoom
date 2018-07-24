using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Secret : MonoBehaviour
{
    private GameObject secret;

    private int[] answer;
    private int[] curNumber = new int[5] { 0, 0, 0, 0, 0 };

    private string[] list0;
    private string[] list1;
    private string[] list2;
    private string[] list3;
    private string[] list4;

    private GameObject[] panels;
    private Image[] prevPanelImages;
    private Text[] prevPanelTexts;
    private Image[] centerPanelImages;
    private Text[] centerPanelTexts;
    private Image[] nextPanelImages;
    private Text[] nextPanelTexts;

    public Image enter;

    TouchController touchController;
    Door door;

    void Start()
    {
        Init();
        touchController = Camera.main.GetComponent<TouchController>();
        this.gameObject.SetActive(false);
    }

    private void Init()
    {
        panels = new GameObject[3];
        if (this.transform.name == "Secret 3")
        {
            panels[0] = GameObject.Find("Prev Panel 3");
            panels[1] = GameObject.Find("Center Panel 3");
            panels[2] = GameObject.Find("Next Panel 3");
        }
        else if (this.transform.name == "Secret 4")
        {
            panels[0] = GameObject.Find("Prev Panel 4");
            panels[1] = GameObject.Find("Center Panel 4");
            panels[2] = GameObject.Find("Next Panel 4");
        }
        else if (this.transform.name == "Secret 5")
        {
            panels[0] = GameObject.Find("Prev Panel 5");
            panels[1] = GameObject.Find("Center Panel 5");
            panels[2] = GameObject.Find("Next Panel 5");
        }
        else
        {
            Debug.LogError("Secret space count 설정 에러: " + this.transform.name);
        }

        prevPanelImages = panels[0].GetComponentsInChildren<Image>();
        prevPanelTexts = panels[0].GetComponentsInChildren<Text>();
        centerPanelImages = panels[1].GetComponentsInChildren<Image>();
        centerPanelTexts = panels[1].GetComponentsInChildren<Text>();
        nextPanelImages = panels[2].GetComponentsInChildren<Image>();
        nextPanelTexts = panels[2].GetComponentsInChildren<Text>();
    }

    private void SetSecret()
    {
        for (int i = 0; i < answer.Length; i++)
        {
            SetPanelTexts(i);
        }
    }

    public void UpdateSecret(int[] answer, string[] list0, string[] list1, string[] list2, Door door)
    {
        touchController.enabled = false;
        this.answer = answer;
        this.list0 = list0;
        this.list1 = list1;
        this.list2 = list2;
        this.door = door;
        SetSecret();
    }

    public void UpdateSecret(int[] answer, string[] list0, string[] list1, string[] list2, string[] list3, Door door)
    {
        touchController.enabled = false;
        this.answer = answer;
        this.list0 = list0;
        this.list1 = list1;
        this.list2 = list2;
        this.list3 = list3;
        this.door = door;
        SetSecret();
    }

    public void UpdateSecret(int[] answer, string[] list0, string[] list1, string[] list2, string[] list3, string[] list4, Door door)
    {
        touchController.enabled = false;
        this.answer = answer;
        this.list0 = list0;
        this.list1 = list1;
        this.list2 = list2;
        this.list3 = list3;
        this.list4 = list4;
        this.door = door;
        SetSecret();
    }

    private void SetPanelTexts(int panelNum)
    {
        int prev = curNumber[panelNum] - 1;
        if (prev < 0)
            prev = list0.Length - 1;
        int cur = curNumber[panelNum];
        int next = curNumber[panelNum] + 1;
        if (next > list0.Length - 1)
            next = 0;

        if (panelNum == 0)
        {
            prevPanelTexts[panelNum].text = list0[prev];
            centerPanelTexts[panelNum].text = list0[cur];
            nextPanelTexts[panelNum].text = list0[next];
        }
        else if (panelNum == 1)
        {
            prevPanelTexts[panelNum].text = list1[prev];
            centerPanelTexts[panelNum].text = list1[cur];
            nextPanelTexts[panelNum].text = list1[next];
        }
        else if (panelNum == 2)
        {
            prevPanelTexts[panelNum].text = list2[prev];
            centerPanelTexts[panelNum].text = list2[cur];
            nextPanelTexts[panelNum].text = list2[next];
        }
        else if (panelNum == 3)
        {
            prevPanelTexts[panelNum].text = list3[prev];
            centerPanelTexts[panelNum].text = list3[cur];
            nextPanelTexts[panelNum].text = list3[next];
        }
        else if (panelNum == 4)
        {
            prevPanelTexts[panelNum].text = list4[prev];
            centerPanelTexts[panelNum].text = list4[cur];
            nextPanelTexts[panelNum].text = list4[next];
        }

        SetPanelImages(panelNum);
    }

    private void SetPanelImages(int panelNum)
    {
        for (int i = 0; i < answer.Length; i++)
        {
            if (i == panelNum)
            {
                if (curNumber[panelNum] % 2 == 0)
                {
                    prevPanelImages[panelNum].color = new Color32(192, 192, 192, 255);
                    centerPanelImages[panelNum].color = new Color32(255, 255, 255, 255);
                    nextPanelImages[panelNum].color = new Color32(192, 192, 192, 255);
                }
                else
                {
                    prevPanelImages[panelNum].color = new Color32(255, 255, 255, 255);
                    centerPanelImages[panelNum].color = new Color32(192, 192, 192, 255);
                    nextPanelImages[panelNum].color = new Color32(255, 255, 255, 255);
                }
                break;
            }
        }
    }

    public void PrevButton(int panelNum)
    {
        curNumber[panelNum]++;
        if (curNumber[panelNum] > list0.Length - 1)
            curNumber[panelNum] = 0;

        SetPanelTexts(panelNum);
        door.DoorSound.PlayLockButtonPushSound();
    }

    public void NextButton(int panelNum)
    {
        curNumber[panelNum]--;
        if (curNumber[panelNum] < 0)
            curNumber[panelNum] = list0.Length - 1;

        SetPanelTexts(panelNum);
        door.DoorSound.PlayLockButtonPushSound();
    }

    public void MarkButton()
    {
        StartCoroutine(CoMarkButton(MarkAnswer()));
    }

    private IEnumerator CoMarkButton(bool result)
    {
        if (result)
        {
            door.State = DoorState.Open;
            FadeInOutColor(enter, Color.yellow);
            yield return new WaitForSeconds(0.6f);

            // 숫자 초기화
            for (int i = 0; i < curNumber.Length; i++)
                curNumber[i] = 0;

            CloseSecret();
        }
        else
        {
            FadeInOutColor(enter, Color.red);
            yield return new WaitForSeconds(0.4f);
            FadeInOutColor(enter, Color.white);
        }
    }

    private bool MarkAnswer()
    {
        bool result = false;

        for (int i = 0; i < answer.Length; i++)
        {
            if (answer[i] != curNumber[i])
            {
                break;
            }

            if (i == answer.Length - 1)
            {
                result = true;
            }
        }
        //실패 소리 재생
        door.DoorSound.PlayFailSound();
        return result;
    }

    private void FadeInOutColor(Image img, Color color)
    {
        StartCoroutine(CoFadeInOutColor(img, color));
    }

    private IEnumerator CoFadeInOutColor(Image img, Color color)
    {
        float duration = 0.1f;
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float r = Mathf.Lerp(img.color.r, color.r, currentTime / duration);
            float g = Mathf.Lerp(img.color.g, color.g, currentTime / duration);
            float b = Mathf.Lerp(img.color.b, color.b, currentTime / duration);
            enter.color = new Color(r, g, b, 1f);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield break;
    }

    public void CloseSecret()
    {
        enter.color = Color.white;
        touchController.enabled = true;
        this.gameObject.SetActive(false);
    }
}
