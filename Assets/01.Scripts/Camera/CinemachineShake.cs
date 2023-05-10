using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin channelPerlin;

    private float timer;
    private float timerMax;
    private float startingIntensity;

    public static CinemachineShake Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        channelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if(timer<timerMax)
        {
            timer += Time.deltaTime;
            float amplitude = Mathf.Lerp(startingIntensity, 0f, timer / timerMax);
            channelPerlin.m_AmplitudeGain = amplitude;
        }
    }

    public void ShakeCemera(float intensity, float timerMax)
    {
        this.timerMax = timerMax;
        timer = 0f;
        startingIntensity = intensity;
        channelPerlin.m_AmplitudeGain = intensity;
    }
}
