using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class WrathBoss : Enemy
{
    public AudioClip throwSound;
    public AudioClip restoreSound;
    public override void Start()
    {
        base.Start();
        deathStateToTrigger = new WrathBossDeathState();
        _activeRings = new List<GameObject>(_initialRings);


        RestoreAllRings();
        enemyStateManager.TransitionToState(new WrathBossIdleState());
    }

    public void RestoreAllRings()
    {
        for (int i = 0; i < _initialRings.Count; i++)
        {
            GameObject ring = GameObject.Instantiate(_initialRings[i]);
            _activeRings[i] = ring;
            ring.transform.position = gameObject.transform.position + _ringsOffset;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private Vector3 _homePosition;
    public Vector3 HomePosition
    {
        get { return _homePosition; }
        set { _homePosition = value; }
    }

    // initial min/max properties
    [SerializeField] private List<GameObject> _initialRings = new List<GameObject>();
    public List<GameObject> InitialRings
    {
        get { return _initialRings; }
        set { _initialRings = value; }
    }

    private List<GameObject> _activeRings = new List<GameObject>();
    public List<GameObject> ActiveRings
    {
        get { return _activeRings; }
        set { _activeRings = value; }
    }

    [SerializeField] private Vector3 _ringsOffset = Vector3.zero;
    public Vector3 RingsOffset
    {
        get { return _ringsOffset; }
        set { _ringsOffset = value; }
    }

    [SerializeField] private float _minWalkDistance = 0.5f;
    public float MinWalkDistance
    {
        get { return _minWalkDistance; }
        set { _minWalkDistance = value; }
    }

    [SerializeField] private float _maxWalkDistance = 3f;
    public float MaxWalkDistance
    {
        get { return _maxWalkDistance; }
        set { _maxWalkDistance = value; }
    }

    [SerializeField] private float _getToLocationInTime = 2f;
    public float GetToLocationInTime
    {
        get { return _getToLocationInTime; }
        set { _getToLocationInTime = value; }
    }

    [SerializeField] private float _waitAtDestinationTime = 1f;
    public float WaitAtDestinationTime
    {
        get { return _waitAtDestinationTime; }
        set { _waitAtDestinationTime = value; }
    }
}
