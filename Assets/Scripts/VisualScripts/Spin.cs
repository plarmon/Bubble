using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 20;

    private void Update() {
        transform.Rotate(Vector3.right * spinSpeed * Time.deltaTime);
    }
}
