using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelect : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		Game.GameOver = false;
		Game.Winner = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (InputHandler.GetInput(InputHandler.Type.USE, 1) || InputHandler.GetInput(InputHandler.Type.PUNCH, 1))
		{
			SceneManager.LoadScene("Game");
		}
	}
}
