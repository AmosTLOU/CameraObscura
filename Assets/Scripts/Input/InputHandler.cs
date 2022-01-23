using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private float currentPitch = 0f;
    [SerializeField] private float currentRoll = 0f;
    [SerializeField] private float currentYaw = 0f;
    [SerializeField] private float distance = 0f;
    [SerializeField] private bool isFlashButtonDown = false;
    [SerializeField] private bool isShutterButtonDown = false;

    private float prevPitch, prevRoll, prevYaw, prevDistance;

    SerialPort port = new SerialPort("COM3", 115200);
    void Start()
    {
        port.Open();
        port.ReadTimeout = 1;
        port.NewLine = "\n";
    }

    void Update()
    {
        if (port.IsOpen && port.BytesToRead > 32)
        {
            print("PORT OPEN!!!!!!");
            try
            {
                string input = port.ReadLine();
                var inputMeta = input.Split(':');
                SetInputData(inputMeta);
                print(input);
            }
            catch (System.Exception ex)
            {
                print(ex.Message);
            }
        }
    }

    public Vector3 GetRotationValues()
    {
        return new Vector3(currentPitch-prevPitch, currentRoll-prevRoll, currentYaw-prevYaw);
    }

    public float GetZoomValue()
    {
        return distance-prevDistance;
    }

    private void SetInputData(string[] inputMeta)
    {
        switch(inputMeta[0])
        {
            case "Button":
                if(inputMeta[1] == "1")
                {
                    isFlashButtonDown = true;
                }
                if(inputMeta[1] == "2")
                {
                    isShutterButtonDown = true;
                }
                break;
            case "Pitch":
                prevPitch = currentPitch;
                currentPitch = float.Parse(inputMeta[1]);
                break;
            case "Roll":
                prevRoll = currentRoll;
                currentRoll = float.Parse(inputMeta[1]);
                break;
            case "Yaw":
                prevYaw = currentYaw;
                currentYaw = float.Parse(inputMeta[1]);
                break;
            case "Distance":
                prevDistance = distance;
                distance = float.Parse(inputMeta[1]);
                break;
            default:
                print("un-identified input.");
                break;
        }
    }
}
