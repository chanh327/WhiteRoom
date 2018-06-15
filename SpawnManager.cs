using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public PlayerController player;
    private Image blackScreen;
    private const float blackoutTime = 3.0f;

    void Start()
    {
        blackScreen = transform.GetComponentInChildren<Image>();
        blackScreen.enabled = false;

        //StartCoroutine(SpawnPlayer());
    }

    public IEnumerator SpawnPlayer()
    {
        int randomNumber = Random.Range(0, spawnPoints.Length);
        StartCoroutine(LoadBlackScreen());
        player.Teleport(spawnPoints[randomNumber]);
        yield return new WaitForSeconds(blackoutTime);
        //Touch Disable, Enable 기능 구현 필
    }
    private IEnumerator LoadBlackScreen()
    {
        blackScreen.enabled = true;
        yield return new WaitForSeconds(blackoutTime);
        blackScreen.enabled = false;
    }

    public void SpawnPlayer(Transform[] points)
    {
        
    }

    
}
