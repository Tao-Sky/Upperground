using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform Player;

	public Vector2
		Margin,
		Smoothing;

	public BoxCollider2D[] Bounds;

	private Vector3
		min,
		max;

	public bool IsFollowing { get; set; }

	//pour la fonction de shake
	public float shaketime=0.0f;//valeur a appeler dans le code pour set le timer
	public float shakeAmount = 2.0f;
	private bool firstshake;
	private Vector3 pos;

	public void Start()
	{
		min = Bounds[0].bounds.min;
		max = Bounds[0].bounds.max;
		IsFollowing = true;
	}

    public void changeCadre()
    {
        GameManager gamemanager = FindObjectOfType<GameManager>();
        min = Bounds[gamemanager.GetComponent<GameManager>().level].bounds.min;
        max = Bounds[gamemanager.GetComponent<GameManager>().level].bounds.max;
        IsFollowing = true;
    }

	public void SwapLvl3(int i)
	{
		min = Bounds[i].bounds.min;
		max = Bounds[i].bounds.max;
		IsFollowing = true;		
	}

	public void Update()
	{
		if (shaketime > 0.1f) 
		{
			if (firstshake) {
				pos = gameObject.transform.localPosition;
				firstshake = false;
			}
			Shake ();
		} 
		else 
		{
			if (!firstshake) {
				firstshake = true;
			}
			var x = transform.position.x;
			var y = transform.position.y;

			if (IsFollowing) {
				if (Mathf.Abs (x - Player.position.x) > Margin.x)
					x = Mathf.Lerp (x, Player.position.x, Smoothing.x * Time.deltaTime);

				if (Mathf.Abs (y - Player.position.y) > Margin.y)
					y = Mathf.Lerp (y, Player.position.y, Smoothing.y * Time.deltaTime);
			}

			var cameraHalfWidth = Camera.main.orthographicSize * ((float)Screen.width / Screen.height);

			x = Mathf.Clamp (x, min.x + cameraHalfWidth, max.x - cameraHalfWidth);
			y = Mathf.Clamp (y, min.y + Camera.main.orthographicSize, max.y - Camera.main.orthographicSize);

			transform.position = new Vector3 (x, y, transform.position.z);
		}
	}

	public void Shake(){
		float randx = Random.Range (-0.001f*shakeAmount, 0.001f*shakeAmount);
		gameObject.transform.localPosition=new Vector3(pos.x+randx,pos.y,pos.z);
			shaketime -= Time.deltaTime;
	}

}
