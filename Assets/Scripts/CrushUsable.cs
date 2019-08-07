using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushUsable : Usable
{
    private bool heartv = false;
    private float timer;
    private GameObject heart;
    
    protected override void Action(PlayerController player)
    {
        heart.SetActive(true);
        timer = 1;
        heartv = true;
        
        if (player.GetSympathy() > 100)
        {
            Game.GameOver = true;
            Game.Winner = player.GetPID();
        }
    }
    
    protected override void ChildUpdate()
    {
        if(!heartv)
        {return;}
        timer -= Time.deltaTime;
        if (timer <= 0 && heartv)
            heart.SetActive(heartv = false);
    }

    protected override void ChildStart()
    {
        heart = transform.Find("heart").gameObject;
        heart.SetActive(false);
    }
}
