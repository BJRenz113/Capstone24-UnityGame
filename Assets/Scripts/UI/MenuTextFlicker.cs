using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuTextFlicker : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        while(true)
        {
            yield return new WaitForSeconds(_activeTime);
            gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
            yield return new WaitForSeconds(_inactiveTime);
            gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }

    [SerializeField] private float _activeTime = 1f;
    public float ActiveTime
    {
        get { return _activeTime; }
        set { _activeTime = value; }
    }

    [SerializeField] private float _inactiveTime = 0.5f;
    public float InactiveTime
    {
        get { return _inactiveTime; }
        set { _inactiveTime = value; }
    }
}
