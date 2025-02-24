using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text/Room")]
public class Room : MonoBehaviour
{
    public string roomName;
    [TextArea]
    public string description;
    public Exit[] exits;

    public bool hasKey;
    public bool hasOrb;
}
