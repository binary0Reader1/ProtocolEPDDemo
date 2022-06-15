using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private int _cameraDistanceFromCenter = 2;
    private int _cameraHeight = 7;
    private Vector3 _cameraRotationEuler = new Vector3(45,45,0);

    public void Initialize(int centerCoord)
    {
        Vector3 cameraPosition = new Vector3(centerCoord - _cameraDistanceFromCenter, _cameraHeight, centerCoord - _cameraDistanceFromCenter);
        transform.position = cameraPosition;
        transform.eulerAngles = _cameraRotationEuler;
    }
}
