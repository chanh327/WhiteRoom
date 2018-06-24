using UnityEngine;
using System.Collections;

public enum DoorState
{
    Open, Secret, Locked
}
public class Door : MonoBehaviour
{
    private Locked locked;
    private SecretController secretController;

    private Transform doorTransform;
    private Transform fStep;
    private Transform bStep;
    public DoorState state;
    private Animator doorAnim;
    int openhash = Animator.StringToHash("Open");

    private void Start()
    {
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
        else
        {
            state = DoorState.Open;
        }

    }
    public void Open()
    {
        doorAnim.SetTrigger(openhash);
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
        // locked 상태 확인
    }

    public void SecretCall(Door door)
    {
        secretController.OpenSecret(door);
    }
}