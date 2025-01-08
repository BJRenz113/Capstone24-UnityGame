using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject squarePrefab; // Prefab representing a 2D square
    public int maxSquares = 20; // Maximum number of squares in the dungeon
    public float squareSize = 1f; // Size of each square
    public int crazyValue = 5;
    private Room[] mapInfo = new Room[20];

    private void Start()
    {
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        GameObject startingSquare = CreateSquare(Vector2.zero);
        GameObject guideSquare = startingSquare;

        for (int i = 0; i < maxSquares; i++)
        {
            Vector2 newPosition = GetRandomDirection(i) * squareSize;
            Debug.Log(Mathf.Round(newPosition.x));
            GameObject newSquare = CreateSquare(guideSquare.transform.position + new Vector3(Mathf.Round(newPosition.x), Mathf.Round(newPosition.y), 0));

            mapInfo[i].isStartingRoom = false; // By default, set the room as not starting room
            mapInfo[i].connectingRooms = new List<int>(); // Initialize the list of connecting rooms

            if (i == 0)
            {
                mapInfo[i].isStartingRoom = true; // Set the first room as the starting room
            }
            else
            {
                mapInfo[i].connectingRooms.Add(i - 1); // Add the previous room as a connecting room
                mapInfo[i - 1].connectingRooms.Add(i); // Add the current room as a connecting room to the previous room
            }

            guideSquare = newSquare;
        }
    }

    GameObject CreateSquare(Vector2 position)
    {
        return Instantiate(squarePrefab, position, Quaternion.identity, transform);
    }

    Vector2 GetRandomDirection(int i)
    {
        Vector2 value = Vector2.zero;

        // Check if i-1 is within the bounds of the array
        if (i - 1 >= 0 && i - 1 < mapInfo.Length)
        {
            int randomNumber = Random.Range(1, 11);

            if (randomNumber < crazyValue)
            {
                Debug.Log("USING LAST DIRECTION");
                value = mapInfo[i - 1].direction;
            }
            else
            {
                // Assign a new random direction along the x-axis or y-axis
                int randomAxis = Random.Range(0, 2); // 0 for x-axis, 1 for y-axis

                if (randomAxis == 0)
                {
                    value = new Vector2(Random.Range(-1f, 1f), 0f).normalized;
                }
                else
                {
                    value = new Vector2(0f, Random.Range(-1f, 1f)).normalized;
                }
            }
        }
        else
        {
            // Handle the case when i-1 is out of bounds
            Debug.LogError("Index out of bounds: " + (i - 1));
        }

        mapInfo[i].direction = value;
        return value;
    }

    struct Room
    {
        public bool isStartingRoom;
        public Vector2 direction;
        public List<int> connectingRooms;
    }
}
