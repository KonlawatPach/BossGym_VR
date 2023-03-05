using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundwithUser : MonoBehaviour
{
    private AudioSource trainerAudio;
    public AudioClip trainer7;

    void Start()
    {
        trainerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playtrainer7()
    {
        trainerAudio.PlayOneShot(trainer7);
    }
}
