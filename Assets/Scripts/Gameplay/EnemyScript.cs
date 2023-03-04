using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyScript : MonoBehaviour
{
    private Animator enemyAnimate;

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
        enemyAnimate = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BossHP <= 0)
        {
            enemyAnimate.SetBool("isDying", true);
        }
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
