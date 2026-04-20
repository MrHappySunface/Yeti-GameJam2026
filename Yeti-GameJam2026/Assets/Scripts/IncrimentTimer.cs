using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class IncrimentTimer : MonoBehaviour
{
    private CircleCollider2D plantCollider;
    public float growthTime = 20f;
    public float gameoverDelay = 1f;

    // 1. Change the list from Sprites to GameObjects
    [SerializeField] public List<GameObject> growthStages;
    public GameOverMenu gameOverMenu;

    void Start()
    {
        // Ensure all stages are hidden at the start
        foreach (GameObject stage in growthStages)
        {
            if (stage != null) stage.SetActive(false);
        }

        if (growthStages.Count > 0)
        {
            StartCoroutine(Growth());
        }
    }

    private IEnumerator Growth()
    {
        float timePerStage = growthTime / growthStages.Count;

        for (int i = 0; i < growthStages.Count; i++)
        {
            // Activate the current stage
            if (growthStages[i] != null) growthStages[i].SetActive(true);

            // Deactivate the previous stage (if there was one)
            if (i > 0 && growthStages[i - 1] != null)
            {
                growthStages[i - 1].SetActive(false);
            }

            yield return new WaitForSeconds(growthTime);
        }

        if (gameOverMenu != null)
        {
            yield return new WaitForSeconds(gameoverDelay);
            gameOverMenu.IsGameOver();
        }
    }
}
