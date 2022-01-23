using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraInterfaceDisplay : MonoBehaviour
{
    public TextMeshProUGUI TimeAndDate;
    public Slider IndicatorZoom;
    public CameraControl InstanceCameraControl;

    void Start()
    {
        
    }

    void Update()
    {
        TimeAndDate.text = System.DateTime.Now.ToString();
        IndicatorZoom.value = (InstanceCameraControl.maxFOV - Camera.main.fieldOfView) / (InstanceCameraControl.maxFOV - InstanceCameraControl.minFOV);
    }
}
