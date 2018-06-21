using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {
	protected bool condition;
	
	public List<Locked> lockeds;
	public abstract void Touched();
	public bool Condition{
		get { return condition;}
	}
}
