using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverState : State<GhostAI>
{
    private static GameOverState instance;
    private Collider ghostCollider;
    private Rigidbody ghostBody;
    private GameObject player;
    private Collider playerCollider;
    private Rigidbody playerRigidBody;
    private PlayerMovement playerMovement;
    private GameObject cameraObject;
    //private GameObject deathCamera;
    private Camera mainCamera;
    private Vector3 targetPoint;
    private Quaternion targetRotation;

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
        cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        //deathCamera = GameObject.FindGameObjectWithTag("DeathCam");
        mainCamera = cameraObject.GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerRigidBody = player.GetComponent<Rigidbody>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerCollider = player.GetComponent<Collider>();
        ghostCollider = owner.GetComponent<Collider>();

        //Freezes the anim
        playerMovement.enabled = false;
        playerRigidBody.constraints = RigidbodyConstraints.FreezePosition;

        //Makes the ghost visible and turn off the player and ghost colliders
        mainCamera.cullingMask |= 1 << LayerMask.NameToLayer("Ghost");
        //ghostCollider.enabled = false;
        playerCollider.enabled = false;

        //Rotates the ghost to the player
        Vector3 relativePos = player.transform.position - owner.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        owner.transform.rotation = rotation;

        //plays the anim and freezes the ghost
        //owner.heardSomethingAnim.SetInteger("KillAnim", 1);
        owner.NMA.isStopped = true;
        owner.NMA.velocity = Vector3.zero;
        owner.r.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
    }

    public override void UpdateState(GhostAI owner)
    {
        //TO DO slurp players cam to ghost mabey
        //targetPoint = new Vector3(owner.transform.position.x, owner.transform.position.y, owner.transform.position.z) - player.transform.position;
        //targetRotation = Quaternion.LookRotation(-targetPoint, Vector3.up);
        //owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, targetRotation, Time.deltaTime * 2.0f);
    }

    public override void ExitState(GhostAI owner)
    {
        
    }

}
