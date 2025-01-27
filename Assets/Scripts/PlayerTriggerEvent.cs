using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTriggerEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent triggerEvent;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            if(!triggered) {
                Debug.Log("Hit Trigger Enter");
                triggered = true;
                triggerEvent.Invoke();
            }
        }
    }
}
