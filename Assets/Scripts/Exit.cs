using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text/Exit")]
public class Exit : MonoBehaviour
{
    public enum Direction { north, south, east, west }

    public Direction direction;
    [TextArea]
    public string description;
    public Room room;

    public bool isLocked;
    public bool isHidden;

    public void setRoom(Room r)
    {
        room = r;
    }
}
