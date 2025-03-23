using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<string> inventory = new List<string>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        InputManager.instance.onRestart += ResetGame;
        Debug.Log(Application.persistentDataPath);
        Load();
    }

    void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/player.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream aFile = File.Open(Application.persistentDataPath + "/player.save", FileMode.Open);
            SaveState playerData = (SaveState)bf.Deserialize(aFile);
            aFile.Close();

            inventory = playerData.inventory;

            Room room = NavigationManager.instance.GetRoomFromName(playerData.currentRoom);
            if (room != null)
            {
                NavigationManager.instance.SwitchRooms(room);
            }
        }
        else
        {
            NavigationManager.instance.ResetGame();
        }
    }

    void ResetGame()
    {
        inventory.Clear();
    }

    public void Save()
    {
        SaveState playerState = new SaveState();
        playerState.currentRoom = NavigationManager.instance.currentRoom.name;
        playerState.inventory = inventory;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream aFile = File.Create(Application.persistentDataPath + "/player.save");
        Debug.Log(Application.persistentDataPath);
        bf.Serialize(aFile, playerState);
        aFile.Close();
    }
}
