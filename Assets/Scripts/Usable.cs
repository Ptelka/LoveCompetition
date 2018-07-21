using UnityEngine;

public class Usable : MonoBehaviour
{
	[SerializeField] private string hint_name = "";
	private GameObject hint_object;
	private bool is_active;
	
	void Start ()
	{
		hint_object = Instantiate(Utils.Load(hint_name));
		hint_object.transform.parent = transform;
		hint_object.transform.position = transform.position;
		hint_object.SetActive(false);
	}

	void Update()
	{
		if (!is_active)
			return;

		if (Input.GetButtonDown("Use"))
		{
			Action();
		}
	}

	protected virtual void Action()
	{
		Debug.Log("Action performed!");
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		ShowHint();
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		HideHint();
	}

	void ShowHint()
	{
		hint_object.SetActive(is_active = true);
	}

	void HideHint()
	{
		hint_object.SetActive(is_active = false);
	}
}
