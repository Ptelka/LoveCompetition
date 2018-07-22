using System;
using UnityEngine;

public class PlayerController : Punchable
{
	[SerializeField] private GameObject panel;
	[SerializeField] private float dogeTime = 1;
	[SerializeField] private float dogeWait = 3;
	[SerializeField] private float doge = 2;
	
	private Animator animator;
	static private PlayerController[] instance = new PlayerController[2];
	private Puncher puncher;

	private float current_speed;

	private Transform spritemask;
	private InventoryManager inventory;

	[SerializeField] private int pid;
	private int jid;

	private float doge_timer;
	private float wait_timer;

	public static PlayerController[] GetInstance()
	{
		return instance;
	}

	void UpdateHeart()
	{
		var scale = spritemask.localScale;
		float sym = (float) GetSympathy();
		scale.y = Mathf.Min(1f,  sym/ 100f);
		spritemask.localScale = scale;
	}
	
	public void AddItem(Item it)
	{
		inventory.InsertItem(it);
		UpdateHeart();
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
		spritemask = transform.Find("heart-mask");
		base.Start();
		jid = InputHandler.GetJoyId(pid);
		instance[pid - 1] = this;	
		animator = GetComponent<Animator>();
		animator.Play("player" + pid + "_standing");
		puncher = GetComponentInChildren<Puncher>();
		
		var scale = new Vector3(1, 0, 1);
		spritemask.localScale = scale;

		inventory = panel.GetComponent<InventoryManager>();
	}

	protected override void OnDeath()
	{
		inventory.pop();
		UpdateHeart();
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
		current_speed = speed;
		if (doge_timer > 0)
		{
			current_speed *= doge;
			doge_timer -= Time.deltaTime;
		}
		else
			wait_timer -= Time.deltaTime;
		
		if (IsStunned())
		{
			StunUpdate(Time.fixedDeltaTime);
			return;
		}
		
		if (puncher.IsBusy() || Game.GameOver)
			return;
		
		SetVisible(true);
		
		if (InputHandler.GetInput(InputHandler.Type.PUNCH, jid))
		{
			puncher.Punch();
			return;
		}
		
		if (InputHandler.GetInput(InputHandler.Type.DOGE, jid) && wait_timer <= 0)
		{
			doge_timer = dogeTime;
			wait_timer = dogeWait;
		}
		
		float x = InputHandler.GetHorizontal(jid) * current_speed;
		float y = InputHandler.GetVertical(jid) * current_speed;

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
