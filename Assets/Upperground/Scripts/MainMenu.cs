using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public CanvasRenderer pause;
	public CanvasRenderer start1;
	public CanvasRenderer start2;

	public CanvasRenderer options1;
	public CanvasRenderer options2;

	public CanvasRenderer quit1;
	public CanvasRenderer quit2;

	public CanvasRenderer retour1;
	public CanvasRenderer retour2;
	public CanvasRenderer volM1;
	public CanvasRenderer volM2;
	public CanvasRenderer volS1;
	public CanvasRenderer volS2;

	public GameObject Sliders;

	public Slider S1;
	public Slider S2;

	public AudioMixer AMMusic;
	public AudioMixer AMSFX;

	public AudioSource MusiqueIntro;
	public AudioSource bruitageMenu;


	private int etatMenu;

	private float currentTime = 0f;
	private float time = 2f;
	private float time2 = 3f;

	private bool fadeIn = false;
	private bool fadeOut = false;

	private bool isPaused;
	private bool enterPause;
	private bool available;
	private bool options;

	private bool music = false;

	// Use this for initialization
	void Awake () 
	{
		isPaused = false;
		enterPause = false;
		available = true;
		options = false;
		etatMenu = 0;

		float valM;
		float valS;
		AMMusic.GetFloat("Volume",out valM);
		AMSFX.GetFloat("Volume",out valS);

		S1.value = (valM / 45.0f) + 45.0f;
		S2.value = (valS / 45.0f) + 45.0f;
		DefaultValue ();
	}

	// Update is called once per frame
	void Update () 
	{	
		if(!music && !MusiqueIntro.isPlaying)
		{
			MusiqueIntro.Play ();
			music = true;
		}
		else if( !MusiqueIntro.isPlaying)
		{
			music = false;
		}

		enterPause = true;
		if(Input.GetAxis("Vertical") == 0)
		{
			available = true;
		}

		float h = Input.GetAxis("Vertical");

		if(Mathf.Abs(h) > 0.9f && available)
		{
			bool updated = false;
			if (h > 0.9f ) 
			{
				etatMenu--;
				updated = true;
				bruitageMenu.Play ();
			}
			else if(h < -0.9f)
			{
				etatMenu++;
				updated = true;
				bruitageMenu.Play ();
			}
			if(!options)
			{
				if(etatMenu > 2)
				{
					etatMenu = 0;
				}
				else if(etatMenu < 0)
				{
					etatMenu = 2;
				}				
			}
			else
			{
				if(etatMenu > 5)
				{
					etatMenu = 3;
				}
				else if(etatMenu < 3)
				{
					etatMenu = 5;
				}				
			}


			if(updated)
			{
				updated = false;
				if(options)
				{
					ResetOptions ();
				}
				else
				{
					ResetValue ();
				}
				switch(etatMenu)
				{
				case(0):
					{
						start1.SetAlpha (0f);
						start2.SetAlpha (1f);
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
						quit1.SetAlpha (0f);
						quit2.SetAlpha (1f);
						break;
					}

				case(3):
					{
						volM1.SetAlpha (0f);
						volM2.SetAlpha (1f);
						break;
					}

				case(4):
					{
						volS1.SetAlpha (0f);
						volS2.SetAlpha (1f);
						break;
					}

				case(5):
					{
						retour1.SetAlpha (0f);
						retour2.SetAlpha (1f);
						break;
					}
				}
			}	
			available = false;
		}

		if(Input.GetButtonDown("X button"))
		{
			switch(etatMenu)
			{
			case(0):
				{
					MusiqueIntro.Stop ();
					string s = "Scene_0";
					SceneManager.LoadScene (s);
					break;
				}
			case(1):
				{
					options = true;
					Options ();
					etatMenu = 3;
					break;
				}

			case(2):
				{
					Application.Quit ();
					break;
				}
					
			case(5):
				{
					options = false;
					etatMenu = 0;
					DefaultValue ();
					break;
				}
			}
		}

		float val = Input.GetAxis ("Horizontal");
		if (Mathf.Abs (val) > 0.5f) 
		{
			switch (etatMenu) 
			{
			case(3):
				{
					if (val > 0.01f) {
						S1.value += 0.01f;
					} else {
						S1.value -= 0.01f;
					}
					AMMusic.SetFloat ("Volume", -45f + (S1.value * 45f));
					break;
				}
			case(4):
				{
					if (val > 0.01f) {
						S2.value += 0.01f;
					} else {
						S2.value -= 0.01f;
					}
					AMSFX.SetFloat ("Volume", -45f + (S2.value * 45f));
					break;
				}
			}
		}
	}

	void HideAll()
	{
		pause.SetAlpha (0f);
		start1.SetAlpha (0f);
		start2.SetAlpha (0f);

		options1.SetAlpha (0f);
		options2.SetAlpha (0f);
		quit1.SetAlpha (0f);
		quit2.SetAlpha (0f);

		retour1.SetAlpha (0f);
		retour2.SetAlpha (0f);
		volM1.SetAlpha (0f);
		volM2.SetAlpha (0f);
		volS1.SetAlpha (0f);
		volS2.SetAlpha (0f);

		Sliders.SetActive (false);
	}

	void DefaultValue()
	{
		start1.SetAlpha (0f);
		start2.SetAlpha (1f);

		options1.SetAlpha (1f);
		options2.SetAlpha (0f);
		quit1.SetAlpha (1f);
		quit2.SetAlpha (0f);

		retour1.SetAlpha (0f);
		retour2.SetAlpha (0f);
		volM1.SetAlpha (0f);
		volM2.SetAlpha (0f);
		volS1.SetAlpha (0f);
		volS2.SetAlpha (0f);

		Sliders.SetActive (false);
	}

	void ResetValue()
	{
		start1.SetAlpha (1f);
		start2.SetAlpha (0f);

		options1.SetAlpha (1f);
		options2.SetAlpha (0f);
		quit1.SetAlpha (1f);
		quit2.SetAlpha (0f);

		retour1.SetAlpha (0f);
		retour2.SetAlpha (0f);
		volM1.SetAlpha (0f);
		volM2.SetAlpha (0f);
		volS1.SetAlpha (0f);
		volS2.SetAlpha (0f);

		Sliders.SetActive (false);
	}

	void Options()
	{
		start1.SetAlpha (0f);
		start2.SetAlpha (0f);

		options1.SetAlpha (0f);
		options2.SetAlpha (0f);
		quit1.SetAlpha (0f);
		quit2.SetAlpha (0f);

		retour1.SetAlpha (1f);
		retour2.SetAlpha (0f);
		volM1.SetAlpha (0f);
		volM2.SetAlpha (1f);
		volS1.SetAlpha (1f);
		volS2.SetAlpha (0f);

		Sliders.SetActive (true);		
	}

	void ResetOptions()
	{
		retour1.SetAlpha (1f);
		retour2.SetAlpha (0f);
		volM1.SetAlpha (1f);
		volM2.SetAlpha (0f);
		volS1.SetAlpha (1f);
		volS2.SetAlpha (0f);		
	}
}