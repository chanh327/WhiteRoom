using UnityEngine;
using System.Collections;

public class PlayerController: MonoBehaviour
{
    //public static PlayerController instance = null;
    private GameObject player;
    private bool hasKey;

    public void MoveTo(Door door) 
    {
        
    }

    public bool HasKey
    {
        get { return hasKey; }
        set
        {
            if (hasKey != value)
                hasKey = value;
        }
    }
}
