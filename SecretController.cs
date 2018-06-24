using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretController : MonoBehaviour
{
    public int[] answer;
    public string[] list0;
    public string[] list1;
    public string[] list2;
    public string[] list3;
    public string[] list4;

    private Secret secret;

    void Awake()
    {
        if (answer.Length == 3)
            secret = GameObject.Find("Secret 3").GetComponent<Secret>();
        else if (answer.Length == 4)
            secret = GameObject.Find("Secret 4").GetComponent<Secret>();
        else if (answer.Length == 5)
            secret = GameObject.Find("Secret 5").GetComponent<Secret>();
    }

    public void OpenSecret(Door door)
    {
        secret.gameObject.SetActive(true);
        if (answer.Length == 3)
            secret.UpdateSecret(answer, list0, list1, list2, door);
        else if (answer.Length == 4)
            secret.UpdateSecret(answer, list0, list1, list2, list3, door);
        else if (answer.Length == 5)
            secret.UpdateSecret(answer, list0, list1, list2, list3, list4, door);
    }
}
