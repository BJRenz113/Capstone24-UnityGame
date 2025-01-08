using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedThrowSpeedUp : MonoBehaviour
{
    public Player player;
    public float RangedThrowSpeedIncrease = 0.2f;



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
        player.RangedThrowSpeed += (int) RangedThrowSpeedIncrease;

        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
        }
        player.getText = "Bullet Hell";
        Destroy(gameObject);
    }
}
