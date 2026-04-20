using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    public Image[] slotImages; // assign the 3 item icon images (not the slot backgrounds)

    private string[] items = new string[3]; // store item IDs ("RedKey", etc.)

    void Start()
    {
        ClearHotbar();
    }

    public bool AddItem(string itemID, Sprite itemSprite)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (string.IsNullOrEmpty(items[i])) // slot is empty
            {
                items[i] = itemID;
                slotImages[i].sprite = itemSprite;
                slotImages[i].enabled = true; // show item icon
                return true;
            }
        }
        return false; // inventory full
    }

    public bool HasItem(string itemID)
    {
        foreach (string item in items)
        {
            if (item == itemID)
                return true;
        }
        return false;
    }

    public void RemoveItem(string itemID)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == itemID)
            {
                items[i] = null;
                slotImages[i].enabled = false; // hide icon instead of setting empty sprite
                return;
            }
        }
    }

    private void ClearHotbar()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = null;
            slotImages[i].enabled = false; // hide icons at start
        }
    }
}
