using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    int checkpointTarget = 0;

    public Transform[] checkpoints;

    public Transform playerTransform;

    Rigidbody playerRB;

    private void Start()
    {
        playerRB = playerTransform.GetComponentInChildren<Rigidbody>();
    }

    public void UpdateCheckpoint(int targetID)
    {
        if(targetID > checkpointTarget)
        {
            checkpointTarget = targetID;
        }
    }

    public void ResetPlayer()
    {
        Debug.Log("Reset");

        playerRB.velocity = Vector3.zero;
        playerRB.angularVelocity = Vector3.zero;

        playerTransform.position = checkpoints[checkpointTarget].position;

    }


}
