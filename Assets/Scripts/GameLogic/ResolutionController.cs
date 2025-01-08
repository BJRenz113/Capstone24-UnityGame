using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionController : MonoBehaviour
{
    public int screenWidth = 1920; // Set your desired screen width 
    public int screenHeight = 1080; // Set your desired screen height 
    public bool fullscreen = true; // Set to true for fullscreen, false for windowed 

    void Start()
    {
        // Change the screen resolution 
        Screen.SetResolution(screenWidth, screenHeight, fullscreen);
    }
}