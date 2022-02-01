using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_anim : MonoBehaviour
{
    // Start is called before the first frame update
    



    public GameObject man;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("c"))
        {
            man.GetComponent<Animator>().Play("demo_trail");
        }
    }
}
