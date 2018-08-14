using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    private GameObject menu;

    private bool effect;
    private Image effectImg;

    private bool music;
    private Image musicImg;

    void Awake()
    {
        menu = GameObject.Find("Menu");
        effectImg = GameObject.Find("Effect").GetComponent<Image>();
        musicImg = GameObject.Find("Music").GetComponent<Image>();
    }

    void Start()
    {
        effect = true;
        music = true;
        changeSoundColor(effectImg, new Color(0.125f, 0.125f, 0.125f));
        changeSoundColor(musicImg, new Color(0.125f, 0.125f, 0.125f));
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

    public void EffectOnOff()
    {
        if (effect)
        {
            changeSoundColor(effectImg, new Color(0.6875f, 0.6875f, 0.6875f));
            SoundManager.instance.EffectMute();
            effect = false;
        }
        else
        {
            changeSoundColor(effectImg, new Color(0.125f, 0.125f, 0.125f));
            SoundManager.instance.EffectUnMute();
            effect = true;
        }
    }

    public void MusicOnOff()
    {
        if (music)
        {
            changeSoundColor(musicImg, new Color(0.6875f, 0.6875f, 0.6875f));
            SoundManager.instance.MusicMute();
            music = false;
        }
        else
        {
            changeSoundColor(musicImg, new Color(0.125f, 0.125f, 0.125f));
            SoundManager.instance.MusicUnMute();
            music = true;
        }
    }

    public void changeSoundColor(Image img, Color c)
    {
        img.color = c;
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
