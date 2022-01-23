using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float MaxOffsetPosX;
    public float MaxOffsetPosY;
    public float SpeedZoom;
    public float SpeedRotateXY;
    public float SpeedRotateZ;
    public float MinFOV;
    public float MaxFOV;

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
   
        float offset_r_y = Input.GetAxis("Mouse X");
        float offset_r_x = Input.GetAxis("Mouse Y");
        float offset_r_z = Input.GetAxis("Fire1");
        offset_r_z -= Input.GetAxis("Fire2");
        _camRot.x -= SpeedRotateXY * offset_r_x * Time.deltaTime;
        _camRot.y += SpeedRotateXY * offset_r_y * Time.deltaTime;
        _camRot.z -= SpeedRotateZ * offset_r_z * Time.deltaTime;

        float offset_pos_x = Input.GetAxis("Horizontal");
        float offset_pos_y = Input.GetAxis("Vertical");
        _camPos.x += offset_pos_x * Time.deltaTime;
        _camPos.x = Mathf.Clamp(_camPos.x, _camInitialPos.x - MaxOffsetPosX, _camInitialPos.x + MaxOffsetPosX);
        _camPos.y += offset_pos_y * Time.deltaTime;
        _camPos.y = Mathf.Clamp(_camPos.y, _camInitialPos.y - MaxOffsetPosY, _camInitialPos.y + MaxOffsetPosY);

        float offset_zoom = Input.GetAxis("Mouse ScrollWheel");
        _camFOV -= SpeedZoom * offset_zoom * Time.deltaTime; ;
        _camFOV = Mathf.Clamp(_camFOV, MinFOV, MaxFOV);

        transform.eulerAngles = _camRot;
        transform.position = _camPos;
        Camera.main.fieldOfView = _camFOV;
        
    }
}
