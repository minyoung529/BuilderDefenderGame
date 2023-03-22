using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilClass
{
    private static Camera mainCamera = null;

    public static Vector3 GetMouseWorldPosition()
    {
        mainCamera ??= mainCamera = Camera.main;
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        return mouseWorldPosition;
    }

}
