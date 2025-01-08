using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPDisplayBar : MonoBehaviour
{
    public void Update()
    {
        Enemy enemy = gameObject.transform.parent.transform.parent.gameObject.GetComponent<Enemy>();
        gameObject.GetComponent<Slider>().value = 1.0f * enemy.CurrentHealth / enemy.MaxHealth;
    }
}
