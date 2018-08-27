using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : State<GhostAI>
{
    private static GameOverState instance;

    private GameOverState(GhostAI owner)
    {
        instance = this;
        owner.gameOverState = this;
        stateName = "GameOver";
    }

    public static GameOverState GetInstance(GhostAI owner)
    {
        if (owner.gameOverState == null)
            new GameOverState(owner);

        return instance;
    }

    public override void EnterState(GhostAI owner)
    {
        owner.heardSomethingAnim.SetInteger("KillAnim", 1);
        owner.NMA.speed = 0;
        owner.r.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
    }

    public override void ExitState(GhostAI owner)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(GhostAI owner)
    {
        throw new System.NotImplementedException();
    }
}
