using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float maxOffsetPosX;
    public float maxOffsetPosY;
    public float speedZoom;
    public float speedRotateXY;
    public float speedRotateZ;
    public float minFOV;
    public float maxFOV;
    public bool isKbMouseEnabled = false;

    [SerializeField] private InputHandler inputHandler;

    Vector3 _camRot;
    Vector3 _camPos;
    float _camFOV;
    Vector3 _camInitialPos;
    Vector3 _camInitialRot;
    float _camInitialFOV;
    //float _timeReset;
    //float _intervalFrozen;

    private void Start()
    {
        _camPos = transform.position;
        _camRot = transform.eulerAngles;
        _camFOV = Camera.main.fieldOfView;

        _camInitialPos = transform.position;
        _camInitialRot = transform.eulerAngles;
        _camInitialFOV = Camera.main.fieldOfView;

        //_timeReset = float.NegativeInfinity;
        //_intervalFrozen = 0.5f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _camRot = _camInitialRot;
            _camPos = _camInitialPos;
            _camFOV = _camInitialFOV;
            //_timeReset = Time.time;
            return;
        }
        //if (Time.time <= _timeReset + _intervalFrozen)
        //    return;
        var rotationValues = inputHandler.GetRotationValues();
        float offset_r_y = isKbMouseEnabled ? Input.GetAxis("Mouse X") : rotationValues.x;
        float offset_r_x = isKbMouseEnabled ? Input.GetAxis("Mouse Y") : rotationValues.y;
        // float offset_r_z = isKbMouseEnabled ? Input.GetAxis("Fire1");
        // offset_r_z -= Input.GetAxis("Fire2");
        _camRot.x -= speedRotateXY * offset_r_x * Time.deltaTime;
        _camRot.y += speedRotateXY * offset_r_y * Time.deltaTime;
        // m_camRot.z -= speedRotateZ * offset_r_z * Time.deltaTime;

        float offset_pos_x = Input.GetAxis("Horizontal");
        float offset_pos_y = Input.GetAxis("Vertical");
        _camPos.x += offset_pos_x * Time.deltaTime;
        _camPos.x = Mathf.Clamp(_camPos.x, _camInitialPos.x - maxOffsetPosX, _camInitialPos.x + maxOffsetPosX);
        _camPos.y += offset_pos_y * Time.deltaTime;
        _camPos.y = Mathf.Clamp(_camPos.y, _camInitialPos.y - maxOffsetPosY, _camInitialPos.y + maxOffsetPosY);

        float offset_zoom = isKbMouseEnabled ? Input.GetAxis("Mouse ScrollWheel") : inputHandler.GetZoomValue();
        _camFOV -= speedZoom * offset_zoom * Time.deltaTime; ;
        _camFOV = Mathf.Clamp(_camFOV, minFOV, maxFOV);

        transform.eulerAngles = _camRot;
        transform.position = _camPos;
        // Camera.main.fieldOfView = m_camFOV;
        
    }
}
