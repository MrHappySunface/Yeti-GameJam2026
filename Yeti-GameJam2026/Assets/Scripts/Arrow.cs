using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Scene to Toggle")]
    public GameObject sceneToDeactivate;
    public GameObject sceneToActivate;

    [Header("Sound Effect")]
    public AudioClip arrowSound;
    public float volume = 1f;

    private void OnMouseDown()
    {
        if (Dialogue.isActive) return;

        if (sceneToDeactivate != null) sceneToDeactivate.SetActive(false);
        if (sceneToActivate != null) sceneToActivate.SetActive(true);

        if (arrowSound != null)
        {
            AudioSource.PlayClipAtPoint(arrowSound, transform.position, volume);
        }
    }
}