using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    private GameObject menu;

    private bool sound;
    private Image soundImg;

    void Awake()
    {
        menu = GameObject.Find("Menu");
        soundImg = GameObject.Find("Sound").GetComponent<Image>();
    }

    void Start()
    {
        sound = true;
        changeSoundColor(new Color(0.125f, 0.125f, 0.125f));
        menu.SetActive(false);
    }

    public void MenuOnOff()
    {
        if (menu.activeSelf)
        {
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
        }
    }

    public void SoundOnOff()
    {
        if (sound)
        {
            changeSoundColor(new Color(0.6875f, 0.6875f, 0.6875f));
            SoundManager.instance.Mute();
            sound = false;
        }
        else
        {
            changeSoundColor(new Color(0.125f, 0.125f, 0.125f));
            SoundManager.instance.UnMute();
            sound = true;
        }
    }

    public void changeSoundColor(Color c)
    {
        soundImg.color = c;
    }

    public void Restart()
    {
        menu.SetActive(false);
        LevelLoader.instance.LoadLevel();
    }

    public void GoSelect()
    {
        menu.SetActive(false);
        LevelLoader.instance.LoadLevel(0);
    }
}
