using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekState : State<GhostAI>
{

    private static SeekState instance;
    private float cantSeePlayerCountdown;
    private float timer;

    private SeekState()
    {
        //if (instance != null)
        //{
        //    return;
        //
        //}

        instance = this;
        stateName = "Seek";
    }

    public static SeekState GetInstance(GhostAI owner)
    {

        //if (instance == null)
        //{
        if (owner.seekState == null)
            new SeekState();
        //}

        return instance;
    }

    public override void EnterState(GhostAI owner)
    {
        cantSeePlayerCountdown = owner.cantSeePlayerCountdown;
        timer = cantSeePlayerCountdown;

        if (owner.seekSound)
        {
            if (!owner.seekSound.isPlaying)
                owner.seekSound.PlayOneShot(owner.seekSoundClips[owner.seekSoundClipIndex]);
        }
    }

    public override void ExitState(GhostAI owner)
    {

    }

    public override void UpdateState(GhostAI owner)
    {
        owner.NMA.SetDestination(owner.destination);

        //killed the player
        if (owner.NMA.remainingDistance <= 1.0f)
        {
            owner.hasHeardSomething = false;
            owner.FSM.ChangeState(WanderState.GetInstance(owner));
        }

        //check if we have a path
        if (owner.NMA.pathPending != false)
        { //If we have a path increase speed while within 10 meters
            if (owner.NMA.remainingDistance >= 1.0f && owner.NMA.remainingDistance <= 11.0f)
            {
                //Increase Speed as we get closer
                if (owner.NMA.speed < 3.0f)
                {
                    owner.NMA.speed += 0.1f;
                }
            } // If we've gotten far enought away reset speed
            else if (owner.NMA.remainingDistance >= 3.0f)
                owner.NMA.speed = 1.0f;
        }




        //if ghost loses sight of you, it goes back to wander
        if (owner.sight.visibleTargets.Count == 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                owner.FSM.ChangeState(WanderState.GetInstance(owner));
                timer = cantSeePlayerCountdown;
            }
        }
    }
}
