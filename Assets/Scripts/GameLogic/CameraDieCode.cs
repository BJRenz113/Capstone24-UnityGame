using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDieCode : MonoBehaviour
{
    public void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("MainCamera").Length > 1)
        {
            GameObject.Destroy(gameObject);
            return;
        }
    }
}
