using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		PlayerProgress playerProgress = GameManager.instance.playerProgress;
		if (playerProgress.stage_clears[0] == false)
        {
            LevelLoader.instance.StartLoad(2);
        }
		else
		{
			LevelLoader.instance.StartLoad(1);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
