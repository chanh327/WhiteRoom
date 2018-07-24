using System.Collections;
using UnityEngine;

public class Turntable : MonoBehaviour{
    //GameObjet : TurnTable
    //Tag : Turntable
    private Animator turntableAnim;
    private bool isPlaying;
    int turningHash = Animator.StringToHash("Base Layer.Turning");
    int idleHash = Animator.StringToHash("Base Layer.TurntableIdle");

    void Awake()
    {
        turntableAnim = this.GetComponent<Animator>();
    }

    void Start()
    {
        isPlaying = SoundManager.instance.MusicOn;
        if(isPlaying)
            turntableAnim.SetBool("isPlaying", true);
    }



    public bool IsPlaying
    {
        get { return isPlaying;}
        set 
        {
            AnimatorStateInfo currentState = turntableAnim.GetCurrentAnimatorStateInfo(0);
            if(value == true && currentState.fullPathHash == idleHash)
            {
                turntableAnim.SetBool("isPlaying", true);
                isPlaying = true;
                SoundManager.instance.PlayMusic();
            }
            else if(value == false && currentState.fullPathHash == turningHash)
            {
                turntableAnim.SetBool("isPlaying", false);
                isPlaying = false;
                SoundManager.instance.StopMusic();
            }
        }
    }

    public void Touched()
    {
        IsPlaying = !isPlaying;
    }
}