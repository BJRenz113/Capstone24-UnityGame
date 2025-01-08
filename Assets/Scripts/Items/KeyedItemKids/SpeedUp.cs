using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp: KeyedItem
{
    public Player player;
    public float speedBoostAmount = 5f;



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
        player.MoveSpeed += speedBoostAmount;
        Debug.Log($"SpeedBoost obtained! Speed increased by {speedBoostAmount} units.");
        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
        }
        player.getText = "Fast Mode";
        Destroy(gameObject);
    }
}
