using UnityEngine;
using System.Collections;

public enum DoorState
{
    Open, Secret, Locked, DirLock
}

public class Door : MonoBehaviour
{
    private Locked locked;
    private SecretController secretController;
    private DirLockController dirLockController;

    private Transform doorTransform;
    private Transform fStep;
    private Transform bStep;
    private DoorState state;
    private Animator doorAnim;
    int fOpenHash = Animator.StringToHash("FOpen");
    int bOpenHash = Animator.StringToHash("BOpen");
    int passedHash = Animator.StringToHash("Passed");
    private DoorSound doorSound;

    void Awake()
    {
        doorSound = GetComponentInChildren<DoorSound>();
        doorAnim = GetComponentInChildren<Animator>();
        doorTransform = transform.transform;
        fStep = transform.Find("FStep").transform;
        bStep = transform.Find("BStep").transform;

        if (HasComponent<Locked>())
        {
            state = DoorState.Locked;
            locked = GetComponent<Locked>();
        }
        else if (HasComponent<SecretController>())
        {
            state = DoorState.Secret;
            secretController = GetComponent<SecretController>();
        }
        else if (HasComponent<DirLockController>())
        {
            state = DoorState.DirLock;
            dirLockController = GetComponent<DirLockController>();
        }
        else
        {
            state = DoorState.Open;
        }
    }

    public DoorState State
    {
        get { return state; }
        set
        {
            state = value;
        }
    }

    public DoorSound DoorSound
    {
        get { return doorSound; }
    }
    public void FOpen()
    {
        doorAnim.SetTrigger(fOpenHash);
    }

    public void BOpen()
    {
        doorAnim.SetTrigger(bOpenHash);
    }

    public void Close()
    {
        doorAnim.SetTrigger(passedHash);
    }

    public Transform DoorTransform
    {
        get { return doorTransform; }
    }

    public Transform FStep
    {
        get { return fStep; }
    }

    public Transform BStep
    {
        get { return bStep; }
    }
    public bool HasComponent<T>()
    {
        Component[] cs = (Component[])GetComponents(typeof(Component));
        foreach (Component c in cs)
        {
            if (c.GetType().Equals(typeof(T)))
            {
                return true;
            }

        }
        return false;
    }

    public void LockedCall()
    {
        doorSound.PlayLockedSound();
    }

    public void SecretCall(Door door)
    {
        doorSound.PlayLockedSound();
        secretController.OpenSecret(door);
    }

    public void DirLockCall(Door door)
    {
        doorSound.PlayLockedSound();
        dirLockController.OpenDirLock(door);
    }
}