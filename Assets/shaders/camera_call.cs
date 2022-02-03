using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_call : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject opening_cam;
    public GameObject main_cam;

    // Update is called once per frame
    void Update()
    {
        opening_cam.SetActive(true);
    }
}
