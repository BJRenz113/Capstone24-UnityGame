using UnityEngine;

public class PlayerLastRoomTracker : MonoBehaviour
{
    private static string lastRoomKey = "LastRoomVisited";
    public int numberofRooms;

    void Start()
    {
        numberofRooms = 0;
    }

    // Save the name of the last visited room
    public static void SaveLastRoom(string roomName)
    {
        PlayerPrefs.SetString(lastRoomKey, roomName);
    }

    // Get the name of the last visited room
    public static string GetLastRoom()
    {
        return PlayerPrefs.GetString(lastRoomKey, "");
    }
}
