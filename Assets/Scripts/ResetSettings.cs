using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetSettings : MonoBehaviour
{
    public Toggle themeToggle;
    public Toggle soundToggle;

    public void OnClick()
    {
        themeToggle.isOn = true;
        soundToggle.isOn = true;
    }
}
