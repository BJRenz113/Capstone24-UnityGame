using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class SkullRing : MonoBehaviour
{
    public void Start()
    {
        RestoreRing();
    }

    public void FixedUpdate()
    {
        for(int i = 0; i < _skulls.Count; i++)
        {
            GameObject skull = _skulls[i];

            if(skull == null)
            {
                _skulls.RemoveAt(i);
            }
        }

        gameObject.transform.Rotate(0, 0, _rotateSpeed * Time.fixedDeltaTime);
    }

    public void RestoreRing()
    {
        DestroyRing();

        float ringX = gameObject.transform.position.x;
        float ringY = gameObject.transform.position.y;
        float ringZ = gameObject.transform.position.z;

        for (int i = 0; i < _maxSkulls; i++)
        {
            float angle = i * _skullSeparation + _initialAngleOffset;

            GameObject skull = GameObject.Instantiate(_skull);
            skull.transform.SetParent(gameObject.transform);

            float xOffset = Mathf.Cos(angle * Mathf.PI / 180) * _radius / skull.transform.lossyScale.x;
            float yOffset = Mathf.Sin(angle * Mathf.PI / 180) * _radius / skull.transform.lossyScale.y;
            
            skull.transform.localPosition = new Vector3(xOffset, yOffset, 0);

            _skulls.Add(skull);


        }
    }

    public void DestroyRing()
    {
        for (int i = 0; i < _skulls.Count; i++)
        {
            GameObject skull = _skulls[i];

            if (skull == null)
            {
                _skulls.RemoveAt(i);
            }
            else
            {
                GameObject.Destroy(_skulls[i]);
            }
        }
        _skulls.Clear();
    }

    // initial min/max properties
    [SerializeField] private GameObject _skull;
    public GameObject Skull
    {
        get { return _skull; }
        set { _skull = value; }
    }

    [SerializeField] private List<GameObject> _skulls = new List<GameObject>();
    public List<GameObject> Skulls
    {
        get { return _skulls; }
        set { _skulls = value; }
    }

    [SerializeField] private float _rotateSpeed = 0f;
    public float RotateSpeed
    {
        get { return _rotateSpeed; }
        set { _rotateSpeed = value; }
    }

    [SerializeField] private float _radius = 0f;
    public float Radius
    {
        get { return _radius; }
        set { _radius = value; }
    }

    [SerializeField] private float _skullSeparation = 0f;
    public float SkullSeparation
    {
        get { return _skullSeparation; }
        set { _skullSeparation = value; }
    }

    [SerializeField] private float _initialAngleOffset = 0f;
    public float InitialAngleOffset
    {
        get { return _initialAngleOffset; }
        set { _initialAngleOffset = value; }
    }

    [SerializeField] private int _maxSkulls = 0;
    public int MaxSkulls
    {
        get { return _maxSkulls; }
        set { _maxSkulls = value; }
    }
}
