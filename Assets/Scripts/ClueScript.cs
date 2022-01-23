using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueScript : MonoBehaviour
{
    public bool IsClueDetectionEnabled;
    public float MaxDetectFOV;

    Camera _mainCamera;
    Vector3 _viewPos;
    CameraInputScript _cameraInput;

    void Start()
    {
        _mainCamera = Camera.main;
        _cameraInput = _mainCamera.GetComponent<CameraInputScript>();
    }

    public void CheckIfClueCaptured()
    {
        _viewPos = _mainCamera.WorldToViewportPoint(transform.position);
        if(IsClueDetectionEnabled && _mainCamera.fieldOfView < MaxDetectFOV)
        {
            float extraRange = (1.0f - _cameraInput.EvidenceDetectArea) / 2.0f;
            if (extraRange <= _viewPos.x  && _viewPos.x <= (1.0f - extraRange) &&
               extraRange <= _viewPos.y && _viewPos.y <= (1.0f - extraRange))
            {
                Debug.Log("Clue '" + gameObject.name + "' captured");
            }
        }
    }
}
