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

    GameManager _gameManager;
    Camera _mainCamera;    

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _mainCamera = Camera.main;
    }

    public void CheckIfClueCaptured()
    {
        Vector3 viewPos = _mainCamera.WorldToViewportPoint(transform.position);
        // If zoom in close enough while photographing, then the clue is considered detected
        if(IsClueDetectionEnabled && _mainCamera.fieldOfView < MaxDetectFOV)
        {
            float extraRange = (1f - EvidenceDetectArea) / 2f;
            if (extraRange <= viewPos.x  && viewPos.x <= (1f - extraRange) &&
               extraRange <= viewPos.y && viewPos.y <= (1f - extraRange))
            {
                _gameManager.FindClue(viewPos, gameObject.name);
                Debug.Log("Clue '" + gameObject.name + "' captured");
            }
        }
    }
}