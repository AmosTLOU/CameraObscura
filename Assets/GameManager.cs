using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Canvas CanvasShoot;
    public Canvas CanvasGallery;
    public PhotoGallery InstancePhotoGallery;
    public GameEvent CameraCaptureEvent;

    // Temporarily, 0 shoot, 1 gallery
    int _gameStatus; 

    // Start is called before the first frame update
    void Start()
    {
        _gameStatus = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameStatus == 0)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                _gameStatus = 1;
                InstancePhotoGallery.EnterGallery();
                CanvasShoot.gameObject.SetActive(false);
                CanvasGallery.gameObject.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                InstancePhotoGallery.Capture();
                CameraCaptureEvent.Raise();
            }
        }
        else if(_gameStatus == 1)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                _gameStatus = 0;
                CanvasShoot.gameObject.SetActive(true);
                CanvasGallery.gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                InstancePhotoGallery.ShowPrevPhoto();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                InstancePhotoGallery.ShowNextPhoto();
            }
        } 
        
        
    }
}
