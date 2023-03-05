using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class DimpleIntro : MonoBehaviour
{
    private Animator dimpleAnimate;
    private TrainerScripts trainer;
    private bool startAnim = false;
    private bool ispunch = false;
    private bool isrotate = false;
    private bool isrunback = false;

    void Start()
    {
        dimpleAnimate = GetComponent<Animator>();
        trainer = GameObject.Find("Trainer").GetComponent<TrainerScripts>();
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
                if (isrunback)
                {
                    if (gameObject.transform.position.x < -219.64f)
                    {
                        dimpleAnimate.SetFloat("speed", 0f);
                        SceneManager.LoadScene("Stage 1");
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
            Invoke("RunBack", 0.5f);
        }
    }

    private void RunBack()
    {
        dimpleAnimate.SetBool("hookLeft", false);
        trainer.died();
        gameObject.transform.Rotate(0f, -10f, 0f);
        dimpleAnimate.SetFloat("speed", -0.5f);
        isrunback = true;
    }
}
