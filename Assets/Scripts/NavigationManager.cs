using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    public static NavigationManager instance;
    public Room startingRoom;
    public Room currentRoom;
    public List<Room> rooms;

    public Exit toKeyNorth;
    public Exit toPortalNorth;
    public Exit toTeledNorth;

    public Room orbTeled;
    public Room keyTeled;

    private Dictionary<string, Room> exitRooms = new Dictionary<string, Room>();

    public delegate void GameOver();
    public event GameOver onGameOver;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else

        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        InputManager.instance.onRestart += ResetGame;

        //ResetGame();
    }

    public void ResetGame()
    {
        toKeyNorth.isHidden = true;
        toPortalNorth.isHidden = false;
        int randNum = Random.Range(0,2);
        if (randNum == 0)
        {
            toTeledNorth.setRoom(orbTeled);
        }
        else
        {
            toTeledNorth.setRoom(keyTeled);
        }
        currentRoom = startingRoom;
        Unpack();
    }

    void Unpack()
    {
        string description = currentRoom.description;
        exitRooms.Clear();
        foreach (Exit e in currentRoom.exits)
        {
            if (!e.isHidden)
            {
                description += " " + e.description;
                exitRooms.Add(e.direction.ToString(), e.room);
            }
        }

        InputManager.instance.UpdateStory(description);

        if (exitRooms.Count == 0) 
        {
            if (onGameOver != null)
            {
                onGameOver();
            }
        }
    }

    public bool SwitchRooms(string direction)
    {
        if (exitRooms.ContainsKey(direction))
        {
            if (!getExit(direction).isLocked || GameManager.instance.inventory.Contains("key"))
            {
                currentRoom = exitRooms[direction];
                if (exitRooms[direction] == orbTeled || exitRooms[direction] == keyTeled)
                {
                    toPortalNorth.isHidden = true;
                }
                InputManager.instance.UpdateStory("You go " + direction);
                Unpack();
                return true;
            }
            else
            {
                InputManager.instance.UpdateStory("This exit is locked");
                return true;
            }
        }

        return false;
    }

    public void SwitchRooms(Room room)
    {
        currentRoom = room;
        Unpack();
    }

    Exit getExit(string direction)
    {
        foreach(Exit e in currentRoom.exits)
        {
            if (e.direction.ToString() == direction)
            {
                return e;
            }
        }
        return null;
    }

    public bool TakeItem(string item)
    {
        if (item == "key" && currentRoom.hasKey)
        {
            return true;
        }
        else if (item == "orb" && currentRoom.hasOrb)
        {
            toKeyNorth.isHidden = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public Room GetRoomFromName(string name)
    {
        foreach (Room aRoom in rooms)
        {
            if (aRoom.name == name)
            {
                return aRoom;
            }
        }

        return null;
    }
}
