using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Door : MonoBehaviour
{
    bool playerNearDoor = false;

    public GameObject DoorText;

    public AudioClip doorSound;
    private AudioSource audioSource;

    void Start()
    {
        if (DoorText != null)
            DoorText.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerNearDoor && GameController.levelComplete)
        {
            if (DoorText != null)
                DoorText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(LoadNextLevel());
            }
        }
        else
        {
            if (DoorText != null)
                DoorText.SetActive(false);
        }
    }

    IEnumerator LoadNextLevel()
    {
        if (audioSource != null && doorSound != null)
            audioSource.PlayOneShot(doorSound);

        yield return new WaitForSeconds(1.5f);

        if (SceneManager.GetActiveScene().name == "Level 2")
        {
            GameController.ResetLevel3Progress();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerNearDoor = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerNearDoor = false;
    }
}