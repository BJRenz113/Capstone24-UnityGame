using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInputManager1
{
    private Player _player;

    private float _horizontal;
    private float _vertical;
    private bool _directionExists;
    private Vector2 _currentDirection;
    private Vector2 _recentExistsDirection;

    private Dictionary<string, bool> _buttonsPressed = new Dictionary<string, bool>();
    private Dictionary<string, bool> _buttonsHeld = new Dictionary<string, bool>();

    private Dictionary<string, Coroutine> _buttonsPressedCoroutines = new Dictionary<string, Coroutine>();

    private Dictionary<string, List<bool>> _buttonsPressedQueue = new Dictionary<string, List<bool>>();

    public PlayerInputManager1(Player player)
    {
        _player = player;

        _buttonsPressedQueue["Dash"] = new List<bool>();
        _buttonsPressedQueue["MeleeAttack"] = new List<bool>();
        _buttonsPressedQueue["RangeAttack"] = new List<bool>();

        _horizontal = 0f;
        _vertical = 0f;
        _directionExists = false;
        _currentDirection = new Vector2(0f, 0f);
        _recentExistsDirection = new Vector2(0f, 0f);

        _buttonsPressedCoroutines["Dash"] = null;
        _buttonsPressedCoroutines["MeleeAttack"] = null;
        _buttonsPressedCoroutines["RangeAttack"] = null;

        RefreshInputs();
    }

    public void RefreshInputs2()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _directionExists = _horizontal != 0f || _vertical != 0f;
        _currentDirection = new Vector2(_horizontal, _vertical).normalized;
        if (_directionExists) _recentExistsDirection = _currentDirection;

        if (_buttonsPressedQueue["Dash"].Count >= 32)
        {
            for (int i = 0; i < _buttonsPressedQueue["Dash"].Count - 1; i++)
            {
                _buttonsPressedQueue["Dash"][i] = _buttonsPressedQueue["Dash"][i + 1];
            }
            _buttonsPressedQueue["Dash"].RemoveAt(_buttonsPressedQueue["Dash"].Count - 1);
        }
        _buttonsPressedQueue["Dash"].Add(Input.GetButtonDown("Dash"));

        if (_buttonsPressedQueue["MeleeAttack"].Count >= 32)
        {
            for (int i = 0; i < _buttonsPressedQueue["MeleeAttack"].Count - 1; i++)
            {
                _buttonsPressedQueue["MeleeAttack"][i] = _buttonsPressedQueue["MeleeAttack"][i + 1];
            }
            _buttonsPressedQueue["MeleeAttack"].RemoveAt(_buttonsPressedQueue["MeleeAttack"].Count - 1);
        }
        _buttonsPressedQueue["MeleeAttack"].Add(Input.GetButtonDown("MeleeAttack"));

        if (_buttonsPressedQueue["RangeAttack"].Count >= 32)
        {
            for (int i = 0; i < _buttonsPressedQueue["RangeAttack"].Count - 1; i++)
            {
                _buttonsPressedQueue["RangeAttack"][i] = _buttonsPressedQueue["RangeAttack"][i + 1];
            }
            _buttonsPressedQueue["RangeAttack"].RemoveAt(_buttonsPressedQueue["RangeAttack"].Count - 1);
        }
        _buttonsPressedQueue["RangeAttack"].Add(Input.GetButtonDown("RangeAttack"));

        _buttonsPressed["Dash"] = _buttonsPressedQueue["Dash"].Contains(true);
        _buttonsPressed["MeleeAttack"] = _buttonsPressedQueue["MeleeAttack"].Contains(true);
        _buttonsPressed["RangeAttack"] = _buttonsPressedQueue["RangeAttack"].Contains(true);
        _buttonsHeld["SelfStabAttack"] = Input.GetAxis("SelfStabAttack") >= 0.5;
        _buttonsHeld["HeavyWalk"] = Input.GetAxis("HeavyWalk") >= 0.5;
    }

    public void RefreshInputs()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _directionExists = _horizontal != 0f || _vertical != 0f;
        _currentDirection = new Vector2(_horizontal, _vertical).normalized;
        if (_directionExists) _recentExistsDirection = _currentDirection;

        foreach (string button in _buttonsPressedCoroutines.Keys)
        {
            if (Input.GetButtonDown(button))
            {
                _player.StopCoroutine(_buttonsPressedCoroutines[button]);
                _buttonsPressedCoroutines[button] = _player.StartCoroutine(ButtonPressedLifespan(0.5f));
            }
        }
    }

    private IEnumerator ButtonPressedLifespan(float lifespan)
    {
        yield return new WaitForSeconds(lifespan);
    }
    
    public float GetHorizontal()
    {
        return _horizontal;
    }

    public float GetVertical()
    {
        return _vertical;
    }

    public bool GetDirectionExists()
    {
        return _directionExists;
    }

    public Vector2 GetCurrentDirection()
    {
        return _currentDirection;
    }

    public Vector2 GetRecentExistsDirection()
    {
        return _recentExistsDirection;
    }

    public bool GetButtonPressed(string button)
    {
        if (_buttonsPressed.ContainsKey(button))
        {
            return _buttonsPressedCoroutines[button] != null;
        }
        UnityEngine.Debug.Log("invalid button: " + button + ", returning false");
        return false;
    }

    public void ClearButtonPressed(string button)
    {
        if (_buttonsPressed.ContainsKey(button))
        {
            _buttonsPressed[button] = false;
            _buttonsPressedQueue[button].Clear();
            return;
        }
        UnityEngine.Debug.Log("invalid button: " + button + ", returning");
    }

    public bool GetButtonHeld(string button)
    {
        if (_buttonsHeld.ContainsKey(button))
        {
            return _buttonsHeld[button];
        }
        UnityEngine.Debug.Log("invalid button: " + button + ", returning false");
        return false;
    }
}
