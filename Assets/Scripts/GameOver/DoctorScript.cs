using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoctorScript : MonoBehaviour
{
    private Animator doctorAnimate;
    public AudioClip hurtSFX;
    private AudioSource doctorAudio;

    private bool isDying = false;
    
    
    // Start is called before the first frame update

    void Start()
    {
        doctorAudio = GetComponent<AudioSource>();
        doctorAnimate = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftHand") && !isDying)
        {
            doctorAnimate.SetBool("isDying", true);
            doctorAudio.PlayOneShot(hurtSFX);
            isDying = true;
        }
    }
}
