using UnityEngine;
using UnityEngine.UI;

public class KeyedItem : MonoBehaviour
{
    public enum ItemType
    {
        PowerUp,
        TempPowerUp
    }

    public ItemType type;
    public float pickupRange = 3f; // Range within which the player can pick up the item
    public float time = 5;

    public Sprite sprite;

    // Weight property
    public float weight { get; protected set; }

    public float fadeTime { get; protected set;} 

    protected virtual void Update()
    {
        // Base implementation for general item behavior
    }

    public virtual void ApplyItem() {

    }
}



