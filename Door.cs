using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    private Transform doorTransform;
    private Transform fStep;
    private Transform bStep;
    private bool locked;
    private Transform door;
    private Secret secret;
    private Animator doorAnim;
    int openhash = Animator.StringToHash("Open");


    public bool initialLocked = false;

    private void Start()
    {
        doorAnim = GetComponentInChildren<Animator>();   
        doorTransform = transform.transform;
        door = transform.Find("Door");
        fStep = transform.Find("FStep").transform;
        bStep = transform.Find("BStep").transform;
        locked = initialLocked;
    }

    public bool Locked
    {
        get { return this.locked; }
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
}