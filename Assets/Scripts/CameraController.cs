using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {
	[SerializeField] private float parallax = 0.9f;
	
	[SerializeField] private GameObject sky;
	[SerializeField] private GameObject player;
	[SerializeField] private Vector3 offset = Vector3.zero;
	
	private Transform sky_transform;
	private Transform player_transform;
	private GameObject gameover;
	private PlayerController player_controller;

	private AudioSource source;

	[SerializeField] private AudioClip start;
	[SerializeField] private AudioClip music;
	
	void Start()
	{
		source = GetComponent<AudioSource>();
		player_controller = player.GetComponent<PlayerController>();
		sky_transform = sky.transform;
		player_transform = player.transform;
		gameover = transform.Find("gameover").gameObject;
		source.clip = start;
		if(start)
			source.Play();
	}

	private float timer;
	
	void Update ()
	{
		if(!source.isPlaying && music)
		{
			source.clip = music;
			source.Play();
		}
			
		if (Game.GameOver )
		{

			timer += Time.deltaTime;
			if(Game.Winner == player_controller.GetPID() || Game.Winner == 0)
				gameover.SetActive(true);
			
			if (timer > 1.5f && (InputHandler.GetInput(InputHandler.Type.USE, player_controller.GetJID()) || InputHandler.GetInput(InputHandler.Type.USE, player_controller.GetJID())))
				SceneManager.LoadScene("MainMenu");
		}
		
		Vector3 new_pos = new Vector3(player_transform.position.x + offset.x, player_transform.position.y + offset.y, offset.z);
		Vector3 current_offset = new_pos - transform.position;
		Vector3 sky_new_pos = sky_transform.position;
		sky_new_pos.x += current_offset.x * parallax;
		sky_transform.position = sky_new_pos;
		
		transform.position += current_offset;
		
		
	}
}
