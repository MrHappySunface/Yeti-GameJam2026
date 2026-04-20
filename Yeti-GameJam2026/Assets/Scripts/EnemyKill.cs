using UnityEngine;
using System.Collections;

public class EnemyKill : MonoBehaviour
{
    public GameObject jumpscareObject;
    public GameOverMenu gameoverMenu; // your pause/game over handler

    [Header("Settings")]
    public float delayBeforeGameOver = 2f; // wait this long before showing menu

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something entered: " + other.name);

        if (other.CompareTag("Player"))
        {
            StartCoroutine(CaughtSequence());
        }
    }

    private IEnumerator CaughtSequence()
    {
        // Enable catch animation/sound object
        if (jumpscareObject != null)
            jumpscareObject.SetActive(true);

        // Wait for animation/sound to finish
        yield return new WaitForSeconds(delayBeforeGameOver);     

        // Show your Game Over
        if (gameoverMenu != null)
            gameoverMenu.IsGameOver(); 
    }
}
