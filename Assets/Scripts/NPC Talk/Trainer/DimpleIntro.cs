using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class DimpleIntro : MonoBehaviour
{
    private Animator dimpleAnimate;
    private bool startAnim = false;
    private bool ispunch = false;
    private bool isrotate = false;

    void Start()
    {
        dimpleAnimate = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startAnim)
        {
            if (gameObject.transform.position.z > 72.56f)
            {
                transform.Translate(Vector3.right * Time.deltaTime * 1f);
            }
            else
            {
                if (!ispunch)
                {
                    if (!isrotate)
                    {
                        isrotate = true;
                        gameObject.transform.Rotate(0f, 10f, 0f);
                    }

                    dimpleAnimate.SetFloat("speed", 0.1f);
                    if (gameObject.transform.position.x > -216.3f)
                    {
                        dimpleAnimate.SetFloat("speed", 0f);
                        Invoke("Punch", 0.5f);
                    }
                }
            }
        }
    }

    public void startDimpleAnimation()
    {
        startAnim = true;
    }

    private void Punch()
    {
        if (!ispunch)
        {
            dimpleAnimate.SetBool("hookLeft", true);
            ispunch = true;
            Invoke("RunBack", 1.5f);
        }
    }

    private void RunBack()
    {
        dimpleAnimate.SetBool("hookLeft", false);
        dimpleAnimate.SetFloat("speed", -0.5f);
        if (gameObject.transform.position.x > -226.64f)
        {
            dimpleAnimate.SetFloat("speed", 0f);
        }
    }
}
