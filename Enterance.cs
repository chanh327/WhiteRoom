using UnityEngine;
using System.Collections;

public class Enterance : Teleport
{
   protected override void Event()
   {
       LevelLoader.instance.LoadLevel(stageNum);
   }
}