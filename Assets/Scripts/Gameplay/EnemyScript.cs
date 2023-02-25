using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using UnityEngine.XR;
using TMPro;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI BossHPText;
    public Image healthBar;
    public XRState xrstatus;
    public AudioClip punchSound;
    public AudioClip slapSound;
    private AudioSource playerAudio;

    public string bossname = "";
    public float maxBossHP = 100f;
    public float BossHP = 100f;

    void Start()
    {
        BossHPText.text = bossname;
        playerAudio = GetComponent<AudioSource>();
        xrstatus = GameObject.Find("XR Rig").GetComponent<XRState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("LeftHand") && xrstatus.isLeftGripActive) || (other.CompareTag("RightHand") && xrstatus.isRightGripActive))
        {
            playerAudio.PlayOneShot(punchSound);
            BossHP -= Random.Range(7, 10);

            BossHPText.text = bossname;
            healthBar.fillAmount = BossHP / maxBossHP;
        }
        else if ((other.CompareTag("LeftHand") && !xrstatus.isLeftGripActive) || (other.CompareTag("RightHand") && !xrstatus.isRightGripActive))
        {
            playerAudio.PlayOneShot(slapSound);
            BossHP -= Random.Range(1, 2);

            BossHPText.text = bossname;
            healthBar.fillAmount = BossHP / maxBossHP;
        }
    }




    ///////////////////////START FUNCTION//////////////////////////
    public TeleportationAnchor anchor = null;
    public TeleportationProvider provider = null;

    private void Awake()
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
}
