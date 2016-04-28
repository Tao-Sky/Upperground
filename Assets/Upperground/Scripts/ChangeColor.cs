using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ChangeColor : MonoBehaviour {

	public float valuePurple;
	public float valueRed;
	public float valueBlue;
	public float valueGreen;
	private Material material;
	private float currentTime = 0f;
	private float time = 5f;
	public bool violet = false;
	public bool red = false;
	public bool blue = false;
	public bool green = false;


	// Creates a private material used to the effect
	void Awake ()
	{
		valuePurple = 0.8f;
		valueRed = 0.8f;
		valueBlue = 0.8f;
		valueGreen = 0.8f;
		material = new Material( Shader.Find("Hidden/BWDiffuse") );
	}

	// Postprocess the image
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
/*		if (valuePurple == 0)
		{
			Graphics.Blit (source, destination);
			return;
		}*/
		material.SetFloat("_bwBlendV", valuePurple);
		material.SetFloat("_bwBlendR", valueRed);
		material.SetFloat("_bwBlendB", valueBlue);
		material.SetFloat("_bwBlendG", valueGreen);
		Graphics.Blit (source, destination, material);
	}
		
	void Update()
	{
		if(violet)
		{
			Purple();
		}
	}

	public void Purple()
	{
		if (currentTime <= time)
		{
			currentTime += Time.deltaTime;
			valuePurple = Mathf.Lerp (0.9f, 0.1f, currentTime / time);
		}
		else
		{
			currentTime = 0f;
		}
	}
}