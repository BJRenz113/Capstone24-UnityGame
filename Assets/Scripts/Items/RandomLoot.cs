using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLoot : MonoBehaviour
{
    public GameObject[] basicItemPrefabs;
    public GameObject[] bossItemPrefabs;

    public GameObject GenerateRandomBasicItem(Vector3 position)
    {
        if (basicItemPrefabs.Length == 0)
            return null;

        int randomIndex = Random.Range(0, basicItemPrefabs.Length);
        Debug.Log("Standard Item generated:RandomLOOT Gen " );
        return Instantiate(basicItemPrefabs[randomIndex], position, Quaternion.identity);
    }

    public GameObject GenerateRandomBossItem(Vector3 position)
    {
        if (bossItemPrefabs.Length == 0)
            return null;

        int randomIndex = Random.Range(0, bossItemPrefabs.Length);
        Debug.Log("Boss Item generated:RandomLOOT Gen " );
        return Instantiate(bossItemPrefabs[randomIndex], position, Quaternion.identity);

    }
}


































// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;


// public class RandomLoot : MonoBehaviour
// {
//     // Define basic and boss items arrays
//     public List<KeyedItem> basicItems = new List<KeyedItem>(); //How to get items into lists????? //add weights to items 
//     public List<KeyedItem> bossItems = new List<KeyedItem>(); //other video said that weights need to be sorted highest to lowest... needs to be tested later

//     // Define a list

//     public DashUpDurationDown dashUpDurationDownItem;
//     public AttackSpeedUpDamageDown attackSpeedUpDamageDownItem;
//     public DamageGambleMoveSpeedDown damageGambleMoveSpeedDownItem;
//     public DashCoolDownDown dashCoolDownDownItem;
//     public DashSpeedUp dashSpeedUpItem;
//     public DashSuperBuff dashSuperBuffItem;
//     public DashUpHealthDown dashUpHealthDownItem;
//     public FullHealMaxHealthDown fullHealMaxHealthDownItem;
//     public HealthDownSanityUp healthDownSanityUpItem;
//     public HealthUp healthUpItem; //
//     public HealthUpDamageReduceMovementReduce healthUpDamageReduceMovementReduceItem;
//     public HealthUpSanityDown healthUpSanityDownItem;
//     public HealthUpSanityUpMoveDown healthUpSanityUpMoveDownItem;
    
//     public MaxSanityUpMoveDown maxSanityUpMoveDownItem;
//     public MeleeUpMovementDown meleeUpMovementDownItem;
//     public MoneyUpHealthDown MoneyUpHealthDownItem;
//     public SanityImmuneHealthDown sanityImmuneHealthDownItem;
//     public SanityThresholdSanityResist SanityThresholdSanityResistItem;
//     public SpeedUp speedUpItem;


//     public void Start() {
//         basicItems.Add(dashUpDurationDownItem);
//         basicItems.Add(SanityThresholdSanityResistItem);
//         basicItems.Add(MoneyUpHealthDownItem);
//         basicItems.Add(meleeUpMovementDownItem);
//         basicItems.Add(maxSanityUpMoveDownItem);
//         basicItems.Add(healthUpSanityDownItem);
//         basicItems.Add(healthUpDamageReduceMovementReduceItem);
//         basicItems.Add(healthUpItem);
//         basicItems.Add(dashSpeedUpItem);
//         basicItems.Add(damageGambleMoveSpeedDownItem);
//         basicItems.Add(dashUpDurationDownItem);



//         bossItems.Add(speedUpItem);
//         bossItems.Add(sanityImmuneHealthDownItem);
//         bossItems.Add(healthUpSanityUpMoveDownItem);
//         bossItems.Add(healthDownSanityUpItem);
//         bossItems.Add(fullHealMaxHealthDownItem);
//         bossItems.Add(dashUpHealthDownItem);
//         bossItems.Add(dashSuperBuffItem);
//         bossItems.Add(dashCoolDownDownItem);
//         bossItems.Add(attackSpeedUpDamageDownItem);



//     }

//     public List<KeyedItem> GenerateShop()
//     {
//         List<KeyedItem> shopItems = new List<KeyedItem>();

//         for (int i = 0; i < 2; i++)
//         {
//             KeyedItem newItem = GetRandomItem(basicItems);
//             if (newItem != null)
//             {
//                 shopItems.Add(newItem);

//             }
//         }
//         return shopItems;
//     }

//     // Method to generate items for the boss reward
//     public List<KeyedItem> GenerateBoss()
//     {
//         List<KeyedItem> bossRewardItems = new List<KeyedItem>();

//         for (int i = 0; i < 2; i++) //trying one
//         {
//             KeyedItem newItem = GetRandomItem(bossItems);
//             if (newItem != null)
//             {
//                 bossRewardItems.Add(newItem);
//             }
//         }
//         return bossRewardItems;
//     }


//     private KeyedItem GetRandomItem(List<KeyedItem> itemList)
//     {
//         if (itemList.Count == 0)
//             return null;

//         // Calculate total weight
//         float totalWeight = 0f;
//         foreach (KeyedItem item in itemList)
//         {
//             totalWeight += item.weight;
//         }

//         // Generate a random number between 0 and totalWeight
//         float randomNumber = Random.Range(0f, totalWeight);

//         // Iterate through the items and find the one that corresponds to the random number
//         foreach (KeyedItem item in itemList)
//         {
//             if (randomNumber < item.weight)
//             {
//                 itemList.Remove(item);
//                 return item;
//             }
//             else
//             {
//                 randomNumber -= item.weight;
//             }
//         }

//         // This should never happen
//         return null;
//     }
// }