using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPDisplayFixScale : MonoBehaviour
{
    public Vector3 globalScale;

    public void Update()
    {
        gameObject.transform.localScale = Vector3.one;
        gameObject.transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
    }
}
