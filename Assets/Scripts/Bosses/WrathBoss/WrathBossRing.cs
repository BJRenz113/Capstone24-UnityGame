using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class WrathBossRing : MonoBehaviour
{
    public void Start()
    {
        RestoreRing();
    }

    public void FixedUpdate()
    {
        for (int i = 0; i < _orbitals.Count; i++)
        {
            GameObject orbital = _orbitals[i];

            if (orbital == null)
            {
                _orbitals.RemoveAt(i);
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

        for (int i = 0; i < _maxOrbitals; i++)
        {
            float angle = i * _orbitalSeparation + _initialAngleOffset;

            GameObject orbital = GameObject.Instantiate(_orbital);
            orbital.transform.SetParent(gameObject.transform);

            float xOffset = Mathf.Cos(angle * Mathf.PI / 180) * _radius / orbital.transform.lossyScale.x;
            float yOffset = Mathf.Sin(angle * Mathf.PI / 180) * _radius / orbital.transform.lossyScale.y;

            orbital.transform.localPosition = new Vector3(xOffset, yOffset, 0);

            _orbitals.Add(orbital);
        }
    }

    public void DestroyRing()
    {
        for (int i = 0; i < _orbitals.Count; i++)
        {
            GameObject orbital = _orbitals[i];

            if (orbital == null)
            {
                _orbitals.RemoveAt(i);
            }
            else
            {
                GameObject.Destroy(_orbitals[i]);
            }
        }
        _orbitals.Clear();
    }

    [SerializeField] private GameObject _orbital;
    public GameObject Orbital
    {
        get { return _orbital; }
        set { _orbital = value; }
    }

    private List<GameObject> _orbitals = new List<GameObject>();
    public List<GameObject> Orbitals
    {
        get { return _orbitals; }
        set { _orbitals = value; }
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

    [SerializeField] private float _orbitalSeparation = 0f;
    public float OrbitalSeparation
    {
        get { return _orbitalSeparation; }
        set { _orbitalSeparation = value; }
    }

    [SerializeField] private float _initialAngleOffset = 0f;
    public float InitialAngleOffset
    {
        get { return _initialAngleOffset; }
        set { _initialAngleOffset = value; }
    }

    [SerializeField] private int _maxOrbitals = 0;
    public int MaxOrbitals
    {
        get { return _maxOrbitals; }
        set { _maxOrbitals = value; }
    }

    [SerializeField] private float _moveSpeed = 0f;
    public float MoveSpeed
    {
        get { return _initialAngleOffset; }
        set { _moveSpeed = value; }
    }
}
