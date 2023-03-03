using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{
    public TextMeshProUGUI playerHP;
    public GameObject hurt1;
    public GameObject hurt2;
    public GameObject hurt3;

    public int hp = 3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hp == 2)
        {
            hurt1.SetActive(true);
        }
        else if(hp == 1)
        {
            hurt1.SetActive(false);
            hurt2.SetActive(true);
        }
        else if(hp < 1)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void getDamage()
    {
        hp -= 1;
        playerHP.text = hp.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Fist"))
        //{
        //    getDamage();
        //}
    }
}
