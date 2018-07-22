using UnityEngine;

public class Usable : MonoBehaviour
{
	[SerializeField] private string hint_name;
	[SerializeField] private string icon;
	[SerializeField] private int sympathy;
	[SerializeField] private int use_count = 1;
	private int current_use_count = 1;
	private Item item;
	
	private GameObject hint_object;
	private bool is_active;
	private PlayerController current;
	
	
	void Start ()
	{
		hint_object = Instantiate(Utils.Load(hint_name));
		hint_object.transform.parent = transform;
		hint_object.transform.position = transform.position;
		hint_object.SetActive(false);

		current_use_count = use_count;

		item.icon_path = icon;
		item.sympathy = sympathy;
		item.usable = this;

		ChildStart();
	}

	protected virtual void ChildStart(){}

	public void ResetUse()
	{
		current_use_count++;
	}

	void Update()
	{
		ChildUpdate();
		if (!is_active)
			return;

		if (current_use_count > 0 && current && InputHandler.GetInput(InputHandler.Type.USE, current.GetJID()))
		{
			--current_use_count;
			Action(current);
		}
	}

	protected virtual void ChildUpdate(){}

	protected virtual void Action(PlayerController player)
	{
		player.AddItem(item);
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(!other.CompareTag("player") || current_use_count < 1)
			return;
		
		ShowHint();

		current = other.gameObject.GetComponent<PlayerController>();
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		var pl = other.gameObject.GetComponent<PlayerController>();
		if (current != pl)
			return;

		HideHint();

		current = null;
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
