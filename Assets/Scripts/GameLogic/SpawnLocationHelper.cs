using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocationHelper : MonoBehaviour
{
    public void Awake()
    {
        GameObject.FindWithTag("Player").transform.position = gameObject.transform.position;
    }
}
