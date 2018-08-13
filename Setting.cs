using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    private TouchController touchController;

    void Start()
    {
        touchController = Camera.main.GetComponent<TouchController>();
        gameObject.SetActive(false);
    }

    public void OpenSetting()
    {
        touchController.enabled = false;
        gameObject.SetActive(true);
    }

    public void CloseSetting()
    {
        touchController.enabled = true;
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        GameManager.instance.QuitGame();
    }
}
