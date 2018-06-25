using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public RectTransform[] stages;
    private float stageSizeX = 340f;

    private bool[] progress;

    void Start()
    {
        RectTransform content = GetComponent<RectTransform>();
        content.sizeDelta = new Vector2(stages.Length * stageSizeX, 0f);

        // test
        progress = new bool[1];
        progress[0] = true;
        //

        SetMenu();
    }

    private void SetMenu()
    {
        for (int i = 1; i < progress.Length; i++)
        {
            if (progress[i - 1])
            {
                stages[i - 1].GetComponent<Button>().enabled = true;
            }
            else
            {
                stages[i - 1].GetComponent<Button>().enabled = false;
            }
        }
    }
}
