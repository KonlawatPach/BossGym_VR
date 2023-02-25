using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    public TextMeshProUGUI playerHP;
    public int hp = 3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getDamage()
    {
        hp -= 1;
        playerHP.text = hp.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fist"))
        {
            getDamage();
        }
    }
}
