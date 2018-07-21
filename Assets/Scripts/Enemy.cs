using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private float speed = 1;
	private Action current_behavior;
	private Animator animator;

	
	void Start ()
	{
		current_behavior = Idle;
		animator = GetComponent<Animator>();		
		animator.Play("player2_standing");
	}
	
	void Update ()
	{
		current_behavior();
	}

	private void Idle()
	{
		animator.Play("player2_standing");
	} 

	private void SeekAndDestroy()
	{
		Vector3 players_pos = PlayerController.GetInstance().GetPosition();
		Vector3 direction = players_pos - transform.position;
		
		transform.position += direction * speed;
		animator.Play("player2_walking");

	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("player"))
		{
			current_behavior = SeekAndDestroy;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		current_behavior = Idle;
	}
}
