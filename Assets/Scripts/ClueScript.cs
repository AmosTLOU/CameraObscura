using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueScript : MonoBehaviour
{
    public bool isClueDetectionEnabled;
    public float maxFOV;

    Camera mainCamera;
    Vector3 viewportCoords;
    CameraInputScript cameraInput;

    void Start()
    {
        mainCamera = Camera.main;
        cameraInput = FindObjectOfType<CameraInputScript>();
    }

    public void CheckIfClueCaptured()
    {
        viewportCoords = mainCamera.WorldToViewportPoint(transform.position);
        if(isClueDetectionEnabled && mainCamera.fieldOfView < maxFOV)
        {
            if (viewportCoords.x > cameraInput.evidenceDetectArea/2 && viewportCoords.x < (1 - cameraInput.evidenceDetectArea/2) &&
                viewportCoords.y > cameraInput.evidenceDetectArea/2 && viewportCoords.y < (1 - cameraInput.evidenceDetectArea/2))
            {
                Debug.Log("Clue '" + gameObject.name + "' captured");
            }
        }
    }
}
