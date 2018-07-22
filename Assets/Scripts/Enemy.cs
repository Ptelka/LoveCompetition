using System;
using UnityEngine;

public class Enemy : Punchable
{
	[SerializeField] private float range = 0.1f;
	private Action current_behavior;
	private Animator animator;
	private Puncher puncher;

	private PlayerController current;
	
	void Start ()
	{
		current_behavior = Idle;
		animator = GetComponent<Animator>();		
		puncher = GetComponentInChildren<Puncher>();		
		animator.Play("player2_standing");
	}
	
	void Update ()
	{
		if(!puncher.IsBusy())
			current_behavior();
	}

	private void Idle()
	{
		animator.Play("player2_standing");

		foreach (var player in PlayerController.GetInstance())
		{
			if (player && player.GetPosition().x < range)
			{
				current = player;
				return;
			}
		}
	}

	private void Punch()
	{
		puncher.Punch();
	}

	private void SeekAndDestroy()
	{
		if (current == null)
		{
			current_behavior = Idle;
			return;
		}
		
		Vector3 players_pos = current.GetPosition();
		Vector3 direction = players_pos - transform.position;
		 
		transform.position += direction * speed;
		animator.Play("player2_walking");


		if (current.GetPosition().x >= range)
		{
			current_behavior = Idle;
			current = null;
		}
	}
	
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.CompareTag("player"))
		{
			current_behavior = Punch;
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		current_behavior = SeekAndDestroy;
	}
}
