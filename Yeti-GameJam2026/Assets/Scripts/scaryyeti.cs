using UnityEngine;
using System.Collections;

public class scaryyeti : MonoBehaviour
{
    public bool fadeEnabled = true;
    public float fadeDuration = 2.0f; // Added a variable for timing
    private Renderer myRenderer;

    void Awake()
    {
        // Cache the renderer so we don't have to find it every time
        myRenderer = GetComponent<Renderer>();
    }

    void OnEnable()
    {
        if (fadeEnabled && myRenderer != null)
        {
            // You must use StartCoroutine to run an IEnumerator
            StartCoroutine(FadeOut(myRenderer, fadeDuration));
        }
    }

    void OnDisable()
    {
        // Careful: Destroying here will trigger whenever the fade finishes 
        // because FadeOut sets active to false.
        Destroy(gameObject);
    }

    public IEnumerator FadeOut(Renderer targetRenderer, float duration)
    {
        Material mat = targetRenderer.material;
        Color startColor = mat.color;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            mat.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
