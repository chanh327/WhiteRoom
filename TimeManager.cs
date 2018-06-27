using System.Collections;
using UnityEngine;

public class TimeManager: MonoBehaviour
{
    public static TimeManager instance = null;
    private float recordTime;

    private bool ison;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Debug.Log("timeStart");
    }
    void Update()
    {
        if(ison)
        {
            Debug.Log(recordTime);
            recordTime += Time.deltaTime;
        }
    }

    public void StartRecord()
    {
        recordTime = 0;
        Debug.Log("start");
        recordTime = 0f;
        ison = true;
    }

    public float StopRecord()
    {
        Debug.Log("stop");
        ison = false;
        return recordTime;
    }
}