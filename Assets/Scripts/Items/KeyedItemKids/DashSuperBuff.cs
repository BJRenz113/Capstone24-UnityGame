using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSuperBuff: KeyedItem
{
    public Player player;
    public float speedBoostAmount = 5f;
    public int maxDashesIncrease= 2;
    public float dashDurationIncrease = 0.5f;

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
    player.MaxDashes += maxDashesIncrease;
    player.DashDuration += dashDurationIncrease;
    player.MoveSpeed += speedBoostAmount;

    AudioSource source = GetComponent<AudioSource>();
    if (source != null)
    {
        source.Play();
    }
        player.getText = "Super Man";
        Destroy(gameObject);
}

}
