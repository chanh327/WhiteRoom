using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {
	private bool ison;
	private Animator buttonAnim;

	void Awake()
	{
		buttonAnim = GetComponent<Animator>();
	}
	void Start()
	{
		Ison = false;
	}

	public bool Ison
	{
		get {return ison;}
		set 
		{
			Debug.Log(value);
			buttonAnim.SetBool("ison", value);
			ison = value;
		}
	}
}
