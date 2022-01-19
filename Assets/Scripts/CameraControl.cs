using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float maxOffset_pos_x;
    public float maxOffset_pos_y;
    public float speed_rotate_z;

    Vector3 m_camRot;
    Vector3 m_camPos;
    float m_camFOV;
    float m_camInitialFOV;
    float m_camInitialPosX;
    float m_camInitialPosY;

    private void Start()
    {
        m_camRot = transform.eulerAngles;
        m_camPos = transform.position;
        m_camInitialFOV = Camera.main.fieldOfView;
        m_camFOV = Camera.main.fieldOfView;
        m_camInitialPosX = transform.position.x;
        m_camInitialPosY = transform.position.y;
    }

    private void Update()
    {
        float offset_r_y = Input.GetAxis("Mouse X");
        float offset_r_x = Input.GetAxis("Mouse Y");
        float offset_r_z = Input.GetAxis("Mouse ScrollWheel");
        m_camRot.x -= offset_r_x;
        m_camRot.y += offset_r_y;
        m_camRot.z -= speed_rotate_z * offset_r_z;

        float offset_pos_x = Input.GetAxis("Horizontal");
        float offset_pos_y = Input.GetAxis("Vertical");
        m_camPos.x += offset_pos_x;
        m_camPos.x = Mathf.Clamp(m_camPos.x, m_camInitialPosX - maxOffset_pos_x, m_camInitialPosX + maxOffset_pos_x);
        m_camPos.y += offset_pos_y;
        m_camPos.y = Mathf.Clamp(m_camPos.y, m_camInitialPosY - maxOffset_pos_y, m_camInitialPosY + maxOffset_pos_y);

        float offset_zoom = Input.GetAxis("Fire1");
        offset_zoom += -1 * Input.GetAxis("Fire2");
        m_camFOV -= offset_zoom;
        m_camFOV = Mathf.Clamp(m_camFOV, 10f, m_camInitialFOV);

        transform.eulerAngles = m_camRot;
        transform.position = m_camPos;
        Camera.main.fieldOfView = m_camFOV;
    }
}
