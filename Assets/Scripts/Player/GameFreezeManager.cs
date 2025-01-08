using UnityEngine;
using System.Collections; // Add this line to resolve the IEnumerator error

public class FreezeGameManager : MonoBehaviour
{
    public static FreezeGameManager instance; // Singleton instance

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void FreezeGameForSeconds(float duration)
    {
        StartCoroutine(FreezeGameCoroutine(duration));
    }

    private IEnumerator FreezeGameCoroutine(float duration)
    {
        Time.timeScale = 0f; // Set the time scale to 0 to freeze the game

        yield return new WaitForSecondsRealtime(duration); // Wait for the specified duration in real time

        Time.timeScale = 1f; // Restore the time scale to 1 to resume the game
    }
}
