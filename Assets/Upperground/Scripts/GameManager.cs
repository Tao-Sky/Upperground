using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public enum GameState
	{
		Boot,
		MainMenu,
		InGame,
		AfterGame,
		Loading
	}
	private bool IsPaused;
	private static GameManager manager = null;
	public static GameManager Manager
	{
		get { return manager; }
	}

    public int level;

	void Awake()
	{
		GetThisGameManager();
	}

	// Use this for initialization
	void Start () 
	{
        level = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!IsPaused)
		{
			if(Input.GetButtonDown("Start"))
			{
				SetPause (true);
				Debug.Log ("coucou c'est la pause");
			}
		}
		else
		{
			if(Input.GetButtonDown("Start"))
			{
				SetPause (false);
				Debug.Log ("coucou c'est plus la pause");
			}
		}
	}

	void GetThisGameManager()
	{
		if(manager != null && manager != this)
		{
			Destroy (this.gameObject);
			return;
		}
		else
		{
			manager = this;
		}
		DontDestroyOnLoad (this.gameObject);
	}


	public void SetPause(bool pause)
	{
		if(pause)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1f;
		}
		IsPaused = pause;
	}
}
