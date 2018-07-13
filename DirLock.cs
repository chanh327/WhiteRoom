using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirLock : MonoBehaviour
{
    private int[] answer;
    private Queue<int> curDir;

    public Image enter;

    TouchController touchController;
    Door door;

    void Start()
    {
        curDir = new Queue<int>();
        touchController = Camera.main.GetComponent<TouchController>();
        this.gameObject.SetActive(false);
    }

    public void UpdateDirLock(int[] answer, Door door)
    {
        touchController.enabled = false;
        this.answer = answer;
        this.door = door;
    }

    public void Push(int num)
    {
        curDir.Enqueue(num);
    }

    public void Reset()
    {
        curDir.Clear();
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
            FadeInOutColor(enter, Color.yellow);
            yield return new WaitForSeconds(0.6f);
            CloseDirLock();
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
        int[] curDirInt = curDir.ToArray();

        if (answer.Length == curDirInt.Length)
        {
            for (int i = 0; i < answer.Length; i++)
            {
                if (answer[i] != curDirInt[i])
                {
                    break;
                }

                if (i == answer.Length - 1)
                {
                    result = true;
                }
            }
        }

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

    public void CloseDirLock()
    {
        touchController.enabled = true;
        this.gameObject.SetActive(false);
    }
}
