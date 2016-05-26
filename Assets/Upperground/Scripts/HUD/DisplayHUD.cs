using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class DisplayHUD : MonoBehaviour {

	public CanvasRenderer roue;
	public CanvasRenderer socle;
	public CanvasRenderer pause;
	public CanvasRenderer back1;
	public CanvasRenderer back2;
	public CanvasRenderer restart1;
	public CanvasRenderer restart2;
	public CanvasRenderer options1;
	public CanvasRenderer options2;
	public CanvasRenderer main1;
	public CanvasRenderer main2;

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

	void Awake () 
	{
		roue.SetColor (new Color32 (255, 255, 255, 0));
		socle.SetColor (new Color32 (255, 255, 255, 0));
		isPaused = false;
		enterPause = false;
		available = true;
		options = false;
		etatMenu = 0;

		float valM;
		float valS;

		AMMusic.GetFloat("Volume",out valM);

		AMSFX.GetFloat("Volume",out valS);

		S1.value = (valM + 45.0f) / 45.0f;
		S2.value = (valS + 45.0f) / 45.0f;
	}

	void Update () 
	{
		isPaused = GameObject.Find ("GameManager").GetComponent<GameManager> ().getPause();
		Debug.Log ("etat menu " + etatMenu + " - isPaused " + isPaused);

		if(!isPaused)
		{
			etatMenu = 0;

			if(enterPause)
			{
				GetComponent<HudSFX> ().unpauseSFX ();
				GameObject.Find ("Player").GetComponent<PlayerController> ().canmove = true;
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
		GameObject.Find ("Player").GetComponent<PlayerController> ().canmove = false;

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
			}
			else if(h < -0.9f)
			{
				etatMenu++;
				updated = true;
			}
			if(!options)
			{
				if(etatMenu > 3)
				{
					etatMenu = 0;
				}
				else if(etatMenu < 0)
				{
					etatMenu = 3;
				}				
			}
			else
			{
				if(etatMenu > 6)
				{
					etatMenu = 4;
				}
				else if(etatMenu < 4)
				{
					etatMenu = 6;
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
						restart1.SetAlpha (0f);
						restart2.SetAlpha (1f);
						break;
					}

				case(2):
					{
						options1.SetAlpha (0f);
						options2.SetAlpha (1f);
						break;
					}

				case(3):
					{
						main1.SetAlpha (0f);
						main2.SetAlpha (1f);
						break;
					}

				case(4):
					{
						volM1.SetAlpha (0f);
						volM2.SetAlpha (1f);
						break;
					}

				case(5):
					{
						volS1.SetAlpha (0f);
						volS2.SetAlpha (1f);
						break;
					}

				case(6):
					{
						retour1.SetAlpha (0f);
						retour2.SetAlpha (1f);
						break;
					}
				}
			}	
			available = false;
		}

		if(Input.GetButtonDown("A button"))
		{
			switch(etatMenu)
			{
				case(0):
				{
					GameObject.Find ("GameManager").GetComponent<GameManager> ().SetPause(false);
					isPaused = false;
					etatMenu = 0;
					break;
				}
				case(1):
				{
						etatMenu = 0;
                        GameObject.Find("GameManager").GetComponent<GameManager>().SetPause(false);
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().level < 2)
                        {
                            SceneManager.LoadScene("Scene_0");
                        }
                        else
                        {
                            string s = "Scene_";
                            s += GameObject.Find("GameManager").GetComponent<GameManager>().level.ToString();
                            SceneManager.LoadScene(s);
                        }
						isPaused = false;
					    break;
                    }

				case(2):
				{
					options = true;
					Options ();
					etatMenu = 4;
					break;
				}

				case(3):
				{
					GameObject.Find ("SoundManager").GetComponent<SoundManager> ().ResetEffects ();
                     GameObject.Find("GameManager").GetComponent<GameManager>().SetPause(false);
                     SceneManager.LoadScene ("Main_Menu");
					break;
				}

				case(6):
				{
					options = false;
					etatMenu = 0;
					DefaultValue ();
					break;
				}
			}
		}

		float val = Input.GetAxis ("Horizontal");
		if (Mathf.Abs(val) > 0.5f)
		{

			switch(etatMenu)
			{
			case(4):
				{
					if(val > 0.01f)
					{
						S1.value += 0.01f;
					}
					else
					{
						S1.value -= 0.01f;
					}
					AMMusic.SetFloat ("Volume", -45f + (S1.value*45f));
					break;
				}
			case(5):
				{
					if(val > 0.01f)
					{
						S2.value += 0.01f;
					}
					else
					{
						S2.value -= 0.01f;
					}
					AMSFX.SetFloat ("Volume", -45f + (S2.value*45f));
					break;
				}
			}
		}
			
	}

	void HideAll()
	{			
		pause.SetAlpha (0f);
		back1.SetAlpha (0f);
		back2.SetAlpha (0f);
		restart1.SetAlpha (0f);
		restart2.SetAlpha (0f);
		options1.SetAlpha (0f);
		options2.SetAlpha (0f);
		main1.SetAlpha (0f);
		main2.SetAlpha (0f);

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
		back1.SetAlpha (0f);
		back2.SetAlpha (1f);
		restart1.SetAlpha (1f);
		restart2.SetAlpha (0f);
		options1.SetAlpha (1f);
		options2.SetAlpha (0f);
		main1.SetAlpha (1f);
		main2.SetAlpha (0f);

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
		back1.SetAlpha (1f);
		back2.SetAlpha (0f);
		restart1.SetAlpha (1f);
		restart2.SetAlpha (0f);
		options1.SetAlpha (1f);
		options2.SetAlpha (0f);
		main1.SetAlpha (1f);
		main2.SetAlpha (0f);

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
		back1.SetAlpha (0f);
		back2.SetAlpha (0f);
		restart1.SetAlpha (0f);
		restart2.SetAlpha (0f);
		options1.SetAlpha (0f);
		options2.SetAlpha (0f);
		main1.SetAlpha (0f);
		main2.SetAlpha (0f);

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