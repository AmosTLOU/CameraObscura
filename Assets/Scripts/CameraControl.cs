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

    Vector3 m_camRot;
    Vector3 m_camPos;
    float m_camFOV;
    Vector3 m_camInitialPos;
    Vector3 m_camInitialRot;
    float m_camInitialFOV;
    //float m_timeReset;
    //float m_intervalFrozen;

    private void Start()
    {
        m_camPos = transform.position;
        m_camRot = transform.eulerAngles;
        m_camFOV = Camera.main.fieldOfView;

        m_camInitialPos = transform.position;
        m_camInitialRot = transform.eulerAngles;
        m_camInitialFOV = Camera.main.fieldOfView;

        //m_timeReset = float.NegativeInfinity;
        //m_intervalFrozen = 0.5f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            m_camRot = m_camInitialRot;
            m_camPos = m_camInitialPos;
            m_camFOV = m_camInitialFOV;
            //m_timeReset = Time.time;
            return;
        }
        //if (Time.time <= m_timeReset + m_intervalFrozen)
        //    return;
   
        float offset_r_y = Input.GetAxis("Mouse X");
        float offset_r_x = Input.GetAxis("Mouse Y");
        float offset_r_z = Input.GetAxis("Fire1");
        offset_r_z -= Input.GetAxis("Fire2");
        m_camRot.x -= speedRotateXY * offset_r_x * Time.deltaTime;
        m_camRot.y += speedRotateXY * offset_r_y * Time.deltaTime;
        m_camRot.z -= speedRotateZ * offset_r_z * Time.deltaTime;

        float offset_pos_x = Input.GetAxis("Horizontal");
        float offset_pos_y = Input.GetAxis("Vertical");
        m_camPos.x += offset_pos_x * Time.deltaTime;
        m_camPos.x = Mathf.Clamp(m_camPos.x, m_camInitialPos.x - maxOffsetPosX, m_camInitialPos.x + maxOffsetPosX);
        m_camPos.y += offset_pos_y * Time.deltaTime;
        m_camPos.y = Mathf.Clamp(m_camPos.y, m_camInitialPos.y - maxOffsetPosY, m_camInitialPos.y + maxOffsetPosY);

        float offset_zoom = Input.GetAxis("Mouse ScrollWheel");
        m_camFOV -= speedZoom * offset_zoom * Time.deltaTime; ;
        m_camFOV = Mathf.Clamp(m_camFOV, minFOV, maxFOV);

        transform.eulerAngles = m_camRot;
        transform.position = m_camPos;
        Camera.main.fieldOfView = m_camFOV;
        
    }
}
