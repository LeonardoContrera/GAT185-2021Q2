using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    float timer;
    public override void Enter(Agent owner)
    {
        owner.animator.SetTrigger("Death");
        timer = 4;
    }

    public override void Execute(Agent owner)
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(owner.gameObject);
        }
    }

    public override void Exit(Agent owner)
    {
    }
}
