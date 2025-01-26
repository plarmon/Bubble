using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float heightFromGround = 1f;
    [SerializeField] private int points = 1;
    private Collider _collider;
    private Renderer _renderer;

    private void Start() {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 3)) {
            transform.position = new Vector3(transform.position.x, hit.point.y + heightFromGround, transform.position.z);
        }
        
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            Collect();
        }
    }

    private void Collect() {
        _collider.enabled = false;
        _renderer.enabled = false;
        CoinManager.Instance.AddToScore(points);
    }
}
