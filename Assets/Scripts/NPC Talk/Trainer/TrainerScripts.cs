using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TrainerScripts : MonoBehaviour
{
    private Animator trainerAnimate;
    private AudioSource trainerAudio;

    private bool isDelay = false;
    private float startTime = 0;
    private float delayTime = 0;

    public AudioClip[] trainerClip;
    public AudioClip punchSound;
    public AudioClip slapSound;
    public AudioClip alertSFX;
    public AudioClip hurtSFX;
    public AudioClip blockSFX;
    public int audioClipIndex = 0;

    //item and another
    public XRState xrstatus;
    public SoundwithUser soundwithUser;
    public DimpleIntro dimpleIntro;

    public GameObject grove;
    private bool showGrove = false;
    private bool getGrove = false;

    private bool letPunch = false;
    private int punchCount = 0;

    public GameObject cameraXR;
    public GameObject leftController;
    public GameObject rightController;
    public GameObject FistLeft;
    public GameObject FistRight;

    private string fightingState = "prepare";   //prepare, attack, nextattack
    private string[] hookType = { "FL", "FR", "FL" };
    private int hookTypeIndex = 0;
    private int blockCount = 0;

    public TeleportationAnchor anchor = null;
    public TeleportationProvider provider = null;

    private void TeleportToPointTwo()
    {
        if (anchor && provider)
        {
            TeleportRequest request = CreateRequest();
            provider.QueueTeleportRequest(request);
        }
    }

    private TeleportRequest CreateRequest()
    {
        Transform anchorTransform = anchor.teleportAnchorTransform;

        TeleportRequest request = new TeleportRequest()
        {
            requestTime = Time.time,
            matchOrientation = anchor.matchOrientation,

            destinationPosition = anchorTransform.position,
            destinationRotation = anchorTransform.rotation
        };

        return request;
    }

    void Start()
    {
        trainerAudio = GetComponent<AudioSource>();
        trainerAnimate = GetComponent<Animator>();
        xrstatus = GameObject.Find("XR Rig").GetComponent<XRState>();
        soundwithUser = GameObject.Find("XR Rig").GetComponent<SoundwithUser>();
        dimpleIntro = GameObject.Find("Dimples").GetComponent<DimpleIntro>();

        trainerAudio.clip = trainerClip[0];
        trainerAudio.Play();
    }

    [System.Obsolete]
    void Update()
    {
        if (isDelay)
        {
            if (Time.time - startTime > delayTime)
            {
                isDelay = false;
            }
        }

        else if (audioClipIndex == 0)
        {
            nextClip();
        }

        else if (audioClipIndex == 1)
        {
            trainerAnimate.SetInteger("state", 0);
            nextClip();
        }

        else if (audioClipIndex == 1 || audioClipIndex == 2)
        {
            trainerAnimate.SetInteger("state", 1);
            nextClip();
        }

        else if (audioClipIndex == 3)
        {
            if (!showGrove && !getGrove)
            {
                trainerAnimate.SetInteger("state", 2);
                showGrove = true;
                startDelay(0.5f);
            }
            else if (showGrove && !getGrove)
            {
                grove.SetActive(true);
            }
            else
            {
                grove.SetActive(false);
                trainerAnimate.SetInteger("state", 0);
                letPunch = true;
                nextClip();
            }
        }
        else if (audioClipIndex == 4)
        {
            if (!letPunch)
            {
                trainerAnimate.SetInteger("state", 3);
                Invoke("backToIdle", 2f);
                nextClip();
            }
        }
        else if (audioClipIndex == 5)
        {
            trainerAnimate.SetInteger("state", 4);
            if (hookTypeIndex < 3)
            {
                if (fightingState == "prepare")
                {
                    showAlert(hookType[hookTypeIndex]);
                    startDelay(0.3f);
                    fightingState = "attack";
                }
                else if (fightingState == "attack")
                {
                    if (hookType[hookTypeIndex] == "FR") trainerAnimate.SetInteger("state", 6);
                    else trainerAnimate.SetInteger("state", 5);
                    fightingState = "nextattack";
                    startDelay(1f);
                }
                else if (fightingState == "nextattack")
                {
                    hideAlert(hookType[hookTypeIndex]);
                    checkDamage(hookType[hookTypeIndex]);
                    hookTypeIndex++;
                    trainerAnimate.SetInteger("state", 4);
                    fightingState = "prepare";
                    startDelay(0.7f);

                    if (hookTypeIndex == 3 && blockCount < 2)
                    {
                        trainerAudio.clip = trainerClip[6];
                        trainerAudio.Play();
                        startDelay(trainerAudio.clip.length);
                        hookTypeIndex = 0;
                        blockCount = 0;
                    }
                }
            }
            else
            {
                trainerAnimate.SetInteger("state", 0);
                nextClip();
            }
        }
        else if (audioClipIndex == 7)
        {
            TeleportToPointTwo();
            gameObject.transform.position = new Vector3(-214.22f, -10.59982f, 68.48f);
            gameObject.transform.Rotate(0f, 202.75f , 0f);
            trainerAudio.volume = 0f;
            nextClip();
            soundwithUser.playtrainer7();
            Invoke("runningSonRam", 8f);
        }
        else
        {
            
        }
    }

    void runningSonRam()
    {
        dimpleIntro.startDimpleAnimation();
    }

    void nextClip()
    {
        trainerAudio.clip = trainerClip[audioClipIndex];
        trainerAudio.Play();

        if(audioClipIndex == 5) audioClipIndex = audioClipIndex + 1;
        audioClipIndex = audioClipIndex + 1;

        if(audioClipIndex == 3) startDelay(trainerAudio.clip.length - 2);
        else startDelay(trainerAudio.clip.length);
    }

    void startDelay(float delayT)
    {
        delayTime = delayT;
        startTime = Time.time;
        isDelay = true;
    }

    public void setTrueGetGrove()
    {
        getGrove = true;
    }

    void backToIdle()
    {
        trainerAnimate.SetInteger("state", 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (letPunch)
        {
            if ((other.CompareTag("LeftHand") && xrstatus.isLeftGripActive) || (other.CompareTag("RightHand") && xrstatus.isRightGripActive))
            {
                trainerAudio.PlayOneShot(punchSound);
                if (punchCount < 2) punchCount++;
                else letPunch = false;
            }
            else if ((other.CompareTag("LeftHand") && !xrstatus.isLeftGripActive) || (other.CompareTag("RightHand") && !xrstatus.isRightGripActive))
            {
                trainerAudio.PlayOneShot(slapSound);
                if (punchCount < 2) punchCount++;
                else letPunch = false;
            }
        }
    }

    void showAlert(string alertType)
    {
        if (alertType == "FL")
        {
            FistLeft.SetActive(true);
        }
        else if (alertType == "FR")
        {
            FistRight.SetActive(true);
        }

        trainerAudio.PlayOneShot(alertSFX);

    }
    void hideAlert(string alertType)
    {
        if (alertType == "FL")
        {
            FistLeft.SetActive(false);
        }
        else if (alertType == "FR")
        {
            FistRight.SetActive(false);
        }
    }

    [System.Obsolete]
    void checkDamage(string hooktype)
    {
        if (hooktype == "FL")
        {
            if (xrstatus.isLeftGripActive && leftController.transform.position.z > cameraXR.transform.position.z &&
                leftController.transform.position.y > cameraXR.transform.position.y - 0.02f)
            {
                blockCount++;
                trainerAudio.PlayOneShot(blockSFX);
                ActivateLeftHaptic();
            }
            else
            {
                trainerAudio.PlayOneShot(hurtSFX);
            }
        }
        else
        {
            if (xrstatus.isRightGripActive && rightController.transform.position.z+0.1f > cameraXR.transform.position.z &&
                rightController.transform.position.y > cameraXR.transform.position.y - 0.02f)
            {
                blockCount++;
                trainerAudio.PlayOneShot(blockSFX);
                ActivateRightHaptic();
            }
            else
            {
                trainerAudio.PlayOneShot(hurtSFX);
            }
        }
    }

    [System.Obsolete]
    void ActivateLeftHaptic()
    {
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();

        UnityEngine.XR.InputDevices.GetDevicesWithRole(UnityEngine.XR.InputDeviceRole.LeftHanded, devices);

        foreach (var device in devices)
        {
            UnityEngine.XR.HapticCapabilities capabilities;
            if (device.TryGetHapticCapabilities(out capabilities))
            {
                if (capabilities.supportsImpulse)
                {
                    uint channel = 0;
                    float amplitude = 0.5f;
                    float duration = 0.25f;
                    device.SendHapticImpulse(channel, amplitude, duration);
                }
            }
        }
    }

    [System.Obsolete]
    void ActivateRightHaptic()
    {
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();

        UnityEngine.XR.InputDevices.GetDevicesWithRole(UnityEngine.XR.InputDeviceRole.RightHanded, devices);

        foreach (var device in devices)
        {
            UnityEngine.XR.HapticCapabilities capabilities;
            if (device.TryGetHapticCapabilities(out capabilities))
            {
                if (capabilities.supportsImpulse)
                {
                    uint channel = 0;
                    float amplitude = 0.5f;
                    float duration = 0.25f;
                    device.SendHapticImpulse(channel, amplitude, duration);
                }
            }
        }
    }
}
