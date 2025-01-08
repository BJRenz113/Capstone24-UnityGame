using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHPDisplayText : MonoBehaviour
{
    public void Update()
    {
        Enemy enemy = gameObject.transform.parent.transform.parent.gameObject.GetComponent<Enemy>();
        gameObject.GetComponent<TextMeshProUGUI>().text = "" + enemy.CurrentHealth + " / " + enemy.MaxHealth;
    }
}
