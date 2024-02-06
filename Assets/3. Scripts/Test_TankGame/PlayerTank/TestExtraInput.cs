using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestExtraInput : MonoBehaviour
{
    public void OnFadeAction(InputValue value)
    {
        FadeManager.Instance.Fade(FadeType.INOUT);
    }
}
