using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoCoroutineTester : MonoBehaviour
{
    [SerializeField] private float waitTime = 2f;

    private bool buttonPressed = false;
    private float buttonPressedTime = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!buttonPressed)
            {
                UnityEngine.Debug.Log("just pressed");
                buttonPressed = true;
                buttonPressedTime = Time.time;
            }
            else
            {
                UnityEngine.Debug.Log("refresh press");
                buttonPressedTime = Time.time;
            }
        }

        if (buttonPressed && Time.time - buttonPressedTime >= waitTime)
        {
            UnityEngine.Debug.Log("timer expired");
            buttonPressed = false;
        }
    }
}
