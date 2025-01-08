using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//allows player to obtain gold at the cost of sanity
public class MoneyUpHealthDown: KeyedItem
{
    public Player player;
    public int moneyAmount = 50;
    public int sanityDrainAmount = 50;


    void OnTriggerStay2D(Collider2D other) {
        if (Input.GetButton("Submit")) {
            if (other.gameObject.GetComponent<Player>() != null) {
                player = other.gameObject.GetComponent<Player>();
                ApplyItem();
            }
        }
    }

    
    protected void ApplyItem()
    {
        player.CurrentSanity -= sanityDrainAmount;
        player.CurrentMoney += moneyAmount;

        AudioSource source = GetComponent<AudioSource> ();
            if (source != null) {
                source.Play();
                 }
        player.getText = "Blood Bank";
        Destroy(gameObject);
    }
}
