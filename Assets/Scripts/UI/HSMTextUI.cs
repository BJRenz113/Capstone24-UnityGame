using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HSMTextUI : MonoBehaviour
{
    public int option;      // 0 = health, 1 = sanity, 2 = money

    public void Update()
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();

        if (option == 0)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "" + player.CurrentHealth + " / " + player.MaxHealth;
        }
        else if (option == 1)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "" + player.CurrentSanity + " / " + player.MaxSanity;
        }
        else if (option == 2)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "" + player.CurrentMoney + " / " + player.MaxMoney;
        }
        else if (option == 3)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = player.getText;

        }
    }
}
