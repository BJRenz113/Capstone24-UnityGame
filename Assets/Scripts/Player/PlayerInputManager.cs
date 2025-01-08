using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInputManager
{
    private Player _player;

    private float _horizontal;
    private float _vertical;
    private bool _directionExists;
    private Vector2 _currentDirection;
    private Vector2 _recentExistsDirection;

    private Dictionary<string, Coroutine> _buttonsPressed;
    private Dictionary<string, bool> _buttonsHeld;

    public PlayerInputManager(Player player)
    {
        _player = player;

        _horizontal = 0f;
        _vertical = 0f;
        _directionExists = false;
        _currentDirection = new Vector2(0f, -1f);
        _recentExistsDirection = new Vector2(0f, -1f);

        _buttonsPressed = new Dictionary<string, Coroutine>();
        _buttonsHeld = new Dictionary<string, bool>();

        _buttonsPressed["Dash"] = null;
        _buttonsPressed["MeleeAttack"] = null;
        _buttonsPressed["SpinAttack"] = null;
        _buttonsPressed["RangeAttack"] = null;

        RefreshInputs();
    }

    public void RefreshInputs()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _directionExists = _horizontal != 0f || _vertical != 0f;
        _currentDirection = new Vector2(_horizontal, _vertical).normalized;
        if (_directionExists) _recentExistsDirection = _currentDirection;

        List<string> buttons = _buttonsPressed.Keys.ToList();

        foreach (string button in buttons)
        {
            if (Input.GetButtonDown(button) && _buttonsPressed[button] == null)
            {
                _buttonsPressed[button] = _player.StartCoroutine(ButtonPressedLifespan(button, 0.25f));
            }
            else if (Input.GetButtonDown(button) && _buttonsPressed[button] != null)
            {
                _player.StopCoroutine(_buttonsPressed[button]);
                _buttonsPressed[button] = _player.StartCoroutine(ButtonPressedLifespan(button, 0.25f));
            }
        }

        _buttonsHeld["SelfStabAttack"] = Input.GetButton("SelfStabAttack");
        _buttonsHeld["HeavyWalk"] = Input.GetButton("HeavyWalk");
    }

    private IEnumerator ButtonPressedLifespan(string button, float lifespan)
    {
        yield return new WaitForSeconds(lifespan);
        _buttonsPressed[button] = null;
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

    // 0 = right, 1 = up, 2 = left, 3 = down
    public int GetRecentExistsDirectionAsIndex()
    {
        int angle = (int)Vector2.SignedAngle(Vector2.right, _recentExistsDirection);

        while (angle < 0)
        {
            angle += 360;
        }

        return ((angle + 45) / 90) % 4;
    }

    public bool GetButtonPressed(string button)
    {
        if (_buttonsPressed.ContainsKey(button))
        {
            return _buttonsPressed[button] != null;
        }
        UnityEngine.Debug.Log("invalid button: " + button + ", returning false");
        return false;
    }

    public void ClearButtonPressed(string button)
    {
        if (_buttonsPressed.ContainsKey(button))
        {
            _player.StopCoroutine(_buttonsPressed[button]);
            _buttonsPressed[button] = null;
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
