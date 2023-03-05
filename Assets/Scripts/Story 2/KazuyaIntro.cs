using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KazuyaIntro : MonoBehaviour
{
    private Animator kazuyaAnim;
    private bool walking = false;
    private bool jumpingup = false;
    private bool jumpingdown = false;

    void Start()
    {
        kazuyaAnim = GetComponent<Animator>();
        Invoke("letWalking", 30f);
    }

    void Update()
    {
        if (walking)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 2f);
            if (transform.position.x > -4.2)
            {
                walking = false;
                jumpingup = true;
                kazuyaAnim.SetBool("jump", true);
            }
        }
        else if (jumpingup)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 2f);
            transform.Translate(Vector3.up * Time.deltaTime * 5f);
            if (transform.position.y > 1.65)
            {
                jumpingup = false;
                jumpingdown = true;
                transform.Rotate(0f, 20f, 0f);
            }
        }
        else if (jumpingdown)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 3f);
            transform.Translate(Vector3.down * Time.deltaTime * 5f);
            if (transform.position.y < -0.28)
            {
                jumpingdown = false;
                Invoke("FadeContinueCanvas", 1f);
            }
        }
        else
        {

        }
    }

    void letWalking()
    {
        walking = true;
    }

    public Image fadeImage;
    public TextMeshProUGUI fadeText;
    public float fadeDuration = 1f;

    void FadeContinueCanvas()
    {
        StartFade(true);
        Invoke("changeScene", 2f);
    }

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
            fadeText.color = new Color(255f, 255f, 255f, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    void changeScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
