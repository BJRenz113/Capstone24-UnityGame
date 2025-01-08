using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneLoaderV3 : MonoBehaviour
{
    public enum EFloorType
    {
        Gluttony,
        Treachery,
        Wrath,
        Greed,
        Heresy,
        Violence
    }

    [SerializeField] private EFloorType _floorType;
    public EFloorType FloorType
    {
        get { return _floorType; }
        set { _floorType = value; }
    }

    private int _roomsToClear = 4;

    private Dictionary<EFloorType, List<int>> _floorToRoomsDict;
    private Dictionary<EFloorType, int> _floorToBossDict;

    private GameObject _playerObject;
    private Player _player;
    private bool _isActive;

    // fix spawns to not correct locations
    // cant load same room twice in a row
    // clean this a bit

    public void Start()
    {
        _isActive = false;

        _floorToRoomsDict = new Dictionary<EFloorType, List<int>>();
        _floorToRoomsDict[EFloorType.Gluttony] = new List<int>() { 2, 2, 2, 2, 2, 2 };
        _floorToRoomsDict[EFloorType.Treachery] = new List<int>() { 8, 9, 10, 11, 12, 13 };
        _floorToRoomsDict[EFloorType.Wrath] = new List<int>() { 14, 15, 16, 17, 18, 19 };
        _floorToRoomsDict[EFloorType.Greed] = new List<int>() { 20, 21, 22, 23, 24, 25 };
        _floorToRoomsDict[EFloorType.Heresy] = new List<int>() { 26, 27, 28, 29, 30, 31 };
        _floorToRoomsDict[EFloorType.Violence] = new List<int>() { 32, 33, 34, 35, 36, 37 };

        _floorToBossDict = new Dictionary<EFloorType, int>();
        _floorToBossDict[EFloorType.Gluttony] = 38;
        _floorToBossDict[EFloorType.Treachery] = 39;
        _floorToBossDict[EFloorType.Wrath] = 40;
        _floorToBossDict[EFloorType.Greed] = 41;
        _floorToBossDict[EFloorType.Heresy] = 42;
        _floorToBossDict[EFloorType.Violence] = 43;
        
    }

    public void FixedUpdate()
    {
        _isActive = GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        _playerObject = other.gameObject;
        if (_playerObject == null) return;
        _player = _playerObject.GetComponent<Player>();
        if (_player == null) return;
        if (_player.AreasClearedDict[_floorType.ToString()] || _isActive == false) return;

        _player.GetComponent<Player>().HealSanity(_player.GetComponent<Player>().SanityRegenOnRoomLeave);
        _player.CurrentRoomsCleared++;
        _player.TotalRoomsCleared++;

        if(_player.CurrentRoomsCleared < _roomsToClear)
        {
            System.Random rng = new System.Random();
            List<int> rooms = _floorToRoomsDict[_floorType];
            int rngValue = rng.Next(rooms.Count);
            int sceneNum = rooms[rngValue];

            ExecuteTeleport(sceneNum);
        }
        else if(_player.CurrentRoomsCleared == _roomsToClear)
        {
            int sceneNum = _floorToBossDict[_floorType];

            ExecuteTeleport(sceneNum);
        }
        else
        {
            _player.GetComponent<Player>().CurrentRoomsCleared = 0;
            _player.GetComponent<Player>().CurrentAreasCleared += 1;
            _player.GetComponent<Player>().AreasClearedDict[_floorType.ToString()] = true;

            ExecuteTeleport(1);

            GameObject[] cameras = GameObject.FindGameObjectsWithTag("MainCamera");
            for (int i = 0; i < cameras.Length; i++)
            {
                if (i != 0) GameObject.Destroy(cameras[i]);
            }
        }
    }

    private void ExecuteTeleport(int sceneNum)
    {
        Camera mainCamera = Camera.main;

        DontDestroyOnLoad(_player);
        DontDestroyOnLoad(mainCamera.gameObject);

        SceneManager.LoadScene(sceneNum);
    }
}
