using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private float pitch = 0f;
    [SerializeField] private float roll = 0f;
    [SerializeField] private float yaw = 0f;
    [SerializeField] private float distance = 0f;
    [SerializeField] private bool isFlashButtonDown = false;
    [SerializeField] private bool isShutterButtonDown = false;

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

            }
        }
    }

    public Vector3 GetRotationValues()
    {
        return new Vector3(pitch, roll, yaw);
    }

    public float GetZoomValue()
    {
        return distance;
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
                pitch = float.Parse(inputMeta[1]);
                break;
            case "Roll":
                roll = float.Parse(inputMeta[1]);
                break;
            case "Yaw":
                yaw = float.Parse(inputMeta[1]);
                break;
            case "Distance":
                distance = float.Parse(inputMeta[1]);
                break;
            default:
                print("un-identified input.");
                break;
        }
    }
}
