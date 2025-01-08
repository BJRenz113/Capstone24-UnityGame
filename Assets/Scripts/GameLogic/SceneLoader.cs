/*    using UnityEngine;
    using UnityEngine.SceneManagement;
    using System.IO;

    public class SceneLoader : MonoBehaviour
    {

        public string floorType;
        private void OnTriggerEnter2D(Collider2D other)

        {
            if (other.CompareTag("Player"))
            {
                 Debug.Log("FUCK");
                Debug.Log("Player collided with the box!");

                GameObject player = other.gameObject;

                // Get the last visited room
                string lastRoom = PlayerLastRoomTracker.GetLastRoom();

                string[] floor1Scenes = Directory.GetFiles(Application.dataPath + "/Scenes/" + floorType, "*.unity");

                if (floor1Scenes.Length > 0)
                {
                    string randomSceneName = GetRandomScene(floor1Scenes, lastRoom);
                    Debug.Log("Loading scene: " + randomSceneName);
                    UnityEngine.Debug.Log(GameObject.FindWithTag("Player").GetComponent<Player>().CurrentRoomsCleared);

                    // Remember player position to teleport in the next scene
                    Vector3 playerPosition = player.transform.position;

                    // Get the main camera to bring it to the next scene
                    Camera mainCamera = Camera.main;

                    // Ensure both player and camera persist across scene transitions
                    DontDestroyOnLoad(player);
                    DontDestroyOnLoad(mainCamera.gameObject);

                    if (player.GetComponent<Player>().CurrentRoomsCleared == 1)
                    {
                        Debug.Log("LOAD BOSS AREA");
                        SceneManager.LoadScene("Assets/Scenes/Boss Rooms/GluttonyBoss.unity");

                    }
                    else if (player.GetComponent<Player>().CurrentRoomsCleared == 3)
                    {
                        Debug.Log("BOSS AREA DONE, RESET");
                        player.GetComponent<Player>().CurrentRoomsCleared = 0;
                        player.GetComponent<Player>().CurrentAreasCleared += 1;
                    }
                    else
                    {
                    SceneManager.LoadScene("Assets/Scenes/Boss Rooms/GluttonyBoss.unity");
                    player.GetComponent<Player>().CurrentRoomsCleared += 1;

                       *//* SceneManager.sceneLoaded += (scene, mode) =>
                        {
                            Debug.Log("Scene loaded: " + scene.name);

                            GameObject spawnLocation = GameObject.Find("spawnLocation");

                            if (spawnLocation != null)
                            {
                                Debug.Log("spawnLocation found in the newly loaded scene.");

                                // Teleport player to spawnLocation
                                player.transform.position = spawnLocation.transform.position;
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
                        };*//*
                    }
                else
                    {
                        Debug.LogWarning("No scenes found in the 'Floor1' folder.");
                    }
                }
            }

                
        }

        // Get a random scene excluding the last visited scene
        private string GetRandomScene(string[] scenes, string excludeScene)
        {
            string randomSceneName = "";
            do
            {
                randomSceneName = Path.GetFileNameWithoutExtension(scenes[UnityEngine.Random.Range(0, scenes.Length)]);
            } while (randomSceneName == excludeScene);

            return randomSceneName;
        }
    }
*/