using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityThresholdSanityResist :KeyedItem
{
    public Player player;
    public float goldValue = 25f;
    public float weight = 2f;

    public float sanityResist = 0.2f;
    public float minSanityDamage = 3f;



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
        player.MinSanityToHurt += (int) minSanityDamage;
        player.ResistSanityPercentage += (int) sanityResist;
        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
        }
        player.getText = "Crazy-b-gone";
        Destroy(gameObject);
    }
}
