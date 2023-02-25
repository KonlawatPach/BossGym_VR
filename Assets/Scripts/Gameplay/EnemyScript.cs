using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI bossName;
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
        bossName.text = bossname;
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
        }
        else if ((other.CompareTag("LeftHand") && !xrstatus.isLeftGripActive) || (other.CompareTag("RightHand") && !xrstatus.isRightGripActive))
        {
            playerAudio.PlayOneShot(slapSound);
            BossHP -= Random.Range(1, 2);
        }

        bossName.text = bossname;
        healthBar.fillAmount = BossHP / maxBossHP;
    }
}
