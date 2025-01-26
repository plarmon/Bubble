using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.VFX;

public class CameraSpeedVisual : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private float fovSpeedStart;
    [SerializeField] private float topSpeedFov = 60;
    [SerializeField] private VisualEffect speedLines;
    [SerializeField] private float maxSpeedLines = 96;
    private CinemachineFreeLook cam;
    private float currentSpeed, startingFov, lerp;

    private void Start() {
        cam = GetComponent<CinemachineFreeLook>();
        startingFov = cam.m_Lens.FieldOfView;
        speedLines.SetFloat("SpawnRate", 0);
    }

    private void Update(){
        Vector3 currentVelocity = player.GetRigidbody().velocity;
        currentVelocity = new Vector3(currentVelocity.x, 0, currentVelocity.z);
        currentSpeed = currentVelocity.magnitude;
        if(currentSpeed >= fovSpeedStart) {
            lerp = (currentSpeed - fovSpeedStart) / (player.GetMaxSpeed() - fovSpeedStart);
            cam.m_Lens.FieldOfView = Mathf.Lerp(startingFov, topSpeedFov, lerp);
            speedLines.SetFloat("SpawnRate", Mathf.Lerp(10, maxSpeedLines, lerp));
        }
    }
}
