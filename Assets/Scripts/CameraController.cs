using UnityEngine;

public class CameraController : MonoBehaviour {
	[SerializeField] private float parallax = 0.9f;
	
	[SerializeField] private GameObject sky;
	[SerializeField] private GameObject player;
	[SerializeField] private Vector3 offset = Vector3.zero;
	
	private Transform sky_transform;
	private Transform player_transform;
	
	void Start()
	{
		sky_transform = sky.transform;
		player_transform = player.transform;
	}
	
	void Update ()
	{
		Vector3 new_pos = new Vector3(player_transform.position.x + offset.x, player_transform.position.y + offset.y, offset.z);
		Vector3 current_offset = new_pos - transform.position;
		Vector3 sky_new_pos = sky_transform.position;
		sky_new_pos.x += current_offset.x * parallax;
		sky_transform.position = sky_new_pos;
		
		transform.position += current_offset;
	}
}
