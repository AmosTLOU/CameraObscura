using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameStatus
{
    Shoot,
    Shooting,
    Gallery,
}

public class GameManager : MonoBehaviour
{
    public Canvas CanvasShoot;
    public Canvas CanvasGallery;
    public PhotoGallery InstancePhotoGallery;
    public GameEvent CameraCaptureEvent;
    public float TimeCapture;
    public AudioSource AudioPlayer;
    public AudioClip SFXClick;
    public UnityEngine.Video.VideoPlayer VideoPlayer;

    Camera _mainCamera;
    GameStatus _gameStatus;
    float _lastCaptureTime;
    bool _justTaken;
    

    void Start()
    {
        _gameStatus = GameStatus.Shoot;
        _lastCaptureTime = float.NegativeInfinity;
        _justTaken = false;
        _mainCamera = Camera.main;
        VideoPlayer.gameObject.SetActive(false);
    }

    void Update()
    {
        if (_gameStatus == GameStatus.Shoot)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                _gameStatus = GameStatus.Gallery;
                InstancePhotoGallery.EnterGallery();
                CanvasShoot.gameObject.SetActive(false);
                CanvasGallery.gameObject.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                if (TimeCapture + _lastCaptureTime < Time.time)
                {
                    _gameStatus = GameStatus.Shooting;
                    CanvasShoot.gameObject.SetActive(false);
                    _lastCaptureTime = Time.time;
                }                
            }
        }
        else if(_gameStatus == GameStatus.Shooting)
        {
            if (!_justTaken)
            {
                AudioPlayer.clip = SFXClick;
                AudioPlayer.Play();
                VideoPlayer.gameObject.SetActive(true);
                //VideoPlayer.Play();
                InstancePhotoGallery.Capture();
                CameraCaptureEvent.Raise();
                _justTaken = true;
            }
            if(TimeCapture + _lastCaptureTime < Time.time)
            {
                _gameStatus = GameStatus.Shoot;
                _justTaken = false;
                VideoPlayer.gameObject.SetActive(false);
                CanvasShoot.gameObject.SetActive(true);
            }
        }
        // Gallery
        else if(_gameStatus == GameStatus.Gallery)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                _gameStatus = GameStatus.Shoot;
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
