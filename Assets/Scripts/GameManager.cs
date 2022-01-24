using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HideInInspector]
public enum GameState
{
    Shoot,
    Shooting,
    Gallery,
}

// GameManager Class, charge of input and interaction
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
    GameState _gameState;
    float _lastCaptureTime;
    bool _justTaken;

    void Start()
    {
        _gameState = GameState.Shoot;
        _lastCaptureTime = float.NegativeInfinity;
        _justTaken = false;
        _mainCamera = Camera.main;
        VideoPlayer.gameObject.SetActive(false);
    }

    void Update()
    {
        // Shoot
        if (_gameState == GameState.Shoot)
        {
            // Open the gallery
            if (Input.GetKeyDown(KeyCode.P))
            {
                _gameState = GameState.Gallery;
                InstancePhotoGallery.EnterGallery();
                CanvasShoot.gameObject.SetActive(false);
                CanvasGallery.gameObject.SetActive(true);
            }
            // Capture
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                if (TimeCapture + _lastCaptureTime < Time.time)
                {
                    _gameState = GameState.Shooting;
                    CanvasShoot.gameObject.SetActive(false);
                    _lastCaptureTime = Time.time;
                }                
            }
        }
        // The moment of Shooting 
        else if(_gameState == GameState.Shooting)
        {
            if (!_justTaken)
            {
                AudioPlayer.clip = SFXClick;
                AudioPlayer.Play();
                VideoPlayer.gameObject.SetActive(true);
                InstancePhotoGallery.Capture();
                CameraCaptureEvent.Raise();
                _justTaken = true;
            }
            if(TimeCapture + _lastCaptureTime < Time.time)
            {
                _gameState = GameState.Shoot;
                _justTaken = false;
                VideoPlayer.gameObject.SetActive(false);
                CanvasShoot.gameObject.SetActive(true);
            }
        }
        // Gallery
        else if(_gameState == GameState.Gallery)
        {
            // Return to shoot
            if (Input.GetKeyDown(KeyCode.P))
            {
                _gameState = GameState.Shoot;
                CanvasShoot.gameObject.SetActive(true);
                CanvasGallery.gameObject.SetActive(false);
            }
            // Review details of the clue if there is any
            if (Input.GetKeyDown(KeyCode.Space))
            {
                InstancePhotoGallery.OpenOrCloseDetails();
            }
            // Scroll pictures, left-ward
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                InstancePhotoGallery.ShowPrevPhoto();
            }
            // Scroll pictures, right-ward
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                InstancePhotoGallery.ShowNextPhoto();
            }
        }
    }

    public GameState GetGameState()
    {
        return _gameState;
    }

    public void FindClue(Vector3 viewPos, string clueName)
    {
        InstancePhotoGallery.AddPrompt(viewPos, clueName);
    }
}
