using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HSMBarUI : MonoBehaviour
{
    public int option;      // 0 = health, 1 = sanity, 2 = money

    public void Update()
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();

        if(option == 0)
        {
            gameObject.GetComponent<Slider>().value = 1.0f * player.CurrentHealth / player.MaxHealth;
        }
        else if(option == 1)
        {
            gameObject.GetComponent<Slider>().value = 1.0f * player.CurrentSanity / player.MaxSanity;
        }
        else if(option == 2)
        {
            gameObject.GetComponent<Slider>().value = 1.0f * player.CurrentMoney / player.MaxMoney;
        }
    }
}
