using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRState : MonoBehaviour
{
    public bool isLeftGripActive = false;
    public bool isRightGripActive = false;

    public void ActiveLeftGrip(bool state)
    {
        isLeftGripActive = state;
    }

    public void ActiveRightGrip(bool state)
    {
        isRightGripActive = state;
    }
}
