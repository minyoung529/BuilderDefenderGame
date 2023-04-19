using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField]
    private Gradient gradient;

    private Light2D light;

    [SerializeField]
    private float secondsPerDay = 10f;
    private float dayTime;
    private float dayTimeSpeed;

    private void Awake()
    {
        light = GetComponent<Light2D>();
        dayTimeSpeed = 1 / secondsPerDay;
    }

    private void Update()
    {
        dayTime += Time.deltaTime * dayTimeSpeed;
        light.color = gradient.Evaluate(dayTime % 1f);
    }
}
