using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class SoundSystem : MonoBehaviour
{
    public AudioClip[] songs; // Array of audio clips (MP3 files)
    public float[] volumes;

    private string _currentFolder = "Null";
    private string currentScenePath;
    private AudioSource audioSource; // Declare an AudioSource variable
    private int _currentSceneIndex;
    private int _previousSceneIndex;
    private List<int> _songIndices;
    private List<float> _volumes;
    // private int _currentSongDebug = 1;

    public void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        _previousSceneIndex = SceneManager.GetActiveScene().buildIndex;

        _songIndices = new List<int>() { 0,                 // main game
                                         1,                 // main hub
                                         3,3,3,3,3,3,       // gluttony
                                         9,9,9,9,9,9,       // treachery
                                         13,13,13,13,13,13, // wrath
                                         5,5,5,5,5,5,       // greed
                                         7,7,7,7,7,7,       // heresy
                                         11,11,11,11,11,11, // violence
                                         4,10,14,6,8,12 };
                                         
    }

    public void Update()
    {
        // debug

        /*bool changed = false;
        int og = _currentSongDebug;

        if (Input.GetButtonDown("Cancel")) _currentSongDebug--;
        if (Input.GetButtonDown("Submit")) _currentSongDebug++;

        if (og != _currentSongDebug)
        {
            audioSource.Stop();
            audioSource.clip = songs[_currentSongDebug];
            audioSource.volume = volumes[_currentSongDebug];
            UnityEngine.Debug.Log("Playing song: " + audioSource.clip.name);
            audioSource.Play();
        }*/

        // end debug

        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (_previousSceneIndex != _currentSceneIndex && _currentSceneIndex != 0)
        {
            if (songs[_songIndices[_previousSceneIndex]] != songs[_songIndices[_currentSceneIndex]])
            {
                audioSource.Stop();
                audioSource.clip = songs[_songIndices[_currentSceneIndex]];
                audioSource.volume = volumes[_songIndices[_currentSceneIndex]];
                UnityEngine.Debug.Log("Playing song: " + audioSource.clip.name + " with audio level " + audioSource.volume);
                audioSource.Play();
            }
        }
        _previousSceneIndex = _currentSceneIndex;
    }
}
