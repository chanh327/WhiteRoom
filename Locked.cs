using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locked : MonoBehaviour {
	public Item[] items;
	public bool[] conditions;
	private Door door;
	void Awake()
	{
		door = transform.GetComponent<Door>();
	}
	void Start()
	{
		for(int i=0; i<items.Length; i++)
		{
			items[i].lockeds.Add(transform.GetComponent<Locked>());
		}	
	}

	private bool CheckConditions()
	{
		for(int i=0; i<conditions.Length; i++)
		{
			if(items[i].Condition != conditions[i])
				return false;
		}
		return true;
	}

	public void ButtonChanged()
	{
		if(CheckConditions())
		{
			door.State = DoorState.Open;
		}
		else
			door.State = DoorState.Locked;
	}
}
