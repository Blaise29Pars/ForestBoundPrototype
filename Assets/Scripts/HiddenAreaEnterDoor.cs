using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HiddenAreaEnterDoor : MonoBehaviour
{
    public string hiddenAreaSceneName;
    public GameObject DoorText;
    public AudioClip doorSound;

    private bool playerNearDoor;
    private bool entering;
    private AudioSource audioSource;

    void Start()
    {
        if (DoorText != null)
            DoorText.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (DoorText != null)
            DoorText.SetActive(playerNearDoor);

        if (playerNearDoor && !entering && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E was pressed on hidden area door");
            StartCoroutine(EnterHiddenArea());
        }
    }

    IEnumerator EnterHiddenArea()
    {
        entering = true;

        if (audioSource != null && doorSound != null)
        {
            audioSource.PlayOneShot(doorSound);
            yield return new WaitForSeconds(doorSound.length);
        }

        Debug.Log("Loading hidden area scene: " + hiddenAreaSceneName);
        SceneManager.LoadScene(hiddenAreaSceneName);
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