using UnityEngine;
using System.Collections;

public class DisplayHUD : MonoBehaviour {

	public CanvasRenderer roue;
	public CanvasRenderer socle;
	public CanvasRenderer pause;
	public CanvasRenderer back1;
	public CanvasRenderer back2;
	public CanvasRenderer options1;
	public CanvasRenderer options2;
	public CanvasRenderer main1;
	public CanvasRenderer main2;

	private int etatMenu;

	private float currentTime = 0f;
	private float time = 2f;
	private float time2 = 3f;

	private bool fadeIn = false;
	private bool fadeOut = false;

	private bool isPaused;
	private bool enterPause;
	private bool available;

	// Use this for initialization
	void Awake () 
	{
		roue.SetColor (new Color32 (255, 255, 255, 0));
		socle.SetColor (new Color32 (255, 255, 255, 0));
		isPaused = false;
		enterPause = false;
		available = true;
		etatMenu = 0;
	}

	// Update is called once per frame
	void Update () 
	{
		isPaused = GameObject.Find ("GameManager").GetComponent<GameManager> ().IsPaused;
		if(!isPaused)
		{
			if(enterPause)
			{
				GetComponent<HudSFX> ().unpauseSFX ();
			}
			enterPause = false;
			if(pause.GetAlpha() != 0f)
			{
				HideAll ();
			}
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
		else
		{
			if(!enterPause)
			{
				DefaultValue ();
				pause.SetAlpha (1f);
				GetComponent<HudSFX> ().pauseSFX ();

			}
			Pause ();
		}

	}

	void Pause()
	{
		enterPause = true;
		if(Input.GetAxis("Vertical") == 0)
		{
			available = true;
		}

		float h = Input.GetAxis("Vertical");
		Debug.Log (h);

		if(Mathf.Abs(h) > 0.9f && available)
		{
			bool updated = false;
			if (h > 0.9f ) 
			{
				etatMenu--;
				updated = true;
			}
			else if(h < -0.9f)
			{
				etatMenu++;
				updated = true;
			}
			if(etatMenu > 2)
			{
				etatMenu = 0;
			}
			else if(etatMenu < 0)
			{
				etatMenu = 2;
			}

			if(updated)
			{
				updated = false;
				ResetValue ();
				GetComponent<HudSFX> ().pauseMoveSFX ();
				switch(etatMenu)
				{
				case(0):
					{
						back1.SetAlpha (0f);
						back2.SetAlpha (1f);
						break;
					}

				case(1):
					{
						options1.SetAlpha (0f);
						options2.SetAlpha (1f);
						break;
					}

				case(2):
					{
						main1.SetAlpha (0f);
						main2.SetAlpha (1f);
						break;
					}
				}
			}	
			available = false;
		}

	}

	void HideAll()
	{
		pause.SetAlpha (0f);
		back1.SetAlpha (0f);
		back2.SetAlpha (0f);
		options1.SetAlpha (0f);
		options2.SetAlpha (0f);
		main1.SetAlpha (0f);
		main2.SetAlpha (0f);
	}

	void DefaultValue()
	{
		back1.SetAlpha (0f);
		back2.SetAlpha (1f);
		options1.SetAlpha (1f);
		options2.SetAlpha (0f);
		main1.SetAlpha (1f);
		main2.SetAlpha (0f);
	}

	void ResetValue()
	{
		back1.SetAlpha (1f);
		back2.SetAlpha (0f);
		options1.SetAlpha (1f);
		options2.SetAlpha (0f);
		main1.SetAlpha (1f);
		main2.SetAlpha (0f);		
	}
}