using System;
using UnityEngine;

public class PlayerController : Punchable
{
	[SerializeField] private GameObject panel;
	private Animator animator;
	static private PlayerController[] instance = new PlayerController[2];
	private Puncher puncher;

	private InventoryManager inventory;

	[SerializeField] private int pid;
	private int jid;

	public static PlayerController[] GetInstance()
	{
		return instance;
	}

	public void AddItem(Item it)
	{
		inventory.InsertItem(it);
	}

	public Vector3 GetPosition()
	{
		return transform.position;
	}

	public int GetJID()
	{
		return jid;
	}
	
	void Start ()
	{
		base.Start();
		jid = InputHandler.GetJoyId(pid);
		instance[pid - 1] = this;	
		animator = GetComponent<Animator>();
		animator.Play("player" + pid + "_standing");
		puncher = GetComponentInChildren<Puncher>();

		inventory = panel.GetComponent<InventoryManager>();
	}

	protected override void OnDeath()
	{
		inventory.pop();
	}

	public int GetPID()
	{
		return pid;
	}
	
	public int GetSympathy()
	{
		return inventory.GetSympathy();
	}
	
	void FixedUpdate ()
	{
		if (IsStunned())
		{
			StunUpdate(Time.fixedDeltaTime);
			return;
		}
		
		if (puncher.IsBusy())
			return;
		
		SetVisible(true);
		
		if (InputHandler.GetInput(InputHandler.Type.PUNCH, jid))
		{
			puncher.Punch();
			return;
		}
				
		float x = InputHandler.GetHorizontal(jid) * speed;
		float y = InputHandler.GetVertical(jid) * speed;

		if (!Mathf.Approximately(x + y, 0))
		{
			Walk(x, y);
			return;
		}
		
		animator.Play("player" + pid + "_standing");
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
		
		animator.Play("player" + pid + "_walking");
	}
}
