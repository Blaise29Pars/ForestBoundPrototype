using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectController : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip buttonSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnLevel1Click()
    {
        PlaySound();
        Invoke(nameof(LoadLevel1), 0.5f);
    }

    public void OnLevel2Click()
    {
        PlaySound();
        Invoke(nameof(LoadLevel2), 0.5f);
    }

    public void OnLevel3Click()
    {
        PlaySound();
        Invoke(nameof(LoadLevel3), 0.5f);
    }

    public void OnBackClick()
    {
        PlaySound();
        Invoke(nameof(LoadMainMenu), 0.5f);
    }

    void PlaySound()
    {
        if (audioSource != null && buttonSound != null)
            audioSource.PlayOneShot(buttonSound);
    }

    void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
    }

    void LoadLevel2()
    {
        SceneManager.LoadScene("Level 2", LoadSceneMode.Single);
    }

    void LoadLevel3()
    {
        GameController.ResetLevel3Progress();

        SceneManager.LoadScene("Level 3", LoadSceneMode.Single);
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }
}