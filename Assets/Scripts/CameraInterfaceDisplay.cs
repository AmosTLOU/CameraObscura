using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraInterfaceDisplay : MonoBehaviour
{
    public TextMeshProUGUI TimeAndDate;
    public Slider IndicatorZoom;
    public CameraControl instanceCameraControl;

    void Start()
    {
        
    }

    void Update()
    {
        TimeAndDate.text = System.DateTime.Now.ToString();
        IndicatorZoom.value = (instanceCameraControl.maxFOV - Camera.main.fieldOfView) / (instanceCameraControl.maxFOV - instanceCameraControl.minFOV);
    }
}
