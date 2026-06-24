using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 respawnPoint;
    private Rigidbody2D rb;

    public AudioClip deathSound;
    private AudioSource audioSource;

    void Start()
    {
        if (Level3ReturnData.returningFromHiddenArea)
        {
            GameObject returnPoint = GameObject.Find(Level3ReturnData.returnPointName);

            if (returnPoint != null)
            {
                transform.position = returnPoint.transform.position;
            }

            Level3ReturnData.returningFromHiddenArea = false;
        }
        respawnPoint = transform.position;
        rb = GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        respawnPoint = newCheckpoint;
    }

    public void Respawn()
    {
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        transform.position = respawnPoint;

        rb.velocity = Vector2.zero;
    }
}