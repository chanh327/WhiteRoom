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
    private Image[] answerPanelImages;
    private Text[] answerPanelTexts;
    private Image[] nextPanelImages;
    private Text[] nextPanelTexts;

    private Button enter;

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
            panels[1] = GameObject.Find("Answer Panel 3");
            panels[2] = GameObject.Find("Next Panel 3");
            enter = GameObject.Find("Enter 3").GetComponent<Button>();
        }
        else if (this.transform.name == "Secret 4")
        {
            panels[0] = GameObject.Find("Prev Panel 4");
            panels[1] = GameObject.Find("Answer Panel 4");
            panels[2] = GameObject.Find("Next Panel 4");
            enter = GameObject.Find("Enter 4").GetComponent<Button>();
        }
        else if (this.transform.name == "Secret 5")
        {
            panels[0] = GameObject.Find("Prev Panel 5");
            panels[1] = GameObject.Find("Answer Panel 5");
            panels[2] = GameObject.Find("Next Panel 5");
            enter = GameObject.Find("Enter 5").GetComponent<Button>();
        }
        else
        {
            Debug.LogError("Secret space count 설정 에러: " + this.transform.name);
        }

        prevPanelImages = panels[0].GetComponentsInChildren<Image>();
        prevPanelTexts = panels[0].GetComponentsInChildren<Text>();
        answerPanelImages = panels[1].GetComponentsInChildren<Image>();
        answerPanelTexts = panels[1].GetComponentsInChildren<Text>();
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
        this.answer = answer;
        this.list0 = list0;
        this.list1 = list1;
        this.list2 = list2;
        this.door = door;
        SetSecret();
    }

    public void UpdateSecret(int[] answer, string[] list0, string[] list1, string[] list2, string[] list3, Door door)
    {
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

        for (int i = 0; i < answer.Length; i++)
        {
            if (i == panelNum)
            {
                prevPanelTexts[panelNum].text = list0[prev];
                answerPanelTexts[panelNum].text = list0[cur];
                nextPanelTexts[panelNum].text = list0[next];
                break;
            }
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
                    prevPanelImages[panelNum + 1].color = new Color32(192, 192, 192, 255);
                    answerPanelImages[panelNum + 1].color = new Color32(255, 255, 255, 255);
                    nextPanelImages[panelNum + 1].color = new Color32(192, 192, 192, 255);
                }
                else
                {
                    prevPanelImages[panelNum + 1].color = new Color32(255, 255, 255, 255);
                    answerPanelImages[panelNum + 1].color = new Color32(192, 192, 192, 255);
                    nextPanelImages[panelNum + 1].color = new Color32(255, 255, 255, 255);
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
    }

    public void NextButton(int panelNum)
    {
        curNumber[panelNum]--;
        if (curNumber[panelNum] < 0)
            curNumber[panelNum] = list0.Length - 1;

        SetPanelTexts(panelNum);
    }

    public void MarkButton()
    {
        StartCoroutine(CoMarkButton(MarkAnswer()));
    }

    private IEnumerator CoMarkButton(bool result)
    {

        if (result)
        {
            door.state = DoorState.Open;
            //enter = new Color32(255, 255, 153, 255);
            yield return new WaitForSeconds(0.3f);
            CloseSecret();
        }
        /*else
        {
            //enter.color = new Color32(255, 0, 0, 255);
            yield return new WaitForSeconds(0.3f);
            //enter.color = new Color32(255, 255, 255, 255);
        }*/
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

        return result;
    }

    public void CloseSecret()
    {
        touchController.enabled = true;
        this.gameObject.SetActive(false);
    }
}
