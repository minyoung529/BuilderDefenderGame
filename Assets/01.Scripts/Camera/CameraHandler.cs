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
    private bool edgeScrolling;

    public static CameraHandler Instance;

    private void Awake()
    {
        Instance = this;
        edgeScrolling = PlayerPrefs.GetInt("EdgeScrolling", 0) == 1;
    }

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

        if (!edgeScrolling) return;
        HandleMove();
    }

    private void HandleMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        float edgeScrollingSize = 30;

        if (Input.mousePosition.x > Screen.width - edgeScrollingSize)
        {
            x = 1f;
        }

        if (Input.mousePosition.x < edgeScrollingSize)
        {
            x = -1f;
        }

        if (Input.mousePosition.y > Screen.height - edgeScrollingSize)
        {
            y = 1f;
        }

        if (Input.mousePosition.y < edgeScrollingSize)
        {
            y = -1f;
        }

        Vector3 moveDir = new Vector3(x, y).normalized;
        float moveSpeed = 30f;

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    public void SetEdgeScrolling(bool edge)
    {
        edgeScrolling = edge;
        PlayerPrefs.SetInt("EdgeScrolling", edgeScrolling ? 1 : 0);
    }

    public bool GetEdgeScrolling() => edgeScrolling;
}
