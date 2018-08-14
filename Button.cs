using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Item
{
    private Animator buttonAnim;
    int offHash = Animator.StringToHash("Base Layer.OffButton");
    int onHash = Animator.StringToHash("Base Layer.OnButton");
    public AudioClip clip;
    private AudioSource source;

    void Awake()
    {
        buttonAnim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        lockeds = new List<Locked>();

    }

    void Start()
    {
        buttonAnim.SetBool("ison", false);
        condition = false;
    }

    public bool Ison
    {
        get { return condition; }
        set
        {
            AnimatorStateInfo currentState = buttonAnim.GetCurrentAnimatorStateInfo(0);
            if (value == true && currentState.fullPathHash == offHash)
            {
                buttonAnim.SetBool("ison", value);
                source.PlayOneShot(clip);
                condition = value;
            }
            else if (value == false && currentState.fullPathHash == onHash)
            {
                buttonAnim.SetBool("ison", value);
                source.PlayOneShot(clip);
                condition = value;
            }
            //Debug.Log(condition);
        }
    }

    public override void Touched()
    {
        Ison = !condition;
        for (int i = 0; i < lockeds.Count; i++)
        {
            //Debug.Log(condition);
            lockeds[i].ButtonChanged();
        }
    }
}
