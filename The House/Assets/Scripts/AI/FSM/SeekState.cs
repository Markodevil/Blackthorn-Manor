using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekState : State<GhostAI> {
    
    private static SeekState instance;

    private SeekState()
    {
        if (instance != null)
        {
            return;

        }

        instance = this;
        stateName = "Seek";
    }

    public static SeekState GetInstance
    {
        get
        {
            if (instance == null)
            {
                new SeekState();
            }

            return instance;
        }
    }

    public override void EnterState(GhostAI owner)
    {

    }

    public override void ExitState(GhostAI owner)
    {
        
    }

    public override void UpdateState(GhostAI owner)
    {
        owner.NMA.SetDestination(owner.destination);

        if(owner.NMA.remainingDistance <= 1.0f)
        {
            owner.hasHeardSomething = false;
            owner.FSM.ChangeState(WanderState.GetInstance);
        }

        //if ghost loses sight of you, it goes back to wander
        if(owner.sight.visibleTargets.Count == 0)
        {
            owner.FSM.ChangeState(WanderState.GetInstance);
        }
    }
}
