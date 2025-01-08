using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination;

    public Vector3 GetTeleportPosition()
    {
        return destination.position;
    }
}
