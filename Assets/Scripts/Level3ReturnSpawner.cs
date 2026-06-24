using UnityEngine;

public class Level3ReturnSpawner : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("ReturningFromHiddenArea", 0) == 1)
        {
            string returnPointName = PlayerPrefs.GetString("Level3ReturnPoint", "");

            GameObject returnPoint = GameObject.Find(returnPointName);
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (returnPoint != null && player != null)
            {
                player.transform.position = returnPoint.transform.position;
            }

            PlayerPrefs.SetInt("ReturningFromHiddenArea", 0);
            PlayerPrefs.Save();
        }
    }
}