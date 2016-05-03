using UnityEngine;
using System.Collections;

public class DisplayHUD : MonoBehaviour {

	public CanvasRenderer roue;
	public CanvasRenderer socle;

	private float currentTime = 0f;
	private float time = 2f;
	private float time2 = 3f;

	private bool fadeIn = false;
	private bool fadeOut = false;

	// Use this for initialization
	void Awake () 
	{
		roue.SetColor (new Color32 (255, 255, 255, 0));
		socle.SetColor (new Color32 (255, 255, 255, 0));
	}

	// Update is called once per frame
	void Update () 
	{
		if((Input.GetAxis("gachette gauche") > 0.2 || Input.GetAxis("gachette droite") > 0.2) && GameObject.Find("Sha").GetComponent<FollowPlayer>().PowersAvailable)
		{
			fadeIn=true;
		}
		
		if (fadeIn) 
		{
			roue.SetColor (new Color32 (255, 255, 255, 255));
			socle.SetColor (new Color32 (255, 255, 255, 255));

			currentTime = 0f;
			fadeIn = false;
			fadeOut = true;
		}

		if (fadeOut) 
		{
			if(currentTime <= time) 
			{
				currentTime += Time.deltaTime;
			}
			else if(currentTime <= time2)
			{
				currentTime += Time.deltaTime;
				roue.SetColor (new Color32 (255, 255, 255, (byte)(255 * Mathf.Lerp (1f, 0f, currentTime / time2 ))));
				socle.SetColor (new Color32 (255, 255, 255, (byte)(255 * Mathf.Lerp (1f, 0f, currentTime / time2 ))));				
			}
			else 
			{
				currentTime = 0f;
				fadeOut = false;
			}
		}
	}
}