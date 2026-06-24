using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject checkpointText;

    private bool activated = false;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (checkpointText != null)
        {
            checkpointText.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !activated)
        {
            collision.GetComponent<PlayerRespawn>().SetCheckpoint(transform.position);

            activated = true;

            // Play checkpoint sound
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // Show checkpoint message
            if (checkpointText != null)
            {
                checkpointText.SetActive(true);
                Invoke(nameof(HideMessage), 3f);
            }

            Debug.Log("Checkpoint Activated!");
        }
    }

    void HideMessage()
    {
        if (checkpointText != null)
        {
            checkpointText.SetActive(false);
        }
    }
}