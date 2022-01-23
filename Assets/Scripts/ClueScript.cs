using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueScript : MonoBehaviour
{
    public bool IsClueDetectionEnabled;
    public float MaxDetectFOV;
    // Defines how close the evidence should be to center of the screen for detection
    // 0 -> must be at the center; 1 -> okay if close to the edges 
    [Range(0, 1)]
    public float EvidenceDetectArea;

    Camera _mainCamera;
    Vector3 _viewPos;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    public void CheckIfClueCaptured()
    {
        _viewPos = _mainCamera.WorldToViewportPoint(transform.position);
        if(IsClueDetectionEnabled && _mainCamera.fieldOfView < MaxDetectFOV)
        {
            float extraRange = (1.0f - EvidenceDetectArea) / 2.0f;
            if (extraRange <= _viewPos.x  && _viewPos.x <= (1.0f - extraRange) &&
               extraRange <= _viewPos.y && _viewPos.y <= (1.0f - extraRange))
            {
                Debug.Log("Clue '" + gameObject.name + "' captured");
            }
        }
    }
}
