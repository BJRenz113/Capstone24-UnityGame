using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackTimeDown : MonoBehaviour
{
    public Player player;
    public float meleeTime = 0.3f;



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
        player.MeleeAttackTime -= (int) meleeTime;

        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
        }
        player.getText = "Speedy McSpeedser";
        Destroy(gameObject);
    }
}
