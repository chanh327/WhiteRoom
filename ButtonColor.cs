using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonColor : MonoBehaviour {

	private MeshRenderer renderer;
	public Color onColor;
	public Color idleColor;

	void Awake()
	{
		renderer = transform.Find("Box001").GetComponent<MeshRenderer>();
	}
	void Start () {
		renderer.material.color = idleColor;
	}

	public IEnumerator CoChangeColor(Color color)
	{
		float starTime = 0f;
		Color lastColor = renderer.material.color;
		while(starTime < 1f)
		{
			starTime += Time.deltaTime * 1.0f;
			renderer.material.color = Color.Lerp(lastColor, color, starTime);
			 yield return new WaitForFixedUpdate();
		}
		renderer.material.color = color;
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
