using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttackCooldownDown : MonoBehaviour
{
    public Player player;
    public float cooldownDecrease = 0.1f;



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
        player.SpinAttackCooldown -= (int) cooldownDecrease;

        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
        }
        player.getText = "Spin Spammer";
        Destroy(gameObject);
    }
}
