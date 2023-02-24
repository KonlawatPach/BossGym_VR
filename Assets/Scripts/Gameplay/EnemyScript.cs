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
    public XRState xrstatus;
    public int BossHP = 100;

    void Start()
    {
        xrstatus = GameObject.Find("XR Rig").GetComponent<XRState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftHand") && xrstatus.isLeftGripActive)
        {
            BossHP -= Random.Range(4, 6);
            BossHPText.text = "BOSS HP: " + BossHP.ToString();
        }
        else if (other.CompareTag("RightHand") && xrstatus.isRightGripActive)
        {
            BossHP -= Random.Range(4, 6);
            BossHPText.text = "BOSS HP: " + BossHP.ToString();
        }
    }

}
