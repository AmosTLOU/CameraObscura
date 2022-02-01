using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{

    public GameObject[] points;
    public Transform KillerTransform;
    public Animator KillerAnimator;
    public Animator VictimAnimator;
    //float rotspeed;ef
    public float Speed;
    public int A;

    PhaseManager _phaseManager;
    
    int current = 0;
    float radius = 0.5f;


    private void Start()
    {
        _phaseManager = FindObjectOfType<PhaseManager>();
    }

    private void Update()
    {
        if (_phaseManager.GetPhase() == Phase.Killing1)
            Killing();
        else if (_phaseManager.GetPhase() == Phase.Room1)
            RunawayAfterKilling1();
        else if (_phaseManager.GetPhase() == Phase.Tran1_2)
            TransitionBetween1and2();
        else if (_phaseManager.GetPhase() == Phase.Tran2_3)
            TransitionBetween2and3();
        else if (_phaseManager.GetPhase() == Phase.Killing3)
            Killing3();


        //if(current==0)
        //{
        //    a = 0;
        //}
        //if (current == 1)
        //{
        //    a = 0;
        //}
        //if (current == 2)
        //{
        //    a = 0;
        //}
        //if (current == 3)
        //{
        //    a = -90;
        //}
        //if (current == 4)
        //{
        //    a = 90;
        //}
        //if (current == 5)
        //{
        //    a = 90;
        //}
        //if (current == 6)
        //{
        //    a = 0;
        //}
        //if (current == 7)
        //{
        //    a = -90;
        //}
        //if (current == 10)
        //{
        //    a = 90;
        //}
        //if (current == 11)
        //{
        //    a = 0;
        //}
        //if (current == 12)
        //{
        //    a = -90;
        //}

        ////from here this is the next run sequence after the clue 2i.e from room 2 to 3

        //if (current == 13)
        //{
        //    a = 90;
        //}
        //if (current == 14)
        //{
        //    a = 180;
        //}
        //if (current == 15)
        //{
        //    a = -90;
        //}
        //if (current == 16)
        //{
        //    a = 90;
        //}
        //if (current == 17)
        //{
        //    a = 90;
        //}
        //if (current == 18)
        //{
        //    a = 180;
        //}
        //Vector3 newRotation = new Vector3(0, a, 0);
        //KillerTransform.eulerAngles = newRotation; 



        //KillerTransform.LookAt(points[current].transform);
        //current++;

    }

    void Killing()
    {
        KillerAnimator.SetBool("Killing", true);
        KillerAnimator.SetBool("Running", false);
    }

    void RunawayAfterKilling1()
    {
        if (Vector3.Distance(points[current].transform.position, KillerTransform.position) < radius)
            current++;
        KillerTransform.position = Vector3.MoveTowards(KillerTransform.position, points[current].transform.position, Time.deltaTime * Speed);
    }

    void TransitionBetween1and2()
    {
        // Flickering lights and Sound of progress
    }


    void TransitionBetween2and3()
    {
        // Killer runs, killer tries to kill but fails, killer flees
    }

    void Killing3()
    {
        // Killing, victim died, killer face towards the player
    }

}