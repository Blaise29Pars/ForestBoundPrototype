using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public int progressAmount;
    public Slider progressSlider;

    public static bool levelComplete = false;

    public GameObject levelCompleteText;

    [Header("Level 3 Shared Progress")]
    public bool useLevel3SharedProgress = false;
    public static int level3SharedProgress = 0;
    public static HashSet<string> collectedLevel3Spirits = new HashSet<string>();

    [Header("Audio")]
    public AudioClip levelCompleteSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        progressSlider.minValue = 0;

        if (useLevel3SharedProgress)
            progressAmount = level3SharedProgress;
        else
            progressAmount = 0;

        progressSlider.value = progressAmount;

        levelComplete = progressAmount >= progressSlider.maxValue;

        if (levelCompleteText != null)
            levelCompleteText.SetActive(levelComplete);
    }

    private void OnEnable()
    {
        Spirits.OnSpiritCollect += IncreaseProgressAmount;
    }

    private void OnDisable()
    {
        Spirits.OnSpiritCollect -= IncreaseProgressAmount;
    }

    void IncreaseProgressAmount(int amount)
    {
        progressAmount += amount;
        progressSlider.value = progressAmount;

        if (useLevel3SharedProgress)
            level3SharedProgress = progressAmount;

        if (progressAmount >= progressSlider.maxValue && !levelComplete)
        {
            levelComplete = true;

            if (audioSource != null && levelCompleteSound != null)
                audioSource.PlayOneShot(levelCompleteSound);

            if (levelCompleteText != null)
                levelCompleteText.SetActive(true);
        }
    }

    public static void SaveCollectedSpirit(string spiritID)
    {
        collectedLevel3Spirits.Add(spiritID);
    }

    public static bool HasCollectedSpirit(string spiritID)
    {
        return collectedLevel3Spirits.Contains(spiritID);
    }

    public static void ResetLevel3Progress()
    {
        level3SharedProgress = 0;
        levelComplete = false;
        collectedLevel3Spirits.Clear();
    }
}