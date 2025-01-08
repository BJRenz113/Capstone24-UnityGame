using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCooldownDown : MonoBehaviour
{
    public Player player;
    public float meleeCoodwn = 0.1f;



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
        player.MeleeAttackCooldown -= (int) meleeCoodwn;
        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
        }
        player.getText = "Cat Claws";
        Destroy(gameObject);
    }
}
