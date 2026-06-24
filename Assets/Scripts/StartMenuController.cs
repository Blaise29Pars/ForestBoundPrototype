using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public AudioClip startButtonSound;
    public AudioClip levelSelectButtonSound;
    public AudioClip exitButtonSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnStartClick()
    {
        if (audioSource != null && startButtonSound != null)
            audioSource.PlayOneShot(startButtonSound);

        Invoke(nameof(LoadLevel), 0.5f);
    }

    public void OnLevelSelectClick()
    {
        if (audioSource != null && levelSelectButtonSound != null)
            audioSource.PlayOneShot(levelSelectButtonSound);

        Invoke(nameof(LoadLevelSelect), 0.5f);
    }

    void LoadLevel()
    {
        SceneManager.LoadScene("Level 1");
    }

    void LoadLevelSelect()
    {
        SceneManager.LoadScene("Level Select");
    }

    public void OnExitClick()
    {
        if (audioSource != null && exitButtonSound != null)
            audioSource.PlayOneShot(exitButtonSound);

        Invoke(nameof(QuitGame), 0.5f);
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}