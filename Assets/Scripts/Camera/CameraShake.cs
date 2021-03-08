using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{

    public static CameraShake Instance { get; private set; }
    private CinemachineVirtualCamera vCam;
    private float shakeTimer;
    private float startIntensity;
    private float shakeTime;
    private void Awake()
    {
        Instance = this;
        vCam = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin vCamMultiPerlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        vCamMultiPerlin.m_AmplitudeGain = intensity;
        startIntensity = intensity;
        shakeTime = time;
        shakeTimer = time;
    }


    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin vCamMultiPerlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                vCamMultiPerlin.m_AmplitudeGain = Mathf.Lerp(startIntensity, 0f, 1 - (shakeTimer / shakeTime));
            }
        }
    }
}
