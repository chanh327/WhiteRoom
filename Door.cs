using UnityEngine;
using System.Collections;

public enum DoorState {
    Open, Secret, Locked
}
public class Door : MonoBehaviour
{
    private Transform doorTransform;
    private Transform fStep;
    private Transform bStep;
    public DoorState state;
    private Secret secret;
    private Animator doorAnim;
    int openhash = Animator.StringToHash("Open");

    private void Start()
    {
        doorAnim = GetComponentInChildren<Animator>();   
        doorTransform = transform.transform;
        fStep = transform.Find("FStep").transform;
        bStep = transform.Find("BStep").transform;

        if(HasComponent<Locked>())
            state = DoorState.Locked;
        else if(HasComponent<Secret>())
            state = DoorState.Secret;
        else
            state = DoorState.Open;

    }
    public void Open()
    {
        doorAnim.SetTrigger(openhash);
    }

    public void Touched()
    {

    }

    public Transform DoorTransform
    {
        get { return doorTransform;}
    }

    public Transform FStep 
    {
        get { return fStep; }
    }

    public Transform BStep
    {
        get { return bStep; }
    }
    public bool HasComponent<T> ()
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
}