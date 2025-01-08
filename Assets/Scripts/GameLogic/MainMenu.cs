using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Corrected namespace

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            GameObject.Destroy(players[i]);
        }

        GameObject[] cameras = GameObject.FindGameObjectsWithTag("MainCamera");
        for (int i = 0; i < cameras.Length; i++)
        {
            GameObject.Destroy(cameras[i]);
        }
    }

    public void Update()
    {
        if (Input.GetButtonDown("Submit")) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
