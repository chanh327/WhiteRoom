using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLight : MonoBehaviour
{
    // 0 : E, 1 : M, 2 : S, 3 :T, 4: C, 5:ending
    public GameObject[] PointLights;
    public Door endingDoor;
    void Start()
    {
        PlayerProgress playerProgress = GameManager.instance.playerProgress;
        endingDoor.State = DoorState.Locked;

        //Tutorial
        if (playerProgress.stage_clears[0] == false)
        {
            LevelLoader.instance.TutorialLoad();
        }
        else
        {
            LevelLoader.instance.SetLoadingScreenAlpha(new Color(0, 0, 0, 0));
        }

        for (int i = 0; i < PointLights.Length - 1; i++)
            PointLights[i].SetActive(true);
        PointLights[PointLights.Length - 1].SetActive(false);

        if (playerProgress.stage_clears[6] == true)
        {
            PointLights[PointLights.Length - 1].SetActive(true);
            endingDoor.State = DoorState.Open;
        }
        else if (playerProgress.IsAllClear == true)
        {
            endingDoor.State = DoorState.Open;
            for (int i = 0; i < PointLights.Length - 1; i++)
            {
                PointLights[i].SetActive(false);
            }
            PointLights[PointLights.Length - 1].SetActive(true);
        }
        else
        {
            for (int i = 1; i < playerProgress.stage_clears.Length; i++)
            {
                if (playerProgress.stage_clears[i])
                    PointLights[i - 1].SetActive(false);
            }
        }
    }
}
