using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public Text storyText; // the story 
    public InputField userInput; // the input field object
    public Text inputText; // part of the input field where user enters response
    public Text placeHolderText; // part of the input field for initial placeholder text
    //public Button aButton;
    public delegate void Restart();
    public event Restart onRestart;

    public AudioClip clickSound;
    public AudioSource clickAudioSource;
    
    private string story; // holds the story to display
    private List<string> commands = new List<string>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        commands.Add("commands");
        commands.Add("go");
        commands.Add("get");
        commands.Add("inventory");
        commands.Add("restart");
        commands.Add("save");

        userInput.onEndEdit.AddListener(GetInput);
        story = storyText.text;

        //aButton.onClick.AddListener(DoSomething);

        NavigationManager.instance.onGameOver += EndGame;
    }
    void EndGame()
    {
        UpdateStory("\nEnter 'restart' to play again");
    }

    /*
    public void DoSomething()
    {
        Debug.Log("Button clicked");
    }
    */

    public void UpdateStory(string msg)
    {
        story += "\n" + msg;
        storyText.text = story;
    }

    void GetInput(string msg)
    {
        if (msg != "")
        {
            char[] splitInfo = { ' ' };
            string[] parts = msg.ToLower().Split(splitInfo);

            if (commands.Contains(parts[0]))
            {
                if (parts[0] == "commands")
                {
                    UpdateStory("\nCommands:\ngo - Go to another room in a particular direction (north/south/east/west)\nget - Get an item in a room if it is available\nrestart - Restart the game from the beginning\nsave - Save your progress");
                }
                else if (parts[0] == "go")
                {
                    if (parts[1] != "")
                    {
                        if (NavigationManager.instance.SwitchRooms(parts[1]))
                        {
                            // Do nothing
                        }
                        else
                        {
                            UpdateStory("Exit does not exist.");
                        }
                    }
                }
                else if (parts[0] == "get")
                {
                    if (parts[1] != "")
                    {
                        if (NavigationManager.instance.TakeItem(parts[1]))
                        {
                            GameManager.instance.inventory.Add(parts[1]);
                            UpdateStory("You added a(n) " + parts[1] + " to your inventory.");
                        }
                        else
                        {
                            UpdateStory("" + parts[1] + " does not exist in this room.");
                        }
                    }
                }
                else if (parts[0] == "inventory")
                {
                    if (GameManager.instance.inventory.Count > 0)
                    {
                        UpdateStory("\nItems in your inventory:");
                        for (var i = 0; i < GameManager.instance.inventory.Count; i++)
                        {
                            UpdateStory("" + (i+1) + ". " + GameManager.instance.inventory[i]);
                        }
                    }
                    else
                    {
                        UpdateStory("\nYou have nothing in your inventory");
                    }
                }
                else if (parts[0] == "restart")
                {
                    if (onRestart != null)
                    {
                        onRestart();
                    }
                }
                else if (parts[0] == "save")
                {
                    GameManager.instance.Save();
                    UpdateStory("\nSaved...");
                }
            }
            clickAudioSource.Play();
        }
        userInput.text = "";
        userInput.ActivateInputField();
    }
}
