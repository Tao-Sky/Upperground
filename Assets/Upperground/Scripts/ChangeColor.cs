using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ChangeColor : MonoBehaviour {

	public float intensity;
	private Material material;
	private float currentTime = 0f;
	private float time = 5f;
	public bool violet = false;

	// Creates a private material used to the effect
	void Awake ()
	{
		material = new Material( Shader.Find("Hidden/BWDiffuse") );
	}

	// Postprocess the image
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		if (intensity == 0)
		{
			Graphics.Blit (source, destination);
			return;
		}
		material.SetFloat("_bwBlend", intensity);
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
		Debug.Log ("coucou " + intensity);
		if (currentTime <= time)
		{
			currentTime += Time.deltaTime;
			intensity = Mathf.Lerp (0.9f, 0.1f, currentTime / time);
		}
		else
		{
			currentTime = 0f;
		}
	}
}