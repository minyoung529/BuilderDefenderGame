using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera cmVcam;
    private float orthographicSize;
    private float targetOrthoSize;

    private void Start()
    {
        orthographicSize = targetOrthoSize = cmVcam.m_Lens.OrthographicSize;
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(x, y).normalized;
        float speed = 50f;

        transform.position += dir * speed * Time.deltaTime;

        float zoomAmount = 2f;
        float minSize = 10f, maxSize = 30f;
        targetOrthoSize -= Input.mouseScrollDelta.y * zoomAmount;
        targetOrthoSize = Mathf.Clamp(targetOrthoSize, minSize, maxSize);

        float zoomSpeed = 5f;
        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthoSize, Time.deltaTime * zoomSpeed);

        cmVcam.m_Lens.OrthographicSize = orthographicSize;
    }
}
