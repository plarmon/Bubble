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
    [SerializeField] private float airbornFov = 100;
    [SerializeField] private VisualEffect speedLines;
    [SerializeField] private float maxSpeedLines = 96;
    [SerializeField] private float isAirbornTime = 1;
    private CinemachineFreeLook cam;
    private float currentSpeed, startingFov, lerp, airbornTimer;
    public bool isAirborn = false;
    private bool wasAirbornLastFrame = false;
    private bool changingFov = false;
    private Coroutine currentRoutine;

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
            if(!isAirborn) {
                cam.m_Lens.FieldOfView = Mathf.Lerp(cam.m_Lens.FieldOfView, Mathf.Lerp(startingFov, topSpeedFov, lerp), 0.25f);
                speedLines.SetFloat("SpawnRate", Mathf.Lerp(10, maxSpeedLines, lerp));
            }
        }

        if(player.GetIsGrounded() && isAirborn) {
            Debug.Log("Grounded");
            isAirborn = false;
            if(currentRoutine != null) {
                StopCoroutine(currentRoutine);
            }
            speedLines.SetFloat("Radius", 2.25f);
            speedLines.SetFloat("SpawnRate", Mathf.Lerp(10, maxSpeedLines, lerp));
            currentRoutine = StartCoroutine(SetFov(startingFov));
        }
    }

    public void SetIsAirborn() {
        isAirborn = true;
        if(currentRoutine != null) {
            StopCoroutine(currentRoutine);
        }
        speedLines.SetFloat("Radius", 4);
        speedLines.SetFloat("SpawnRate", maxSpeedLines);
        currentRoutine = StartCoroutine(SetFov(airbornFov));
    }

    private IEnumerator SetFov(float newFov) {
        float fovLerp = 0;
        float startFov = cam.m_Lens.FieldOfView;
        while(fovLerp < 1) {
            fovLerp += Time.deltaTime;
            if(fovLerp > 1) {
                fovLerp = 1;
            }
            cam.m_Lens.FieldOfView = Mathf.Lerp(startFov, newFov, fovLerp);
            yield return null;
        }
    }

    public void Reset() {
        if(currentRoutine != null) {
            StopCoroutine(currentRoutine);
        }
        speedLines.SetFloat("Radius", 2.25f);
        speedLines.SetFloat("SpawnRate", 0);
        currentRoutine = StartCoroutine(SetFov(startingFov));
    }
}
