using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonColor : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public Color onColor;
    public Color idleColor;

    void Awake()
    {
        meshRenderer = transform.Find("Box001").GetComponent<MeshRenderer>();
    }

    void Start()
    {
        meshRenderer.material.color = idleColor;
    }

    public IEnumerator CoChangeColor(Color color)
    {
        float starTime = 0f;
        Color lastColor = meshRenderer.material.color;
        while (starTime < 1f)
        {
            starTime += Time.deltaTime * 1.0f;
            meshRenderer.material.color = Color.Lerp(lastColor, color, starTime);
            yield return new WaitForFixedUpdate();
        }
        meshRenderer.material.color = color;
    }

    public void TurnOnColor()
    {
        StartCoroutine(CoChangeColor(onColor));
    }

    public void TurnOffColor()
    {
        StartCoroutine(CoChangeColor(idleColor));
    }
}
