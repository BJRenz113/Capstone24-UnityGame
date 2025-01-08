using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPlayerInputManagert : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
/*
 * using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager
{
    private float _horizontal;
    private float _vertical;

    private Dictionary<string, bool> _buttonsPressed = new Dictionary<string, bool>();
    private Dictionary<string, bool> _buttonsHeld = new Dictionary<string, bool>();
    private Dictionary<string, bool> _buttonsReleased = new Dictionary<string, bool>();

    public PlayerInputManager()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _buttonsPressed["Dash"] = Input.GetButtonDown("Dash");
        _buttonsPressed["Attack1"] = Input.GetButtonDown("Attack1");

        _buttonsHeld["HeavyWalk"] = Input.GetAxis("HeavyWalk") > 0.5;
    }

    public void RefreshInputs()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _buttonsPressed["Dash"] = Input.GetButtonDown("Dash") || _buttonsPressed["Dash"];
        _buttonsPressed["Attack1"] = Input.GetButtonDown("Attack1") || _buttonsPressed["Attack1"];

        _buttonsHeld["HeavyWalk"] = Input.GetAxis("HeavyWalk") > 0.5;
    }

    public float GetHorizontal()
    {
        return _horizontal;
    }

    public float GetVertical()
    {
        return _vertical;
    }

    public bool DirectionExists()
    {
        return _horizontal != 0 || _vertical != 0;
    }

    public bool GetButtonPressed(string button)
    {
        if (_buttonsPressed.ContainsKey(button))
        {
            return _buttonsPressed[button];
        }
        UnityEngine.Debug.Log("invalid button, returning false");
        return false;
    }

    public void ClearButtonPressed(string button)
    {
        if (_buttonsPressed.ContainsKey(button))
        {
            _buttonsPressed[button] = false;
            return;
        }
        UnityEngine.Debug.Log("invalid button, returning");
    }

    public bool GetButtonHeld(string button)
    {
        if (_buttonsHeld.ContainsKey(button))
        {
            return _buttonsHeld[button];
        }
        UnityEngine.Debug.Log("invalid button, returning false");
        return false;
    }
}
*/
