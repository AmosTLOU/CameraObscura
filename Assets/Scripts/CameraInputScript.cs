using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInputScript : MonoBehaviour
{
    public GameEvent CameraCaptureEvent;

    // Defines how close the evidence should be to center of the screen for detection
    // 0 -> must be at the center; 1 -> okay if close to the edges 
    [Range(0,1)]
    public float EvidenceDetectArea;

    void Update()
    {
        // Take a picture
        if (Input.GetKeyDown(KeyCode.Space)) 
            CameraCaptureEvent.Raise();
    }
}
