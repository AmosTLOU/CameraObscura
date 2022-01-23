using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueScript : MonoBehaviour
{
    public bool IsClueDetectionEnabled;
    public float MaxFOV;

    Camera _mainCamera;
    Vector3 _viewportCoords;
    CameraInputScript _cameraInput;

    void Start()
    {
        _mainCamera = Camera.main;
        _cameraInput = FindObjectOfType<CameraInputScript>();
    }

    public void CheckIfClueCaptured()
    {
        _viewportCoords = _mainCamera.WorldToViewportPoint(transform.position);
        if(IsClueDetectionEnabled && _mainCamera.fieldOfView < MaxFOV)
        {
            if (_viewportCoords.x > _cameraInput.EvidenceDetectArea/2 && _viewportCoords.x < (1 - _cameraInput.EvidenceDetectArea/2) &&
                _viewportCoords.y > _cameraInput.EvidenceDetectArea/2 && _viewportCoords.y < (1 - _cameraInput.EvidenceDetectArea/2))
            {
                Debug.Log("Clue '" + gameObject.name + "' captured");
            }
        }
    }
}
