using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    public UnityEvent OnCheckpointTriggered;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Test");

        if(other.tag == "Player")
        {
            OnCheckpointTriggered.Invoke();
        }
    }


}
