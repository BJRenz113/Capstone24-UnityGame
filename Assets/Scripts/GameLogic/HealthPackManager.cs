using UnityEngine;

public class HealthPackManager : MonoBehaviour
{
    public static HealthPackManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InstantiateHealthPack(GameObject healthPackPrefab)
    {
        // Instantiate the health pack off-screen
        Vector3 offscreenPosition = new Vector3(1000f, 1000f, 1000f); // Adjust the values as needed
        GameObject healthPack = Instantiate(healthPackPrefab, offscreenPosition, Quaternion.identity);
    }
}
