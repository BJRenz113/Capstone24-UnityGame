using UnityEngine;

public class SummonerSpawnZone : MonoBehaviour
{
    private GameObject playerGameObject;

    void Start()
    {
        playerGameObject = GameObject.FindWithTag("Player");
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("WORKSSSS");
        if (collision.gameObject.CompareTag("Player"))
        {
            SummonerChaseState chaseState = playerGameObject.GetComponent<SummonerChaseState>();
            if (chaseState != null)
            {
                chaseState.SetInSpawn(true);
            }
        }
    }
}
