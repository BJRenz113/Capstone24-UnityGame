using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneLoaderV2 : MonoBehaviour
{
    public string floorType;
    private int roomIndex;
    private string bossFloor;
    private int[] floorArray;
    private int _roomsToClearBeforeBoss = 4;

    public void OnTriggerStay2D(Collider2D other)
    {
        GameObject player = other.gameObject;

        if (player == null) return;
        if (player.GetComponent<Player>() == null) return;

        if (player.GetComponent<Player>().AreasClearedDict[floorType]) return;

        player.GetComponent<Player>().CurrentRoomsCleared += 1;

        if (other.CompareTag("Player"))
        {
            if (floorType == "Gluttony")
            {
                // floorArray = new int[] { 2, 2, 2, 2, 2, 2 };
                floorArray = new int[] { 2, 3, 4, 5, 6, 7 };
            }
            else if (floorType == "Treachery")
            {
                // floorArray = new int[] { 11, 11, 11, 11, 11, 11 };
                floorArray = new int[] { 8, 9, 10, 11, 12, 13 };
            }
            else if (floorType == "Wrath")
            {
                floorArray = new int[] { 14, 15, 16, 17, 18, 19 };
            }
            else if (floorType == "Greed")
            {
                floorArray = new int[] { 20, 21, 22, 23, 24, 25 };
            }
            
            else if (floorType == "Heresy")
            {
                floorArray = new int[] { 26, 27, 28, 29, 30, 31 };
                //floorArray = new int[] { 27, 27, 27, 27, 27, 27 };
            }
            else if (floorType == "Violence")
            {
                floorArray = new int[] { 32, 33, 34, 35, 36, 37 };
            }

            player.GetComponent<Player>().GetPlayerStateManager().TransitionToState(new PlayerMovingState());

            // debug

            if(player.GetComponent<Player>().IsDebugMode)
            {
                Camera mainCamera2 = Camera.main;

                DontDestroyOnLoad(player);
                DontDestroyOnLoad(mainCamera2.gameObject);

                SceneManager.LoadScene(player.GetComponent<Player>().CurrentRoomsCleared);

                return;
            }

            // end debug

            GameObject.FindWithTag("Player").GetComponent<Player>().getText = floorType;

            if (player.GetComponent<Player>().CurrentRoomsCleared < _roomsToClearBeforeBoss + 1)
            {
                string lastRoom = PlayerLastRoomTracker.GetLastRoom();

                System.Random rand = new System.Random();

                int currentRoomIndex = SceneManager.GetActiveScene().buildIndex;
                int RoomIndex = currentRoomIndex;

                while(currentRoomIndex == RoomIndex)
                {
                    int randomIndex = rand.Next(floorArray.Length);
                    RoomIndex = floorArray[randomIndex];
                }

                // Remember player position to teleport in the next scene
                Vector3 playerPosition = player.transform.position;

                // Get the main camera to bring it to the next scene
                Camera mainCamera = Camera.main;

                // Ensure both player and camera persist across scene transitions
                DontDestroyOnLoad(player);
                DontDestroyOnLoad(mainCamera.gameObject);

                SceneManager.LoadScene(RoomIndex);

                SceneManager.sceneLoaded += (scene, mode) =>
                {
                    Debug.Log("Scene loaded: " + scene.name);

                    GameObject spawnLocation = GameObject.Find("spawnLocation");

                    if (spawnLocation != null)
                    {
                        Debug.Log("spawnLocation found in the newly loaded scene.");

                        // Teleport player to spawnLocation
                        // player.transform.position = spawnLocation.transform.position;
                    }
                    else
                    {
                        Debug.LogWarning("spawnLocation not found in the newly loaded scene.");
                    }

                    // Move the camera to the player's position
                    mainCamera.transform.position = playerPosition;
                    mainCamera.gameObject.SetActive(true);

                    // Save the name of the current scene as the last visited room
                    PlayerLastRoomTracker.SaveLastRoom(scene.name);
                };



            }
            else if (player.GetComponent<Player>().CurrentRoomsCleared == _roomsToClearBeforeBoss + 1)
            {
                Debug.Log(bossFloor);
                // Remember player position to teleport in the next scene
                Vector3 playerPosition = player.transform.position;

                // Get the main camera to bring it to the next scene
                Camera mainCamera = Camera.main;

                // Ensure both player and camera persist across scene transitions
                DontDestroyOnLoad(player);
                DontDestroyOnLoad(mainCamera.gameObject);
                Debug.Log(bossFloor);

                if (floorType == "Gluttony")
                {
                   SceneManager.LoadScene(38);
                }
                else if (floorType == "Wrath")
                {
                    SceneManager.LoadScene(40);

                }
                else if (floorType == "Heresy")
                {
                    SceneManager.LoadScene(42);

                }
                else if (floorType == "Treachery")
                {
                    SceneManager.LoadScene(39);

                }
                else if (floorType == "Violence")
                {
                    SceneManager.LoadScene(43);
                }
                else if (floorType == "Greed")
                {
                    SceneManager.LoadScene(41);

                }

                SceneManager.sceneLoaded += (scene, mode) =>
                {
                    Debug.Log("Scene loaded: " + scene.name);

                    GameObject spawnLocation = GameObject.Find("spawnLocation");

                    if (spawnLocation != null)
                    {
                        Debug.Log("spawnLocation found in the newly loaded scene.");

                        // Teleport player to spawnLocation
                        // player.transform.position = spawnLocation.transform.position;
                    }
                    else
                    {
                        Debug.LogWarning("spawnLocation not found in the newly loaded scene.");
                    }

                    // Move the camera to the player's position
                    mainCamera.transform.position = playerPosition;
                    mainCamera.gameObject.SetActive(true);

                    // Save the name of the current scene as the last visited room
                    PlayerLastRoomTracker.SaveLastRoom(scene.name);
                };

            }
            else
            {
                Debug.Log("BOSS AREA DONE, RESET");
                player.GetComponent<Player>().CurrentRoomsCleared = 0;
                player.GetComponent<Player>().CurrentAreasCleared += 1;
                player.GetComponent<Player>().AreasClearedDict[floorType] = true;

                Player p = player.GetComponent<Player>();

                switch (floorType)
                {
                    case "Gluttony":
                        UnityEngine.Debug.Log("Buffed player in Gluttony");
                        p.BuffMaxHealth(25);
                        break;
                    case "Treachery":
                        UnityEngine.Debug.Log("Buffed player in Treachery");
                        p.BuffMaxSanity(25);
                        break;
                    case "Wrath":
                        UnityEngine.Debug.Log("Buffed player in Wrath");
                        p.MaxDashes++;
                        p.MaxAttacks++;
                        p.SpinAttackHits++;
                        break;
                    case "Greed":
                        UnityEngine.Debug.Log("Buffed player in Greed");
                        p.MaxMoney += 50;
                        break;
                    case "Heresy":
                        UnityEngine.Debug.Log("Buffed player in Heresy");
                        p.MoveSpeed += 20f;
                        break;
                    case "Violence":
                        UnityEngine.Debug.Log("Buffed player in Violence");
                        p.DashDamage += 3;
                        p.MeleeDamage += 2;
                        p.RangedDamage += 3;
                        p.SpinAttackDamage += 2;
                        p.SelfStabDamage += 5;
                        break;
                    default:
                        break;
                }

                Vector3 playerPosition = player.transform.position;

                // Get the main camera to bring it to the next scene
                Camera mainCamera = Camera.main;

                // Ensure both player and camera persist across scene transitions
                DontDestroyOnLoad(player);

                SceneManager.LoadScene(1);
                GameObject.FindWithTag("Player").GetComponent<Player>().getText = "Limbo (Room Cleared)";
                SceneManager.sceneLoaded += (scene, mode) =>
                {
                    Debug.Log("Scene loaded: " + scene.name);

                    GameObject spawnLocation = GameObject.Find("spawnLocation");

                    if (spawnLocation != null)
                    {
                        Debug.Log("spawnLocation found in the newly loaded scene.");

                        // Teleport player to spawnLocation
                        // player.transform.position = spawnLocation.transform.position;
                    }
                    else
                    {
                        Debug.LogWarning("spawnLocation not found in the newly loaded scene.");
                    }

                    // Move the camera to the player's position
                    mainCamera.transform.position = playerPosition;
                    mainCamera.gameObject.SetActive(true);

                    // Save the name of the current scene as the last visited room
                    PlayerLastRoomTracker.SaveLastRoom(scene.name);
                };
            }

            player.GetComponent<Player>().HealSanity(player.GetComponent<Player>().SanityRegenOnRoomLeave);

        }
    }

    private List<string> FilterFloorScenes(string[] scenePaths)
    {
        List<string> filteredScenes = new List<string>();
        foreach (string scenePath in scenePaths)
        {
            if (Path.GetFileName(scenePath).Contains("Floor"))
            {
                filteredScenes.Add(scenePath);
            }
        }
        return filteredScenes;
    }

    public string getFloorName()
    {
        return floorType;
    }

}
