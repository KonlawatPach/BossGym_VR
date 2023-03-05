using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimpleIntro : MonoBehaviour
{
    private Animator dimpleAnimate;
    private bool startAnim = false;
    void Start()
    {
        dimpleAnimate = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startAnim)
        {
            if (gameObject.transform.position.z > 72.56f)
            {
                transform.Translate(Vector3.right * Time.deltaTime * 1f);
            }
            else
            {

            }
        }
    }

    public void startDimpleAnimation()
    {
        startAnim = true;
    }
}
