// using UnityEngine;
// using System.Collections.Generic;

// [System.Serializable]
// public class Wave
// {
//     public List<GameObject> enemiesToSpawn = new List<GameObject>();
// }

// public class WaveManager : MonoBehaviour
// {
//     public List<Wave> waves = new List<Wave>(); // List of waves
//     private int currentWaveIndex = 0;
//     private bool isWaveActive = false;

//     void Start()
//     {
//         // Start the first wave
//         StartNextWave();
//     }

//     void Update()
//     {
//         Debug.Log(AllEnemiesDefeated());
//         // Check if current wave is defeated
//         if (isWaveActive && AllEnemiesDefeated())
//         {
//             // Start the next wave
//             StartNextWave();
//         }

//         // Check if there are no enemies to start the next wave
//         ActivateObjectsIfNoEnemies();
//     }

//     void StartNextWave()
//     {
//         // Check if there are more waves to start
//         if (currentWaveIndex < waves.Count)
//         {
//             // Activate the next wave
//             ActivateWave(currentWaveIndex);
//             currentWaveIndex++;
//             isWaveActive = true;
//         }
//         else
//         {
//             // All waves are complete
//             Debug.Log("All waves completed!");
//         }
//     }

//     bool AllEnemiesDefeated()
//     {
//             // Find all GameObjects with the "Enemy" tag
//             GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

//             if (enemies.Length != 0){
//                 return false;
//             }
//             return true;
            
//     }

//     void ActivateWave(int index)
//     {
//         // Activate the game objects associated with the wave (enemy spawning logic)
//         foreach (GameObject enemyPrefab in waves[index].enemiesToSpawn)
//         {
//             Instantiate(enemyPrefab, enemyPrefab.transform.position, enemyPrefab.transform.rotation);
//         }
//     }


//     // Method to check for enemies and advance to the next wave
//     void ActivateObjectsIfNoEnemies()
//     {
//         GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

//         if (enemies.Length == 0 && !isWaveActive)
//         {
//             // Start the next wave if there are no enemies and no wave is currently active
//             StartNextWave();
//         }
//     }
// }
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Wave
{
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
}

public class WaveManager : MonoBehaviour
{
    public List<Wave> waves = new List<Wave>(); // List of waves
    public List<Transform> spawnPoints = new List<Transform>(); // List of spawn points
    private int currentWaveIndex = 0;
    private bool isWaveActive = false;

    void Start()
    {
        // Start the first wave
        StartNextWave();
    }

    void Update()
    {
        // Check if current wave is defeated
        if (isWaveActive && AllEnemiesDefeated())
        {
            // Start the next wave
            StartNextWave();
        }

        // Check if there are no enemies to start the next wave
        ActivateObjectsIfNoEnemies();
    }

    void StartNextWave()
    {
        // Check if there are more waves to start
        if (currentWaveIndex < waves.Count)
        {
            // Activate the next wave
            ActivateWave(currentWaveIndex);
            currentWaveIndex++;
            isWaveActive = true;
        }
        else
        {
            // All waves are complete
            Debug.Log("All waves completed!");
            GameObject.Destroy(gameObject);
        }
    }

    bool AllEnemiesDefeated()
    {
        // Find all GameObjects with the "Enemy" tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length == 1;
    }

    void ActivateWave(int index)
    {
        Wave wave = waves[index];

        foreach (GameObject enemyPrefab in wave.enemiesToSpawn)
        {
            // Choose a random spawn point
            Transform spawnPoint = GetRandomSpawnPoint();

            if (spawnPoint != null)
            {
                // Instantiate the enemy at the chosen spawn point
                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            }
            else
            {
                Debug.LogWarning("No spawn points available for enemy spawning.");
            }
        }
    }

    Transform GetRandomSpawnPoint()
    {
        if (spawnPoints.Count > 0)
        {
            // Randomly select a spawn point
            int randomIndex = Random.Range(0, spawnPoints.Count);
            return spawnPoints[randomIndex];
        }
        else
        {
            return null;
        }
    }

    // Method to check for enemies and advance to the next wave
    void ActivateObjectsIfNoEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0 && !isWaveActive)
        {
            // Start the next wave if there are no enemies and no wave is currently active
            StartNextWave();
        }
    }
}
