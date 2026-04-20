using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [Header("Items to Give")]
    public string[] itemIDs; // Changed to array
    public Sprite[] itemSprites; // Changed to array
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

        // 1. Check Requirements
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

            // 2. Consume Requirements
            if (consumeItemsOnUse)
            {
                foreach (string reqID in requiredItemIDs)
                {
                    hotbar.RemoveItem(reqID);
                }
                // Play sound once for consumption
                if (pickupSound != null) AudioSource.PlayClipAtPoint(pickupSound, transform.position, volume);
            }
        }

        // 3. Give Multiple Items
        bool allItemsAdded = true;
        if (itemIDs != null && itemIDs.Length > 0)
        {
            for (int i = 0; i < itemIDs.Length; i++)
            {
                // Check if we have a matching sprite for this ID
                Sprite spriteToGive = (itemSprites.Length > i) ? itemSprites[i] : null;

                if (!hotbar.AddItem(itemIDs[i], spriteToGive))
                {
                    allItemsAdded = false;
                    Debug.Log("Inventory full while adding: " + itemIDs[i]);
                }
            }
        }

        // 4. Finalize Interaction (Only if items were successfully given or there were no items to give)
        if (allItemsAdded)
        {
            isUsed = true;

            // Toggle GameObjects
            foreach (GameObject obj in objectsToDeactivate) if (obj != null) obj.SetActive(false);
            foreach (GameObject obj in objectsToActivate) if (obj != null) obj.SetActive(true);

            // Play pickup sound
            if (pickupSound != null) AudioSource.PlayClipAtPoint(pickupSound, transform.position, volume);

            // Handle visual changes or destruction
            if (changedSprite != null && spriteRenderer != null)
            {
                spriteRenderer.sprite = changedSprite;
                if (GetComponent<BoxCollider2D>() != null)
                    GetComponent<BoxCollider2D>().enabled = false;
            }
            else if (obtainableItem)
            {
                Destroy(gameObject);
            }
        }
    }
}
