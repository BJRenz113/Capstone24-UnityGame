using UnityEngine;
using UnityEngine.UI; // Required for UI elements
using System.Collections;

public class ObjectActivation : MonoBehaviour
{
    public GameObject[] objectsToActivate; // List of objects to activate

    public void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < objectsToActivate.Length; i++)
        {
            objectsToActivate[i].SetActive(enemies.Length == 0);

            string exText = "";

            if (enemies.Length == 0) exText = " (Room Cleared)";

            string ogText = GameObject.FindWithTag("Player").GetComponent<Player>().getText.Split(" ")[0];
            GameObject.FindWithTag("Player").GetComponent<Player>().getText = ogText + exText;
        }
    }
}
