using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HiddenAreaExitDoor : MonoBehaviour
{
    public GameObject DoorText;
    public AudioClip doorSound;

    [Header("Return Point In Level 3")]
    public string returnPointName;

    private bool playerNearDoor = false;
    private AudioSource audioSource;

    void Start()
    {
        if (DoorText != null)
            DoorText.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerNearDoor)
        {
            if (DoorText != null)
                DoorText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(ReturnToLevel3());
            }
        }
        else
        {
            if (DoorText != null)
                DoorText.SetActive(false);
        }
    }

    IEnumerator ReturnToLevel3()
    {
        if (audioSource != null && doorSound != null)
            audioSource.PlayOneShot(doorSound);

        PlayerPrefs.SetInt("ReturningFromHiddenArea", 1);
        PlayerPrefs.SetString("Level3ReturnPoint", returnPointName);
        PlayerPrefs.Save();

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Level 3");
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