using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedDamageUp : MonoBehaviour
{
    public Player player;
    public float rangedDamageIncrease = 15f;



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
        player.RangedDamage += (int) rangedDamageIncrease;
        
        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
        }
        player.getText = "RPG";
        Destroy(gameObject);
    }
}
