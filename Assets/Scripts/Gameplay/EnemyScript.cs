using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

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
            Invoke("changeSceneFade", 5f);
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

    void changeSceneFade()
    {
        StartFade(true);
        Invoke("changeScene", 3f);
    }

    public Image fadeImage;
    public float fadeDuration = 1f;

    public void StartFade(bool fadeIn)
    {
        StartCoroutine(FadeCanvas(fadeIn));
    }

    private IEnumerator FadeCanvas(bool fadeIn)
    {
        Color startColor = fadeImage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, fadeIn ? 1f : 0f);
        float currentTime = 0f;

        while (currentTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(startColor.a, endColor.a, currentTime / fadeDuration);
            fadeImage.color = new Color(endColor.r, endColor.g, endColor.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    void changeScene()
    {
        SceneManager.LoadScene("Story 3");
    }
}
