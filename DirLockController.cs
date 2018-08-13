using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirLockController : MonoBehaviour
{
    //up: 0, right: 1, down: 2, left: 3
    public int[] answer;

    private DirLock dirLock;

    void Awake()
    {
        dirLock = GameObject.Find("Direction Lock").GetComponent<DirLock>();
    }

    public void OpenDirLock(Door door)
    {
        dirLock.gameObject.SetActive(true);
        dirLock.UpdateDirLock(answer, door);
    }
}