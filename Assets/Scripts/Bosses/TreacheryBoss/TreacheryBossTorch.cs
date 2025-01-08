using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TreacheryBossTorch : MonoBehaviour
{
    public void FixedUpdate()
    {
        float scale = _currentLightPercentage / 100f;
        gameObject.transform.GetChild(0).transform.localScale = new Vector3(scale, scale, scale);

        if (!_isBurning && _currentLightPercentage > 0)
        {
            StartCoroutine(BurnTorch());
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player == null) return;

        if (player.GetPlayerStateManager().GetCurrentState().GetType() == typeof(PlayerDashState))
        {
            LightTorch();
        }
    }

    private void LightTorch()
    {
        _currentLightPercentage += _lightPercentageIncrease;
        if (_currentLightPercentage > 100)
        {
            _currentLightPercentage = 100;
        }
    }

    private IEnumerator BurnTorch()
    {
        _isBurning = true;

        while (_currentLightPercentage > 0)
        {
            yield return new WaitForSeconds(_burnRate);
            _currentLightPercentage--;
        }

        _currentLightPercentage = 0;
        _isBurning = false;
    }

    private bool _isBurning = false;

    private int _currentLightPercentage = 0;
    public int CurrentLightPercentage
    {
        get { return _currentLightPercentage; }
        set { _currentLightPercentage = value; }
    }

    [SerializeField] private int _lightPercentageIncrease = 50;
    public int LightPercentageIncrease
    {
        get { return _lightPercentageIncrease; }
        set { _lightPercentageIncrease = value; }
    }

    [SerializeField] private float _burnRate = 0.25f;
    public float BurnRate
    {
        get { return _burnRate; }
        set { _burnRate = value; }
    }
}
