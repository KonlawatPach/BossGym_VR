using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimpleBehavior : MonoBehaviour
{
    private Animator dimpleAnimate;
    public bool isAttacking = false;
    public bool isNearPlayer = false;


    void Start()
    {
        dimpleAnimate = GetComponent<Animator>();
        InvokeRepeating("randomAttack", 5f, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        //dimpleAnimate.SetFloat("speed", 0.4f);
        //dimpleAnimate.SetBool("See_Car", false);
    }

    void randomAttack()
    {
        if (!isAttacking)
        {
            int attackIndex = Random.Range(0, 1);
            isAttacking = true;

            if (attackIndex == 0) attackSet1();
            if (attackIndex == 1) attackSet2();
        }
    }

    void attackSet1()
    {
        


        dimpleAnimate.SetFloat("speed", 0.5f);
        Invoke("x", 1f);
    }

    void attackSet2()
    {

    }

    void x()
    {
        dimpleAnimate.SetFloat("speed", 0f);
    }

    void fistLeft()
    {

    }
}
