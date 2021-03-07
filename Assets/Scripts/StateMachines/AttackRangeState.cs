using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeState : State
{
    float timer;
    public override void Enter(Agent owner)
    {
        owner.animator.SetTrigger("Range");
        owner.movement.Stop();
        timer = 3;
    }

    public override void Execute(Agent owner)
    {
        timer -= Time.deltaTime;
        if ( timer <= 2 && owner.weapon != null)
        {
            owner.weapon.Fire(owner.transform.forward);
            if (timer <= 0)
            {
                ((StateAgent)owner).StateMachine.SetState("AttackState");

            }
        }
    }

    public override void Exit(Agent owner)
    {
        owner.movement.Resume();
    }
}
