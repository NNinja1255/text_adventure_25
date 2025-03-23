using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSoundController : MonoBehaviour
{
    public AudioSource clickSound;

    private bool playSound;

    // Start is called before the first frame update
    void Start()
    {
        Toggle toggle = GetComponent<Toggle>();

        int pref = PlayerPrefs.GetInt("sound", 1);
        if (pref == 1)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
        playSound = toggle.isOn;

        SetSound();

        toggle.onValueChanged.AddListener(ProcessChanged);
    }

    void ProcessChanged(bool value)
    {
        playSound = value;
        PlayerPrefs.SetInt("sound", playSound ? 1 : 0);
        SetSound();
    }

    void SetSound()
    {
        if (playSound)
        {
            clickSound.volume = 0.25f;
        }
        else
        {
            clickSound.volume = 0;
        }
    }
}
