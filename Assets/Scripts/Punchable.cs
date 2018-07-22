using UnityEngine;

public class Punchable : MonoBehaviour
{
	[SerializeField] private float max_life = 1;
	[SerializeField] private float current_life = 1;
	[SerializeField] protected float speed = 1;
	[SerializeField] protected float stun = 2;
	[SerializeField] protected float wink_time = 0.1f;
	private SpriteRenderer my_renderer;

	private float timer = 0;
	private float wink_timer = 0;

	private bool dead;
	private bool visible = true;

	public void Start()
	{
		my_renderer = GetComponent<SpriteRenderer>();
	}

	protected void SetVisible(bool v)
	{
		my_renderer.enabled = v;
	}
	
	public bool IsDead()
	{
		return dead;
	}

	public bool IsStunned()
	{
		return timer >= 0;
	}

	public void StunUpdate(float dt)
	{
		timer -= dt;
		Wink(dt);
	}

	public void Wink(float dt)
	{
		wink_timer += dt;
		if (wink_timer > wink_time)
		{
			visible = !visible;
			SetVisible(visible);
			wink_timer = 0;
		}
	}

	protected virtual void OnDeath()
	{
		
	}

	public void Deal(float dmg)
	{
		current_life -= dmg;
		
		if (current_life <= 0)
		{
			if(timer <= 0)
			{
				timer = stun;
				OnDeath();
			}
			dead = true;

			current_life = max_life;
		}
	}
}
