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
        // Show time and date on camera screen
        TimeAndDate.text = System.DateTime.Now.ToString();
        // Show the extent of zoom in/out with a slider
        IndicatorZoom.value = (InstanceCameraControl.MaxFOV - Camera.main.fieldOfView) / (InstanceCameraControl.MaxFOV - InstanceCameraControl.MinFOV);
    }
}
