using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControll : MonoBehaviour
{
	[SerializeField] private float speed;
	private float direction = -1;
	
	void Update () {
		transform.Translate(speed * direction, 0, 0);
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("catbox"))
		{
			direction *= -1;
			Vector3 s = transform.localScale;
			s.x *= -1;
			transform.localScale = s;
		}
	}
}
