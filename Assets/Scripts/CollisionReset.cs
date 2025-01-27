using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionReset : MonoBehaviour
{

    public CheckpointManager checkpoint;

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.transform.tag == "Reset")
        {
            checkpoint.ResetPlayer();
        }        
    }
}
