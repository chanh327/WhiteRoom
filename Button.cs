using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Item {
	private Animator buttonAnim;
	int idleHash = Animator.StringToHash("Base Layer.IdleButton");
	int onHash = Animator.StringToHash("Base Layer.TurnOnButton");

	void Awake()
	{
		buttonAnim = GetComponent<Animator>();
		lockeds = new List<Locked>();
		
	}
	void Start()
	{
		buttonAnim.SetBool("ison", false);
		condition = false;
	}

	public bool Ison
	{
		get {return condition;}
		set 
		{
			AnimatorStateInfo currentState = buttonAnim.GetCurrentAnimatorStateInfo(0);
			if(value == true && currentState.fullPathHash == idleHash)
			{
				buttonAnim.SetBool("ison", value);
				condition = value;
			}
			else if(value == false && currentState.fullPathHash == onHash)
			{
				buttonAnim.SetBool("ison", value);
				condition = value;
			}
			Debug.Log(condition);
		}
	} 

	public override void Touched()
	{
		Ison = !condition;
		for(int i=0; i<lockeds.Count; i++)
		{
			Debug.Log(condition);
			lockeds[i].ButtonChanged();
		}
	}
}
