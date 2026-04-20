using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Only if youre using TextMeshPro

public class GameOverMenu : MonoBehaviour
{
    public GameObject GameOverMenuUI;
    public int sceneBuildIndex;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void IsGameOver()
    {
        GameOverMenuUI.SetActive(true);      
    }

    public void Restart()
    {
        audioSource.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        audioSource.Play();
        Application.Quit();
    }
}
