using System;
using System.Collections.Generic;
using UnityEngine;

public class Puncher: MonoBehaviour
{
    [SerializeField] private string anim_name;
    [SerializeField] private float dmg = 0.1f;

    private HashSet<Collider2D> collisions = new HashSet<Collider2D>();
    
    static int IdCounter = 0;
    private Animator animator;
    
    private float busy;
    private float punch_time;

    private bool dealed;
    
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        punch_time = Utils.GetAnimTime(animator, anim_name);
    }
    
    public void Punch()
    {
        if (busy > 0)
        {
            return;
        }
        
        busy = punch_time;
        animator.Play(anim_name);
    }

    public void Update()
    {
        if (busy <= 0)
        {
            dealed = false;
            return;
        }

        if (busy < punch_time / 2 && !dealed)
        {   
            foreach (var body in collisions)
            {
                if(body.gameObject != transform.parent.gameObject)
                    body.SendMessage("Deal", dmg);
            }

            dealed = true;
        }

        busy -= Time.deltaTime;
    }

    public bool IsBusy()
    {
        return busy > 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        { 
            collisions.Add(other);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            collisions.Remove(other);
        }
    }
}
