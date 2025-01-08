using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWarp : MonoBehaviour
{
    public static float timeSpeed = .21f;

    void Update()
    {
        Time.timeScale = timeSpeed;
    }
}
