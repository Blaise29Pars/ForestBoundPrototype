using UnityEngine;
using System;
using System.Collections;

public class Spirits : MonoBehaviour, IItem
{
    public static event Action<int> OnSpiritCollect;
    public int worth = 1;

    public AudioClip collectSound;

    [Header("Level 3 / Hidden Area Save")]
    public bool useLevel3Save = false;
    public string spiritID;

    private bool collected = false;

    void Start()
    {
        if (useLevel3Save && GameController.HasCollectedSpirit(spiritID))
        {
            Destroy(gameObject);
        }
    }

    public void Collect()
    {
        if (collected) return;

        collected = true;

        if (useLevel3Save)
        {
            GameController.SaveCollectedSpirit(spiritID);
        }

        OnSpiritCollect?.Invoke(worth);

        StartCoroutine(CollectRoutine());
    }

    IEnumerator CollectRoutine()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        if (collectSound != null && Camera.main != null)
        {
            AudioSource.PlayClipAtPoint(
                collectSound,
                Camera.main.transform.position
            );

            yield return new WaitForSeconds(collectSound.length);
        }

        Destroy(gameObject);
    }
}