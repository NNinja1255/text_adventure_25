using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleThemeController : MonoBehaviour
{
    public Image background;
    public Text displayText;
    public Text toggleThemeText;
    public Text toggleSoundText;
    public Text placeholderTest;
    public Text playerText;

    private bool darkMode;

    // Start is called before the first frame update
    void Start()
    {
        Toggle toggle = GetComponent<Toggle>();

        int pref = PlayerPrefs.GetInt("theme", 1);
        if (pref == 1)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
        darkMode = toggle.isOn;

        SetTheme();

        toggle.onValueChanged.AddListener(ProcessChanged);
    }

    void ProcessChanged(bool value)
    {
        darkMode = value;
        PlayerPrefs.SetInt("theme", darkMode ? 1 : 0);
        SetTheme();
    }

    void SetTheme()
    {
        if (darkMode)
        {
            background.color = Color.black;
            displayText.color = Color.white;
            toggleThemeText.color = Color.white;
            toggleSoundText.color = Color.white;
            placeholderTest.color = Color.white;
            playerText.color = Color.white;
        }
        else
        {
            background.color = Color.white;
            displayText.color = Color.black;
            toggleThemeText.color = Color.black;
            toggleSoundText.color = Color.black;
            placeholderTest.color = Color.black;
            playerText.color = Color.black;
        }
    }
}
