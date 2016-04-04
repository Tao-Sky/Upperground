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

	public void Update()
	{
        var x = transform.position.x;
		var y = transform.position.y;

		if (IsFollowing) 
		{
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
