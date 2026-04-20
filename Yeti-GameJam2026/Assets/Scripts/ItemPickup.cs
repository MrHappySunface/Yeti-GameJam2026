using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemID = "";
    public Sprite itemSprite;
    public bool obtainableItem = true;

    [Header("RequiredItem(Optional)")]
    public string[] requiredItemIDs;
    public bool consumeItemsOnUse = true;
    public GameObject deniedTextBox;

    [Header("Pickup Sound")]
    public AudioClip pickupSound;
    public float volume = 1f;

    [Header("Events to toggle")]
    public GameObject[] objectsToDeactivate;
    public GameObject[] objectsToActivate;

    [Header("Item visuals(Destroy if Empty)")]
    public Sprite changedSprite;

    private SpriteRenderer spriteRenderer;
    private bool isUsed = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (Dialogue.isActive || isUsed) return;

        Hotbar hotbar = FindAnyObjectByType<Hotbar>();
        if (hotbar == null) return;

        if (requiredItemIDs != null && requiredItemIDs.Length > 0)
        {
            foreach (string reqID in requiredItemIDs)
            {
                if (!hotbar.HasItem(reqID))
                {
                    if (deniedTextBox != null) deniedTextBox.SetActive(true);
                    return;
                }
            }
            if (consumeItemsOnUse)
            {
                foreach (string reqID in requiredItemIDs)
                {
                    Debug.Log("Removing: " + reqID);
                    hotbar.RemoveItem(reqID);

                    if (changedSprite != null && spriteRenderer != null)
                    {
                        spriteRenderer.sprite = changedSprite;
                        GetComponent<BoxCollider2D>().enabled = false;
                    }
                    else
                    {
                        if (!obtainableItem)
                        {
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }

        if (hotbar != null)
        {
            isUsed = true;

            // Fix: Loop through the arrays to toggle objects
            foreach (GameObject obj in objectsToDeactivate) if (obj != null) obj.SetActive(false);
            foreach (GameObject obj in objectsToActivate) if (obj != null) obj.SetActive(true);

            if (itemSprite != null && itemID.Length > 0)
            { 
                if (hotbar.AddItem(itemID, itemSprite))
                {
                    // Play sound at the item's location
                    if (pickupSound != null)
                    {
                        AudioSource.PlayClipAtPoint(pickupSound, transform.position, volume);
                    }

                    if (changedSprite != null && spriteRenderer != null)
                    {
                        spriteRenderer.sprite = changedSprite;
                        GetComponent<BoxCollider2D>().enabled = false;
                    }
                    else
                    {
                        if (obtainableItem)
                        {
                            Destroy(gameObject);
                        }
                       
                    }
                }
                else
                {
                    Debug.Log("Inventory full!");
                }
            }
        }
    }
}


