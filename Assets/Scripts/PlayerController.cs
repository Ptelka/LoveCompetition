using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float speed = 1;

	private Animator animator;
	static private PlayerController instance;

	public static PlayerController GetInstance()
	{
		return instance;
	}

	public Vector3 GetPosition()
	{
		return transform.position;
	}
	
	void Start ()
	{
		instance = this;
		animator = GetComponent<Animator>();		
		animator.Play("player1_standing");
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{	
		float x = Input.GetAxis("Horizontal") * speed;
		float y = Input.GetAxis("Vertical") * speed;

		if (!Mathf.Approximately(x + y, 0))
		{
			Walk(x, y);
			return;
		}
		
		animator.Play("player1_standing");
	}

	void Walk(float x, float y)
	{
		Vector3 pos = transform.position;
		
		pos.x += x;
		pos.y += y;

		transform.position = pos;
		
		Vector3 sc = transform.localScale;
		sc.x = (-1 + (x < 0 ? 2 : 0)) * Math.Abs(sc.x);
		transform.localScale = sc;
		
		animator.Play("player1_walking");
	}

	void Punch()
	{
		
	}
}
