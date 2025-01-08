using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttackDamageUp : MonoBehaviour
{
    public Player player;
    public float spinDamage = 10f;



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
        player.SpinAttackDamage += (int)10f;

        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
        }
        player.getText = "Spin to Win";
        Destroy(gameObject);
    }
}
